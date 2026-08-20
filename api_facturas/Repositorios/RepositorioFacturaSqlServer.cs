// ============================================================
// RepositorioFacturaSqlServer — la capa de DATOS de factura.
//
// La API como TRADUCTORA: este repositorio no escribe SQL de
// tablas — llama procedimientos almacenados (CommandType.
// StoredProcedure), recibe su resultado como JSON por el
// parámetro de salida @p_resultado, y lo deserializa a los
// modelos (System.Text.Json).
//
// También traduce los THROW de los SPs a excepciones de negocio:
// los números y mensajes de error del motor son un detalle de la
// capa de datos — servicio y controlador no conocen SqlException.
// ============================================================

using System.Data;
using System.Text.Json;
using System.Text.Json.Serialization;
using ApiFacturas.Excepciones;
using ApiFacturas.Modelos;
using Microsoft.Data.SqlClient;

namespace ApiFacturas.Repositorios;

public class RepositorioFacturaSqlServer : IRepositorioFactura
{
    private readonly string _cadenaConexion;

    // Opciones de deserialización: ignorar mayúsculas/minúsculas
    // ("numero" del SP → propiedad Numero). Las claves snake_case
    // las resuelven los [JsonPropertyName] de los modelos.
    private static readonly JsonSerializerOptions _opcionesJson = new()
    {
        PropertyNameCaseInsensitive = true,
    };

    public RepositorioFacturaSqlServer(string cadenaConexion)
    {
        _cadenaConexion = cadenaConexion;
    }

    // ------------------------------------------------------------
    // El ayudante central: ejecutar un SP y devolver su JSON
    // ------------------------------------------------------------

    /// <summary>Ejecuta el SP indicado (con los parámetros que agregue
    /// 'configurar'), agrega el @p_resultado de salida, y devuelve el
    /// JSON que el SP dejó ahí. Traduce los THROW 50010 a excepciones
    /// de negocio; cualquier otro error del motor sube tal cual (500).</summary>
    private async Task<string> EjecutarSpAsync(string nombreSp, Action<SqlParameterCollection> configurar)
    {
        await using var conexion = new SqlConnection(_cadenaConexion);
        // CommandType.StoredProcedure = "el texto es el NOMBRE de un SP":
        await using var comando = new SqlCommand(nombreSp, conexion)
        {
            CommandType = CommandType.StoredProcedure,
        };
        configurar(comando.Parameters);

        // El parámetro de SALIDA donde el SP deja su JSON.
        // Tamaño -1 = NVARCHAR(MAX):
        var salida = new SqlParameter("@p_resultado", SqlDbType.NVarChar, -1)
        {
            Direction = ParameterDirection.Output,
        };
        comando.Parameters.Add(salida);

        try
        {
            await conexion.OpenAsync();
            await comando.ExecuteNonQueryAsync();
        }
        // "catch … when" atrapa SOLO si la condición se cumple.
        // Números de los THROW en db/bdfacturas.sql: 50003 = el "no
        // existe" de sp_consultar · 50010 = los dos errores de sp_anular
        // ("no existe" y "ya está anulada"):
        catch (SqlException e) when ((e.Number == 50003 || e.Number == 50010)
                                     && e.Message.Contains("no existe"))
        {
            throw new NoEncontradoExcepcion(e.Message);      // → 404
        }
        catch (SqlException e) when (e.Number == 50010 && e.Message.Contains("anulada"))
        {
            throw new ConflictoExcepcion(e.Message);         // → 409
        }
        // Lo demás (stock insuficiente del trigger, FK inexistente,
        // el 50002 del mínimo de renglones) sube tal cual → 500.

        return (string)salida.Value;
    }

    // El SP de consultar/crear responde {"factura":{…},"productos":[…]}.
    // Esta clase privada calza ese sobre para poder deserializarlo y
    // devolver UNA Factura con su detalle adentro:
    private class RespuestaFacturaSp
    {
        [JsonPropertyName("factura")]
        public Factura? Factura { get; set; }

        [JsonPropertyName("productos")]
        public List<ProductoDeFactura>? Productos { get; set; }
    }

    private static Factura ArmarFactura(string json)
    {
        var respuesta = JsonSerializer.Deserialize<RespuestaFacturaSp>(json, _opcionesJson)!;
        var factura = respuesta.Factura!;
        factura.Productos = respuesta.Productos ?? new List<ProductoDeFactura>();
        return factura;
    }

    // ------------------------------------------------------------
    // Los 4 métodos del contrato
    // ------------------------------------------------------------

    public async Task<List<Factura>> ListarAsync()
    {
        // Este SP no recibe parámetros de entrada:
        var json = await EjecutarSpAsync("sp_listar_facturas_y_productosporfactura", _ => { });
        // Devuelve un ARRAY de facturas, cada una con "productos" adentro:
        return JsonSerializer.Deserialize<List<Factura>>(json, _opcionesJson) ?? new List<Factura>();
    }

    public async Task<Factura> ConsultarAsync(int numero)
    {
        var json = await EjecutarSpAsync("sp_consultar_factura_y_productosporfactura",
            parametros => parametros.AddWithValue("@p_numero", numero));
        return ArmarFactura(json);
    }

    public async Task<Factura> CrearAsync(int fkidcliente, int fkidvendedor, string productosJson)
    {
        var json = await EjecutarSpAsync("sp_insertar_factura_y_productosporfactura", parametros =>
        {
            parametros.AddWithValue("@p_fkidcliente", fkidcliente);
            parametros.AddWithValue("@p_fkidvendedor", fkidvendedor);
            // El detalle viaja como JSON y el SP lo abre con OPENJSON —
            // un solo viaje a la BD, UNA transacción (la lección ACID):
            parametros.AddWithValue("@p_productos", productosJson);
        });
        return ArmarFactura(json);
    }

    public async Task<string> AnularAsync(int numero)
    {
        // El JSON del SP ({mensaje, numero_anulado, total_anulado,…}) ES
        // la respuesta del contrato: se devuelve tal cual, sin retiparlo.
        return await EjecutarSpAsync("sp_anular_factura",
            parametros => parametros.AddWithValue("@p_numero", numero));
    }
}

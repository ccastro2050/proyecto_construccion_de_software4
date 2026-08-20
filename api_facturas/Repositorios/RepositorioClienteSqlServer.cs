// RepositorioClienteSqlServer — la capa de DATOS de cliente (v3).
// CALCADO de RepositorioProductoSqlServer: ADO.NET, SQL parametrizado, async.

using ApiFacturas.Modelos;
using Microsoft.Data.SqlClient;

namespace ApiFacturas.Repositorios;

public class RepositorioClienteSqlServer : IRepositorioCliente
{
    private readonly string _cadenaConexion;

    public RepositorioClienteSqlServer(string cadenaConexion)
    {
        _cadenaConexion = cadenaConexion;
    }

    private async Task<SqlConnection> AbrirConexionAsync()
    {
        var conexion = new SqlConnection(_cadenaConexion);
        await conexion.OpenAsync();
        return conexion;
    }

    private static Cliente Armar(SqlDataReader lector)
    {
        return new Cliente
        {
            Id = lector.GetInt32(0),
            Credito = lector.GetDecimal(1),
            Fkcodpersona = lector.GetString(2),
            Fkcodempresa = lector.IsDBNull(3) ? null : lector.GetString(3),
        };
    }

    public async Task<List<Cliente>> ObtenerTodosAsync(int limite)
    {
        const string sql = @"SELECT TOP (@limite) id, credito, fkcodpersona, fkcodempresa
                             FROM cliente ORDER BY id";
        await using var conexion = await AbrirConexionAsync();
        await using var comando = new SqlCommand(sql, conexion);
        comando.Parameters.AddWithValue("@limite", limite);
        await using var lector = await comando.ExecuteReaderAsync();
        var lista = new List<Cliente>();
        while (await lector.ReadAsync()) { lista.Add(Armar(lector)); }
        return lista;
    }

    public async Task<Cliente?> ObtenerPorIdAsync(int id)
    {
        const string sql = @"SELECT id, credito, fkcodpersona, fkcodempresa FROM cliente WHERE id = @id";
        await using var conexion = await AbrirConexionAsync();
        await using var comando = new SqlCommand(sql, conexion);
        comando.Parameters.AddWithValue("@id", id);
        await using var lector = await comando.ExecuteReaderAsync();
        if (await lector.ReadAsync()) { return Armar(lector); }
        return null;
    }

    public async Task CrearAsync(Cliente entidad)
    {
        const string sql = @"INSERT INTO cliente (credito, fkcodpersona, fkcodempresa)
                             VALUES (@credito, @fkcodpersona, @fkcodempresa)";
        await using var conexion = await AbrirConexionAsync();
        await using var comando = new SqlCommand(sql, conexion);
        comando.Parameters.AddWithValue("@credito", entidad.Credito);
        comando.Parameters.AddWithValue("@fkcodpersona", entidad.Fkcodpersona);
        comando.Parameters.AddWithValue("@fkcodempresa", (object?)entidad.Fkcodempresa ?? DBNull.Value);
        await comando.ExecuteNonQueryAsync();
    }

    public async Task<int> ActualizarAsync(int id, Dictionary<string, object> datos)
    {
        // SET dinámico con lista blanca (los nombres salen de las PETICIONES):
        var asignaciones = new List<string>();
        foreach (var columna in datos.Keys) { asignaciones.Add($"{columna} = @{columna}"); }
        var sql = $"UPDATE cliente SET {string.Join(", ", asignaciones)} WHERE id = @pk_clave";
        await using var conexion = await AbrirConexionAsync();
        await using var comando = new SqlCommand(sql, conexion);
        foreach (var (columna, valor) in datos) { comando.Parameters.AddWithValue($"@{columna}", valor); }
        comando.Parameters.AddWithValue("@pk_clave", id);
        return await comando.ExecuteNonQueryAsync();
    }

    public async Task<int> EliminarAsync(int id)
    {
        const string sql = "DELETE FROM cliente WHERE id = @id";
        await using var conexion = await AbrirConexionAsync();
        await using var comando = new SqlCommand(sql, conexion);
        comando.Parameters.AddWithValue("@id", id);
        return await comando.ExecuteNonQueryAsync();
    }
}

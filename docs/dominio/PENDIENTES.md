# Lo que falta — registro de pendientes

> **Qué es este documento.** Lo que este repositorio **todavía no tiene**,
> dicho antes de que alguien lo descubra en la sustentación. Cada punto dice
> **qué falta**, **dónde** y **cómo se comprueba** que ya se hizo.
>
> No es una lista de deseos: es lo que se midió —con el sistema encendido— el
> **8 de octubre de 2026**, con el repositorio en la v4.
>
> **La regla de este archivo:** un pendiente que se cierra se **tacha con su
> commit**, no se borra. Lo que se borró nunca estuvo — y un documento donde
> los pendientes desaparecen sin rastro no se puede auditar.

---

## 0. Lo que SÍ está, para que el pendiente se lea en su tamaño

Antes de la lista de huecos conviene ver el tamaño del edificio, porque los
pendientes de abajo son remates, no cimientos. Todo esto se midió hoy:

| | Cuánto | Cómo se comprobó |
|---|---|---|
| **La base de datos** | 12 tablas · 2 disparadores · 16 procedimientos | `db/bdfacturas_postgres.sql` |
| **La API** | 15 controladores · **43 rutas, 81 operaciones** | `GET /swagger/v1/swagger.json` |
| **Las tres capas** | 14 servicios + 14 interfaces · **28 repositorios** + 14 interfaces | `ls api_facturas/` |
| **El control de acceso** | token, **401 sin token y 403 sin permiso** | La matriz de §5 |
| **Las 10 consultas de la v4** | las diez responden 200 | §5 |
| **El front** | **15 pantallas**, todas en 200 | §5 |

> **Los 28 repositorios contra las 14 interfaces son la arquitectura en un
> número:** cada contrato tiene dos implementaciones —PostgreSQL y SQL
> Server— y arriba nadie sabe cuál está puesta.

---

## 1. La v4, que está EN CURSO

Lo que falta para cerrarla. Está también en [`PLAN_V4.md`](PLAN_V4.md) §7 y en
[`v4_aplicativo/2_spec.md`](../spec_kit/versiones/v4_aplicativo/2_spec.md).

| | Estado | Qué falta exactamente | Cómo se comprueba |
|---|---|---|---|
| **Las 10 consultas y el tablero** | **Hecho** | — | Las diez responden 200 y el tablero las dibuja |
| **Imagen corporativa** | **Hecho** | — | `marca.css` enchufada en `App.razor` (línea 30), con los colores del [manual](MANUAL_DE_MARCA.md) |
| **Páginas corporativas** | **Pendiente** | Hay `Home.razor`. Faltan **servicios, soporte y contacto** | Que existan las tres páginas y el menú las alcance |
| **Responsive / PWA** | **A medias** | Responsive **sí** (Bootstrap). En `front_blazor/wwwroot/` hay `app.css`, `marca.css`, `js/` y `lib/`: **no hay `manifest.json` ni *service worker*** | Que el navegador ofrezca instalar el sitio |
| **Publicación** | **Pendiente** | Publicar con los **secretos en variables de entorno del servidor**, no en el repositorio | Una URL que responda, y ningún secreto en el `git log` |

> **La distinción que más se confunde:** «responsive» es que la interfaz se
> acomode al ancho; **PWA** es que el navegador la pueda instalar y abrir sin
> red. Lo primero está; lo segundo son dos archivos que no existen.

---

## 2. Los comentarios de la base de datos — la interpretabilidad

Desde la v2, el 20 % de la nota de cada versión es interpretabilidad
([`0_METODOLOGIA.md`](../../ProyectosDeAula/docs/0_METODOLOGIA.md) §7), y el
comentario es lo que queda cuando el recuerdo de la conversación se fue.

Los dos scripts **están comentados** —514 líneas de comentario en el de
PostgreSQL y 501 en el de SQL Server—, pero el reparto no es parejo. Contando
los comentarios **por dentro de cada procedimiento**, seis quedaron en cero:

| Procedimiento | Comentarios internos |
|---|---|
| `sp_consultar_factura_y_productosporfactura` | **0** |
| `sp_listar_facturas_y_productosporfactura` | **0** |
| `consultar_usuario_con_roles` | **0** |
| `listar_usuarios_con_roles` | **0** |
| `verificar_acceso_ruta` | **0** |
| `listar_rutarol` | **0** |

> **Y los seis son los que SOLO LEEN**, lo cual explica el descuido y no lo
> justifica: el que escribe se comenta porque da miedo, y el que lee se deja
> porque «se entiende». Pero `verificar_acceso_ruta` es **el que decide cada
> 403 de la aplicación**, y `listar_usuarios_con_roles` arma su JSON con
> `json_agg` sobre una subconsulta — dos cosas que nadie adivina leyendo.
>
> **Lo que falta son las decisiones, no las palabras.** Ya se midió que ahí no
> hay sintaxis nueva: comparando el vocabulario SQL de esos seis contra el de
> `sp_insertar_factura`, lo único de más es SQL corriente —`EXISTS`,
> `ORDER BY`, `COALESCE`—.

Se comprueba así:

```powershell
# cuántos comentarios tiene cada procedimiento por dentro
Select-String -Path db\bdfacturas_postgres.sql -Pattern 'CREATE OR REPLACE PROCEDURE'
```

---

## 3. El material del curso

| | Estado | Qué falta |
|---|---|---|
| [`0_METODOLOGIA.md`](../../ProyectosDeAula/docs/0_METODOLOGIA.md) | **Hecho** | Incluye el disparador obligatorio y las maestro-detalle pedidas |
| [`VERSION_2.md`](../../ProyectosDeAula/docs/VERSION_2.md) | **Hecho** | Con sus ejemplos sobre **cátedras**, que es el proyecto de aula de este curso |
| **`VERSION_3.md`** | **No existe** | El equivalente para la v3: el control de acceso, con sus criterios y lo que suele salir mal |
| **`VERSION_4.md`** | **No existe** | El equivalente para la v4: las 10 consultas, el tablero, la marca y la publicación |
| **Los planes de versión** | **Hecho** | [`PLAN_V1`](PLAN_V1.md) · [`PLAN_V2`](PLAN_V2.md) · [`PLAN_V3`](PLAN_V3.md) · [`PLAN_V4`](PLAN_V4.md) |

> **El proyecto de aula de este curso es el de cátedras**, no `bdfacturas`.
> `bdfacturas` es el sistema que el profesor construye a la vista para
> enseñar; lo que los equipos entregan es su propio proyecto, con la
> metodología de [`0_METODOLOGIA.md`](../../ProyectosDeAula/docs/0_METODOLOGIA.md).

---

## 4. Lo que GitHub muestra, y lo que no

| | Estado |
|---|---|
| **La carpeta `v5_otros_motores`** | **Fuera de GitHub, y bien.** Salió con `git rm --cached`, está en el `.gitignore` (línea 15) y `git ls-files` sobre ella devuelve **0 archivos**. Sigue en el disco: es trabajo en curso, y la historia no se reescribió |
| **Los tags** | `v1` · `v2` · `v3` · `v4`. **No hay tag `v5`**, así que no hay forma de que el desplegable de GitHub muestre la v5. (En `proyecto_aplicacion_y_servicios_web4` sí existe ese tag, y ahí sigue siendo una decisión abierta) |
| **Los tags describen el mapa VIEJO** | «segundo motor», «tercer motor». Con el mapa nuevo esas serían la v5. **No se mueven:** un tag es la foto de lo que se entregó ese día; reescribirlo sería falsificar la historia ([`CRONOGRAMA.md`](CRONOGRAMA.md)) |

---

## 5. Cómo se midió todo esto

Para que el próximo que lea esto no tenga que creer. Con el sistema
encendido, el 8 de octubre de 2026:

| Qué se comprobó | Resultado |
|---|---|
| La API en pie | `GET /` → 200, `motor: postgres` |
| **401 sin token** | `GET /api/producto` sin cabecera → **401** |
| **El 403, con tres usuarios** | ver la matriz de abajo |
| Las diez consultas | **200** las diez, con `admin` |
| Las 15 pantallas del front | **200** las quince |
| El contrato publicado | **43 rutas, 81 operaciones** en `swagger.json` |
| El principio abierto/cerrado | `git diff --stat v3..v4 -- Controllers Servicios` → **vacío**; y los 12 archivos de SQL Server, **1 232 líneas** |

**La matriz del 403**, que es el criterio que de verdad importa:

| | `/producto` | `/factura` | `/cliente` | `/usuario` | `/rutarol` |
|---|---|---|---|---|---|
| **admin** | 200 | 200 | 200 | 200 | 200 |
| **vendedor1** | **403** | 200 | 200 | **403** | **403** |
| **cliente1** | 200 | **403** | **403** | **403** | **403** |

> Las contraseñas están escritas en `db/bdfacturas_postgres.sql`, y eso **no
> es un descuido**: se guardan con hash bcrypt y de un hash no se vuelve a la
> clave. Sin tenerlas anotadas no habría forma de entrar a comprobar nada.

**Y dos cosas que parecen pendientes y no lo son:**

| | Por qué está bien así |
|---|---|
| `productos-sin-vender`, `interfaces-sin-usuarios` y `anulaciones-por-cliente` devuelven **0 filas** | Porque **es el dato correcto**: con lo sembrado, todo producto se ha vendido, toda ruta tiene quien entre y no hay facturas anuladas. Una consulta con cero filas no está rota — y confundir «vacío» con «falla» hace perder tardes |
| Los puentes no tienen `PUT` ni `PATCH` | En una tabla puente las dos columnas **son** la llave: una pareja existe o no existe. Los dos verbos están **escritos y apagados** en el controlador, con su explicación ([`REQUISITOS_FUNCIONALES.md`](REQUISITOS_FUNCIONALES.md) §6) |

---

| Qué | Dónde |
|---|---|
| Lo que falta de la v4, con su razón | [`PLAN_V4.md`](PLAN_V4.md) §7 |
| La rúbrica y el 20 % de interpretabilidad | [`0_METODOLOGIA.md`](../../ProyectosDeAula/docs/0_METODOLOGIA.md) §7 |
| El mapa de versiones, y por qué la v5 está fuera | [`0_mapa_versiones.md`](../spec_kit/versiones/0_mapa_versiones.md) |
| Lo que ya se hizo, contado de `git log` | [`CRONOGRAMA.md`](CRONOGRAMA.md) |
| El cambio de motor, paso a paso | [`v5_otros_motores/7_quickstart.md`](../spec_kit/versiones/v5_otros_motores/7_quickstart.md) *(local: no está en GitHub)* |

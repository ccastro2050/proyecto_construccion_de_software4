# Mapa de versiones — Construcción de Software

> La ruta completa del proyecto. Cada versión se especifica **solo cuando la
> anterior está cerrada** (commit + tag). Este mapa da la dirección; el spec
> kit de cada versión da el detalle.
>
> **Y cada versión entrega su API Y SU PANTALLA.** No hay una versión «de
> back» y otra «de front»: se construyen en paralelo, y una versión no está
> cerrada si la API responde y la pantalla no.
>
> La ruta es la que define
> [0_METODOLOGIA.md](../../../ProyectosDeAula/docs/0_METODOLOGIA.md) §2; aquí
> no se inventa nada, se ordena.

## La ruta — **cuatro versiones**

| Versión | Qué agrega (acumulativo) | Estado |
|---|---|---|
| v1 | CRUD completo de las **tablas sin clave foránea** — **API y pantallas** | **Cerrada** · tag `v1` |
| v2 | CRUD de las **tablas con clave foránea** y las puente: las FK como **listas desplegables cargadas desde la API**, y la facturación maestro-detalle — **API y pantallas** | **Cerrada** · tag `v2` |
| v3 | **JWT**, sesiones y control de acceso por roles; CRUD de `usuario`, `rol`, `rol_usuario`, `ruta` y `rutarol` solo para administradores | **Cerrada** · tag `v3` |
| **v4** | **10 consultas multitabla** (4+ tablas cada una), dashboard con gráficos, **imagen corporativa con su manual de marca**, páginas corporativas, responsive/PWA y **publicación** en un servidor | **En curso** ([spec](v4_aplicativo/2_spec.md)) |

> **Son cuatro, y no más.** Si aparece una quinta, es que algo de las cuatro
> se dejó a medias y se está aplazando.
>
> **Este repositorio está en la v4 — «el resto»**, y trae las tres anteriores
> funcionando: la regresión es obligatoria.

## El anexo: el segundo motor

Este repositorio trae además **`anexo_multimotor/`** y una implementación
completa de los repositorios contra **SQL Server**, con sus servicios en el
`docker-compose.yml`.

**Eso NO es la v4.** Es el ejercicio del segundo motor, y está aquí porque ya
estaba construido cuando el mapa pasó de seis versiones a cuatro.

| | |
|---|---|
| **Qué demuestra** | Que la interfaz del repositorio servía: se agregó una segunda implementación **sin tocar el servicio ni el controlador**. Es la inversión de dependencias, comprobada |
| **Por qué no es una versión** | Cambiar de motor **no agrega funcionalidad** al producto: agrega una implementación de la misma interfaz. El usuario del sistema no nota nada |
| **Dónde queda entonces** | Como anexo del repositorio, disponible para quien lo estudie. La v4 de verdad —el aplicativo completo— **está sin especificar** |

> **Se conservó a propósito.** Era código que funcionaba y que enseña algo
> real; tirarlo por un cambio de mapa habría sido peor que reubicarlo.

## La estrategia: back y front EN PARALELO

**Cada versión entrega su parte de la API *y* su parte del front.** Conviene
decir por qué, porque la alternativa —construir toda la API y meter el front
al final— es la que uno hace por inercia.

| | |
|---|---|
| **Lo terminado se le puede mostrar a alguien** | Una versión que solo trae endpoints se sustenta con Swagger. Una que trae pantallas se le muestra a quien la pidió |
| **El contrato se ejercita de inmediato** | Uno descubre que el JSON es incómodo **cuando le toca pintarlo**. Si el front llega tres versiones después, el contrato lleva tres versiones equivocado |
| **No hay front de golpe al final** | Es el error que se paga caro: doce entidades de API esperando un front que nace con una sola |
| **Es lo que pide el curso** | `0_METODOLOGIA.md` §2, textual: *«v1 — CRUD de las tablas sin FK del módulo — **API REST + Frontend funcionando**»* |

> **La regla operativa:** una versión **no está cerrada** si la API responde y
> la pantalla no. **Media versión no es una versión.**

### El stack del front

**Blazor Server sobre .NET 10**, en un tercer contenedor, en el puerto
**8051**. Habla con la API **solo por HTTP**: no tiene cadena de conexión, ni
driver de base de datos, ni el servicio `postgres` en su `depends_on`.

Que el front y la API estén los dos en C# **no cambia nada de eso**, y hay que
cuidarlo: la tentación de compartir una clase entre los dos proyectos existe
aquí y no existiría con dos lenguajes distintos. **No se comparte nada.**

## Qué tabla entra en qué versión

Las 12 tablas de `bdfacturas`, repartidas:

| Versión | Tablas |
|---|---|
| **v1** | `producto` · `empresa` · `persona` |
| v2 | `cliente` · `vendedor` · `factura` · `productosporfactura` |
| v3 | `rol` · `usuario` · `rol_usuario` · `ruta` · `rutarol` |

> **Ojo:** las 12 tablas **existen en la base desde la v1** (Artículo 5 de la
> [constitución](../1_constitution.md)). Lo que reparte esta tabla es qué
> puede **nombrar el código** de cada versión, no qué existe en el motor.
>
> **Y fíjese en `usuario`:** no tiene clave foránea, así que «cabría» en la
> v1. No entra, porque es la tabla del control de acceso y eso es la v3. El
> criterio de la v1 no es solo técnico — es también de alcance.

## Lo que este mapa dejó por fuera, y por qué

Una versión anterior de este mapa tenía **seis versiones** y repartía el
trabajo **por motor de base de datos**: la v4 era SQL Server, la v5 MariaDB, y
el front quedaba en la **v6**.

**Se cambió, y conviene saber qué se perdió y qué se ganó:**

| | |
|---|---|
| **El front en la v6** | Era el error que el método existe para evitar. Doce entidades de API esperando un front que nace al final, con el contrato ya equivocado tres versiones atrás |
| **Una versión por motor** | Cambiar de motor **no agrega funcionalidad**: agrega una implementación de la misma interfaz. Es un ejercicio legítimo, pero no es una versión del producto — es una variante del repositorio |
| **Seis versiones** | `0_METODOLOGIA.md` fija **cuatro**, y el calendario del semestre está armado sobre esas cuatro |

> **El multi-motor no desapareció del curso: cambió de lugar.** La fábrica de
> repositorios y la segunda implementación son un ejercicio **dentro** de la
> versión que corresponda, cuando el patrón ya esté sostenido — no una versión
> aparte. Una interfaz con dos implementaciones se demuestra en una tarde; un
> front no.

## Reglas del mapa

1. **No se anticipa nada de una versión futura** (Artículo 1 de la
   constitución).
2. **Una versión cerrada no se reabre**: los ajustes van en la siguiente.
3. **Regresión obligatoria**: al cerrar la vN, los criterios de todas las
   versiones anteriores deben seguir pasando — **incluidos los de sus
   pantallas**.
4. El repositorio siempre muestra la **versión en curso, funcionando** — con
   su API **y su pantalla**.

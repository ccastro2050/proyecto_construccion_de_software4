# La arquitectura de este software

> **Qué es este documento.** Qué piezas hay, quién habla con quién, y la
> pregunta que todo el mundo hace al ver un solo repositorio: **¿esto es un
> monolito?**
>
> Todo lo que dice está medido en este repositorio, no supuesto. Donde hay un
> número, se puede contar.
>
> Versión 1.0 · 5 de octubre de 2026.

---

## 1. Tres procesos, no uno

Levantar el sistema enciende **tres contenedores**, y cada uno es un programa
distinto que podría vivir en otra máquina:

| Contenedor | Qué es | Puerto |
|---|---|---|
| **`front-flask`** | Python + Flask (Jinja2). El único que le habla a la persona | **8040** |
| **`api-catedras`** | C# / ASP.NET Core. El único que habla con la base de datos | **8039** |
| **`postgres`** | PostgreSQL 16, con el esquema de cátedras abiertas | **15463** |

```
  Navegador  ──HTTP──▶  front-flask  ──HTTP──▶  api-catedras  ──SQL──▶  postgres
   (persona)              :8040                    :8039                 :15463
```

> **La regla que sostiene todo el diseño:** el front **nunca** toca la base de
> datos. Si necesita un dato, se lo pide a la API. Y se comprueba en un
> minuto: busque `Npgsql` o una cadena de conexión dentro de `front_flask/`.
> No hay ninguna, y no puede haberla.

---

## 2. ¿Esto es un monolito?

Es la pregunta que sale sola al ver **un solo repositorio**. Y la respuesta
corta es: **la API es un monolito, el front también, y el sistema no.**

Lo que pasa es que se están mezclando **dos cosas que no son la misma**:

| | Qué mide | Aquí |
|---|---|---|
| **MONOREPO** | cuántos **repositorios** hay | **uno** |
| **MONOLITO** | cuántas **unidades desplegables** hay | **dos propias** (el front y la API) |

> **«Monolito» se predica de una unidad desplegable, no de un repositorio.**
> Tener todo junto en GitHub se llama **monorepo**, y es una decisión sobre
> cómo se guarda el código — no sobre cómo se ejecuta.
>
> Existen monorepos con microservicios adentro, y existen veinte repositorios
> que al desplegarse son un solo proceso. Las dos dimensiones son
> independientes.

**Entonces, ¿qué es cada pieza?**

| | ¿Monolito? | Por qué |
|---|---|---|
| `api_catedras/` | **Sí** | Un `.csproj`, un proceso, un despliegue. Los diez recursos viven adentro y se despliegan juntos |
| `front_flask/` | **Sí** | Un `app.py`, un proceso, un despliegue |
| **El sistema** | **No** | Son **dos** unidades que se despliegan por separado y se hablan por HTTP |

**¿Y microservicios?** No, y conviene decir por qué no, porque es la confusión
siguiente: faltan las tres cosas que los definen — **base de datos propia por
servicio**, **descubrimiento** y **comunicación asíncrona**. Aquí hay una sola
base de datos, las direcciones están en el `docker-compose.yml`, y todo es
HTTP sincrónico.

> **Y no es un defecto.** Para un sistema de este tamaño, partirlo en
> microservicios agregaría problemas que no se tienen: transacciones
> distribuidas, consistencia eventual y un despliegue por cada servicio. Casi
> todo el software empieza siendo un monolito, y muchos nunca necesitan dejar
> de serlo.

---

## 3. Los tres niveles

La forma clásica, y conviene nombrarla con su nombre: **arquitectura de tres
niveles** (*three-tier*).

| Nivel | Quién | Qué decide |
|---|---|---|
| **1 · Presentación** | `front_flask/` — 22 plantillas, 10 recursos | **Nada.** Pregunta y obedece |
| **2 · Aplicación** | `api_catedras/` — 10 controladores, 3 capas | La forma de la petición y el flujo |
| **3 · Datos** | `postgres` — 37 tablas | La integridad: claves, restricciones y `CHECK` |

**Y el nivel 3 no solo guarda.** El esquema de cátedras abiertas trae
restricciones que **defienden las reglas del negocio** aunque la API se
equivoque: `uq_sesion_reunion` impide dos reuniones con el mismo número en una
cátedra, `chk_sesion_fechas` impide que una sesión termine antes de empezar, y
`chk_catedra_asis` exige que el código del evento tenga nueve dígitos.

> **Eso importa para la sustentación:** si alguien pregunta «¿y si la API tiene
> un error y manda una fecha de fin anterior a la de inicio?», la respuesta no
> es «confiamos en la API». Es **la base de datos la rechaza**, y se puede
> demostrar intentándolo por `psql`.

---

## 4. Las tres capas de la API, que no son los tres niveles

Se confunden siempre, así que vale separarlas: los **niveles** son procesos;
las **capas** son la organización de uno de ellos.

```
  Controlador  ──▶  Servicio  ──▶  Repositorio  ──▶  la base de datos
   (HTTP)           (reglas)       (SQL)
```

| Carpeta | Qué hace | Qué tiene PROHIBIDO |
|---|---|---|
| `Controllers/` | Traduce HTTP ↔ negocio. Lee la petición, devuelve el código | Saber SQL |
| `Servicios/` | Las reglas de flujo | Saber qué es un 404 |
| `Repositorios/` | El SQL, y nada más | Decidir reglas de negocio |

**Cada repositorio está detrás de una interfaz**, y eso es lo que hace que el
servicio no sepa qué motor hay debajo:

```csharp
builder.Services.AddScoped<IRepositorioSede, RepositorioSedePostgreSql>();
```

> **Fíjese en el nombre de la clase: `RepositorioSedePostgreSql`.** El sufijo
> está puesto a propósito, porque anticipa lo que viene: el día que se agregue
> un segundo motor, aparece `RepositorioSedeSqlServer` al lado y **el servicio
> no se entera** — solo cambia esa línea del registro.

---

## 5. El front también tiene capas

Y es la parte que más sorprende, porque un front en Flask parece «una sola
cosa». No lo es:

| Archivo | Qué es | Su equivalente en la API |
|---|---|---|
| `app.py` | Las rutas: qué pantalla responde a cada dirección | El **controlador** |
| `cliente_api.py` | **La única pieza que habla HTTP.** Traduce cada respuesta a `(ok, datos, errores)` | El **repositorio** |
| `templates/` | Lo que la persona ve | La vista |

> **`cliente_api.py` es al front lo que el repositorio es al back:** la única
> pieza que sabe **dónde viven los datos**. Las plantillas no saben que existe
> un 422; reciben `errores` y los pintan.
>
> **Y por eso el front se puede probar sin la API**, igual que el servicio se
> puede probar sin la base de datos: se reemplaza esa pieza por una falsa.

---

## 6. El viaje de un clic

Guardar una sede, de punta a punta:

| # | Dónde | Qué pasa |
|---|---|---|
| 1 | Navegador | Se envía el formulario a `POST /sedes/nuevo` |
| 2 | `app.py` | La ruta de Flask recibe el formulario |
| 3 | `cliente_api.py` | Lo convierte en `POST http://api-catedras:8039/api/sede` |
| 4 | `SedeController` | Valida la **forma**: si falta algo, **422** y no sigue |
| 5 | `ServicioSede` | Las reglas. No sabe que existe un 404 |
| 6 | `RepositorioSedePostgreSql` | El `INSERT` |
| 7 | `postgres` | Las restricciones. Si la clave se repite, **rechaza** |

Y de vuelta, el mismo camino al revés: el repositorio devuelve la fila, el
controlador arma el sobre, `cliente_api.py` lo traduce a `(ok, datos, errores)`
y la plantilla lo pinta.

**El sobre, que es el contrato entre los dos procesos:**

```json
{ "tabla": "sede", "limite": 100, "total": 3, "datos": [ ... ] }
```

> **No es una lista pelada, y es a propósito.** Un arreglo suelto no puede
> decir cuántos hay en total ni de qué tabla viene. Leer mal este sobre deja la
> pantalla **vacía y sin ningún error**, que es el defecto más difícil de ver.

---

## 7. Lo que se puede comprobar en cinco minutos

Esto es lo que vale en una sustentación: no la explicación, sino la prueba.

| Afirmación | Cómo se comprueba |
|---|---|
| Son tres procesos | `docker compose ps` muestra tres contenedores |
| El front no toca la base de datos | Buscar `Npgsql` o una cadena de conexión en `front_flask/`: no hay |
| Son dos unidades desplegables | Apagar la API: el front **responde** y muestra el error. Son procesos distintos |
| La base defiende sus reglas | Intentar por `psql` una sesión con `fin` anterior a `inicio`: la rechaza |
| El servicio no sabe de SQL | Buscar `SELECT` en `Servicios/`: no hay |

---

| Qué | Dónde |
|---|---|
| Las capas y sus prohibiciones | [`SOLID_CAPAS_PATRONES.md`](SOLID_CAPAS_PATRONES.md) |
| El viaje de una petición, en detalle | [`FLUJO_DE_UNA_PETICION.md`](FLUJO_DE_UNA_PETICION.md) |
| Qué es cada contenedor | [`CONCEPTOS_DOCKER.md`](CONCEPTOS_DOCKER.md) |
| El esquema y sus restricciones | [`../dominio/DISENO_BD.md`](../dominio/DISENO_BD.md) |

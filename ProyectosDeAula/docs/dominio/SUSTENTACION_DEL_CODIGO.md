# Sustentación del código — diez preguntas, respondidas

El modelo de datos de este proyecto ya tiene la suya:
el documento de sustentación del modelo relacional recibido *(ya no versionado)*
responde **veinte preguntas** sobre por qué el esquema quedó como quedó. Este
documento es el otro lado: **diez preguntas sobre el código**, las mismas que
se le hacen a un estudiante en la evaluación individual, respondidas aquí con
archivo y línea.

> **Por qué existe.** Una respuesta correcta pero genérica no vale. «El
> servicio no sabe de HTTP» lo dice cualquiera; lo que se pide es **señalar
> dónde se ve, y qué se rompería si no fuera así**. Todo lo que sigue se puede
> comprobar abriendo los archivos que se citan o corriendo los comandos que
> aparecen.

---

## 1 · ¿Qué fase costó más, y dónde está el rastro?

El `git log` de este repositorio no es el de una jornada de dos horas: es el de
un proyecto que creció por versiones. El rastro por fases está en los dos
planes — [`PLAN_V1.md`](PLAN_V1.md) §4, «El plan, en 10 pasos», y
[`PLAN_V2.md`](PLAN_V2.md) §4 —, y cada uno dedica una sección entera a lo
que **obligó a cambiar el plan**:

| Plan | La sección que cuenta el tropiezo |
|---|---|
| v1 | §2 «El primer hallazgo, y el que cambió el plan» |
| v2 | §2 «Lo primero que hubo que contar bien» |

**Lo que hay que poder defender:** un plan sin sección de hallazgos casi
siempre se escribió al final, para justificar lo hecho. Estos dos la tienen
porque se escribieron antes y se corrigieron después.

---

## 2 · ¿Dónde quedó una ambigüedad del material y cómo se resolvió?

Aquí **no queda ningún `[NECESITA ACLARACIÓN]` sin resolver** —
`9_checklist.md` lo exige para
firmar. Pero eso no significa que no las hubiera: significa que **se
resolvieron, y la decisión quedó escrita**, en
`4_research.md`, con su alternativa
descartada.

La más instructiva es **`D-v1-6` — «El nombre repetido lo defiende la BASE, no
la API»**. La pregunta era: si dos sedes no pueden llamarse igual, ¿quién lo
impide? Podía ser la API (comprobar antes de insertar) o la base de datos (una
restricción). Se decidió que la base de datos:

```sql
CONSTRAINT uq_sede_nombre UNIQUE (nombre)   -- db/init.sql, línea 513
```

**Por qué importa la decisión:** si lo comprueba la API, entre el «¿existe?» y
el «insertar» cabe otra petición, y las dos pasan. La base de datos no tiene esa
ventana. La API comprueba para **dar un buen mensaje**; la base de datos garantiza.

---

## 3 · ¿Qué es una compuerta, y qué pasa si se salta la segunda?

Una **compuerta** es un punto donde el trabajo no avanza hasta que algo se
revisa. Este proyecto tiene tres:

| Compuerta | Dónde | Qué revisa |
|---|---|---|
| 1 | `2_spec.md` | Que las ambigüedades estén detectadas **antes de planear** |
| 2 | `3_plan.md` | Que cada artículo de la constitución se revise **antes de programar** |
| 3 | `9_checklist.md` | Se firma **antes** de escribir la primera línea de código |

**Si se salta la segunda**, se programa contra un plan que nadie contrastó con
las reglas permanentes. El daño no se ve el primer día: se ve cuando ya hay
código escrito y alguien nota que viola un artículo — y entonces corregir
cuesta lo que no costaba antes de empezar.

**¿Por qué la constitución es una sola para todas las versiones?** Porque es lo
**permanente**: si cambiara con cada versión, no sería una restricción, sería
una preferencia. Lo que cambia por versión es el `2_spec` — qué se construye
ahora —, no las reglas de cómo se construye. Una constitución por versión es
una forma elegante de no tener constitución.

---

## 4 · ¿Qué no puede aparecer nunca en la capa de servicio, y quién decide el 404?

**Lo que no puede aparecer:** nada de HTTP —ni `IActionResult`, ni códigos de
estado, ni `HttpContext`— y nada de SQL. El servicio conoce **reglas de
negocio**, y habla con la base de datos a través de una interfaz.

Compruébelo, y no crea en la palabra:

```powershell
# Cuenta cuántas veces el servicio nombra algo de HTTP. Tiene que dar 0.
Select-String -Path api_catedras/Servicios/ServicioSede.cs `
              -Pattern "HttpContext|IActionResult|StatusCode|NotFound" | Measure-Object
```

**Quién decide entonces que algo es un 404:** el controlador, traduciendo una
excepción del negocio.
`api_catedras/Controllers/SedeController.cs`,
**líneas 68, 125, 150 y 173**:

```csharp
catch (NoEncontradoExcepcion ex)
```

El servicio lanza `NoEncontradoExcepcion` —que no sabe que existe el número
404— y el controlador la convierte. Si mañana este mismo servicio se usara
desde una aplicación de escritorio, **no habría nada que cambiarle**.

---

## 5 · Si mañana la base de datos fuera SQL Server, ¿qué cambiaría?

Esta es la pregunta donde hay que ser honesto, porque la respuesta bonita no es
la verdadera.

**Lo que cambiaría, como está previsto:**

| Qué | Cuántos archivos |
|---|---|
| Los repositorios `Repositorio*PostgreSql.cs` | 10 |
| El registro de dependencias en `Program.cs` | 1 archivo, 10 líneas |
| El paquete `Npgsql` en `ApiCatedras.csproj` | 1 |

**Lo que NO cambiaría, y el principio que lo permite:** los modelos, las
peticiones, las interfaces `IRepositorio*` y **los diez servicios**. Ninguno
nombra PostgreSQL. Eso es **inversión de dependencias** (la D de SOLID): el
servicio depende de `IRepositorioSede`, no de quien la implementa.

**Y lo que también cambiaría, aunque no debería:** los **diez controladores**.
Los diez capturan `NpgsqlException` para convertir un error del motor en un
500:

```csharp
using Npgsql;                      // SedeController.cs, línea 7
...
catch (NpgsqlException ex)         // líneas 50, 72, 96, 129, 154, 177
```

Está hecho a propósito y documentado en la cabecera del archivo —ahí caen las
defensas de la base de datos—, pero el efecto es real: **la capa que no debería saber de
motores, sabe**. Cambiar a SQL Server obligaría a tocar `SqlException` en los
diez. La salida limpia sería una excepción propia —`ErrorDeAlmacenamiento`—
lanzada por el repositorio, que es la única capa a la que le corresponde saber
de motores.

**Se deja dicho porque es lo que un examen debería premiar:** reconocer la
grieta en el propio código vale más que recitar el principio.

---

## 6 · `Sede` y `SedeCrear` son clases distintas. ¿Qué tiene una que la otra no?

| Clase | Para qué | Qué la distingue |
|---|---|---|
| `Modelos/Sede.cs` | Lo que el sistema **es** | No tiene `Activo`, que sí está en la tabla: el borrado lógico es del motor, no del dominio |
| `Peticiones/SedeCrear.cs` | El cuerpo del **POST** | Lleva `[Required]` y `[MaxLength]`, y `EsVirtual` es `bool?` |
| `Peticiones/SedeReemplazo.cs` | El cuerpo del **PUT** | **No tiene `IdSede`**: la llave va en la URL, no en el cuerpo |

**El detalle que más se pasa por alto:** `EsVirtual` es `bool?` —admite nulo—
en la petición, y `bool` a secas en el modelo. No es un descuido: con `bool`,
un cuerpo que **no trae** el campo llega como `false`, y el sistema no puede
distinguir «dijo que no» de «no dijo nada». Con `bool?` llega nulo, y el
`[Required]` lo rechaza con 422.

**Qué se rompería usando el modelo como cuerpo del POST:** el cliente podría
mandar `activo` —si el modelo lo tuviera— y darse de alta una sede ya borrada;
y sin los `[Required]`, un cuerpo vacío llegaría hasta la base de datos para que sea
ella quien falle, con un 500 en vez de un 422. El error dejaría de ser «usted
mandó mal el cuerpo» y pasaría a ser «algo se rompió adentro».

---

## 7 · El mismo cuerpo, dos respuestas: 422 en PUT y 200 en PATCH

Este cuerpo **no trae `nombre`**:

```json
{ "idSede": "BELLO", "direccion": "Calle 45 61-40, Bello", "esVirtual": false }
```

Corrido contra el sistema levantado:

```
PUT   /api/sede/BELLO  ->  422
PATCH /api/sede/BELLO  ->  200
```

**Dónde se decide, en cada uno:**

| Verbo | Archivo | Línea | Qué hay ahí |
|---|---|---|---|
| PUT | `Peticiones/SedeReemplazo.cs` | 12–13 | `[Required]` sobre `Nombre` |
| PATCH | `Peticiones/SedeActualizar.cs` | 13–15 | Los tres campos son anulables, y ninguno lleva `[Required]` |

**Y la razón, que no es un capricho:** `PUT` **reemplaza** el recurso entero —
si no manda `nombre`, está pidiendo dejarlo vacío, y eso se rechaza. `PATCH`
**modifica lo que venga** — si no manda `nombre`, está diciendo «ese no lo
toque». El mismo cuerpo significa dos cosas distintas según el verbo, y por eso
son dos clases y no una con campos opcionales.

Note además que **ninguna de las dos decisiones está en el controlador**: las
toma el framework al validar el cuerpo, **antes** de entrar. Por eso el 422 no
aparece escrito en `SedeController.cs`.

---

## 8 · Una línea con `await`: ¿qué hace el hilo en ese instante?

`api_catedras/Servicios/ServicioSede.cs`,
**línea 40**:

```csharp
return await _repositorio.ObtenerTodos(limite);
```

Mientras la base de datos responde, **el hilo no espera: se va**. Vuelve al conjunto de
hilos disponibles y atiende **otra petición**. Cuando PostgreSQL contesta, la
ejecución se retoma —puede que en otro hilo— justo donde quedó.

**Qué habría pasado con `.Result`:** no responda «se bloquea». Diga a quién
afecta y cuándo se nota.

- **A quién afecta:** no al que hizo esa petición, que tarda lo mismo. Afecta a
  **los demás**. El hilo se queda retenido sin hacer nada, y es un hilo menos
  para atender a otro usuario.
- **Cuándo se nota:** no en su máquina, con usted solo probando — ahí todo va
  igual de rápido, y por eso se cuela. Se nota **con concurrencia**: cuando hay
  más peticiones simultáneas que hilos disponibles, las nuevas hacen fila. La
  API no se cae; se vuelve lenta, y el motivo no aparece en ningún log.

---

## 9 · Sin transacciones escritas, ¿qué garantiza ya la base de datos?

En la v1 **nadie escribió un `BEGIN TRANSACTION`**, y sin embargo la base de datos ya
garantiza. De `db/init.sql`, líneas 505–514:

```sql
CREATE TABLE sede (
    id_sede     varchar(15)   NOT NULL,
    nombre      varchar(80)   NOT NULL,
    ...
    CONSTRAINT pk_sede        PRIMARY KEY (id_sede),
    CONSTRAINT uq_sede_nombre UNIQUE (nombre)
);
```

**Dos garantías que ya están:**

1. **Atomicidad de cada sentencia.** Un `INSERT` que viole `uq_sede_nombre` no
   deja media fila: no deja nada. Cada sentencia suelta ya es una transacción,
   aunque nadie la haya escrito.
2. **Consistencia por restricción.** `pk_sede` impide dos sedes con el mismo
   código y `uq_sede_nombre` dos con el mismo nombre — **siempre**, venga la
   escritura de la API, de `psql` o de pgAdmin. Una comprobación en la API solo
   protege a quien pasa por la API.

**Una operación futura que sí necesitaría transacción explícita:** guardar una
encuesta respondida — una fila en `respuesta_encuesta` y sus varias en
`respuesta_item`.

**Qué se dañaría sin ella:** quedaría una encuesta respondida **sin ninguna
respuesta**, y los informes contarían una participación que no existe. Y no se
arregla con una clave foránea: la FK garantiza que lo que se inserte exista,
**no que se inserte algo**. Está respondido en la pregunta 6 de la sustentación
del modelo — es la misma grieta, vista desde el código.

---

## 10 · La prueba hueca

Una **prueba hueca** ejecuta el método y **no comprueba nada**: pasa aunque el
código esté mal, y sube la cobertura exactamente igual que una buena.

De `api_catedras/pruebas/Programa.cs`,
**línea 216**:

```csharp
Revisar(!(await sedes.ObtenerTodos(1000)).Any(),
        "El sistema arranca vacío: sin sedes.",
        "Arrancó con filas que nadie creó.");
```

**La línea que la salva de ser hueca es esa misma.** Sin el `Revisar`, quedaría
`await sedes.ObtenerTodos(1000);` — se llama al servicio, se recorre el
repositorio, se ejecuta todo… y nadie mira el resultado.

**¿Seguiría pasando si se borrara?** Sí. Pasaría siempre — incluso si
`ObtenerTodos` devolviera basura. Y ahí está lo que dice sobre la cobertura:

> **La cobertura mide qué líneas se ejecutaron, no qué se comprobó.** Una
> prueba hueca recorre el mismo código que una buena y suma los mismos puntos.
> Por eso un 90 % de cobertura no significa nada por sí solo: la pregunta no es
> cuánto código se tocó, sino **cuántas de esas líneas pueden fallar**.

Y aquí hay un detalle que vale más que la respuesta: `Revisar` **no detiene el
programa**. Anota el fallo, deja correr las demás comprobaciones y `Main`
termina devolviendo **1**. Ese número es lo único que mira quien corre la
prueba en automático. Sin él, la prueba contaría el problema por pantalla y
nadie se enteraría — que es una segunda forma de ser hueca, menos conocida y
más peligrosa, porque esta sí comprueba.

---

## Cómo se usa este documento

- **Antes de una evaluación:** léalo con los archivos abiertos al lado. Si una
  respuesta no la puede señalar en el código, todavía no la sabe.
- **Al responder:** nunca cite este documento. Cite **su** archivo y **su**
  línea. Una respuesta correcta pero prestada no puntúa, y se nota.
- **Si encuentra que algo de aquí ya no es cierto** —porque el código
  cambió—, eso es un hallazgo, no un error suyo: dígalo. La pregunta 5 existe
  precisamente porque alguien miró el código en vez de repetir el principio.

---

## Referencias internas

| Documento | Qué aporta |
|---|---|
| el documento de sustentación del modelo relacional recibido *(ya no versionado)* | Las veinte preguntas del **modelo de datos** |
| [`SOLID_CAPAS_PATRONES.md`](../conceptos/SOLID_CAPAS_PATRONES.md) | Las preguntas 4 y 5, con los cinco principios completos |
| [`PARADIGMA_POO.md`](../conceptos/PARADIGMA_POO.md) | La pregunta 6 |
| [`PROGRAMACION_ASINCRONICA.md`](../conceptos/PROGRAMACION_ASINCRONICA.md) | La pregunta 8 |
| [`PRINCIPIOS_ACID.md`](../conceptos/PRINCIPIOS_ACID.md) | La pregunta 9 |
| [`PRUEBAS_Y_CALIDAD_DE_PRUEBAS.md`](../conceptos/PRUEBAS_Y_CALIDAD_DE_PRUEBAS.md) | La pregunta 10 |
| [`SDD_SPECKIT.md`](../conceptos/SDD_SPECKIT.md) | Las preguntas 2 y 3 |

# Diseño de base de datos

> **Qué es este documento.** El **modelo de datos del proyecto**, en las cuatro
> etapas que [`CONCEPTOS_DISENO_BD.md`](../conceptos/CONCEPTOS_DISENO_BD.md)
> define: conceptual → **selección del motor** → lógico → físico.
>
> **Qué NO es.** No es el script. `db/init.sql` es la
> **salida** de la etapa física, no su sustituto: un script dice *qué* quedó,
> no *por qué*.
>
> **Para qué sirve.** Es el insumo de la **tanda 2** del
> `PROMPT_GENERAR_SPEC_KIT.md`, la que genera
> `5_data_model` — **recortado a las tablas de cada versión**.

---

## 0. De dónde salió este modelo, y por qué importa decirlo

**En este proyecto el diseño vino dado.** No se elicitó ni se construyó aquí:
se recibió ya hecho, como parte del material de entrada
([`FUENTES.md`](FUENTES.md)).

**Eso es una anomalía, no el caso normal.** Se dice por dos razones:

| | Por qué se declara |
|---|---|
| **Honestidad de la traza** | Quien lea las 37 tablas podría suponer que salieron de las entrevistas. No salieron. Las entrevistas **confirmaron** el modelo y le encontraron huecos; no lo produjeron |
| **Utilidad de la metodología** | Este documento está escrito como **se produciría** un diseño, no como se recibió éste, para que sirva cuando no venga dado — que es lo habitual |

> **Lo que sí se hizo aquí, y se puede sustentar:** medir las dependencias
> funcionales contra los datos reales, comprobar que el script corre en una
> base vacía, verificar que cada restricción **rechaza de verdad** lo que dice
> rechazar, y amarrar cada tabla a la regla de negocio que la justifica.

---

## 1. Etapa conceptual — el modelo entidad-relación

**Independiente de cualquier motor.** Aquí solo hay entidades, atributos y
relaciones, en la notación de Chen.

### Los siete grupos de entidades

| Grupo | Entidades | De qué responde |
|---|---|---|
| **La persona** | `asistente` · `documento_asistente` · `vinculacion_asistente` · `consentimiento_datos` | Quién asiste, cómo se identifica y qué autorizó |
| **Lo académico** | `facultad` · `programa_academico` · `periodo_academico` · `programa_asistente` · `alias_programa` | A qué programa pertenece, y **cómo se llamaba ese programa en el archivo que llegó** |
| **El evento** | `catedra` · `sesion` · `ponencia` · `tipo_evento` · `sede` · `modalidad` · `dependencia` | Qué se dicta, cuándo, dónde y de qué modo |
| **El registro** | `registro_asistencia` · `enlace_registro` · `clave_acceso` | La asistencia efectiva, y por qué medio se tomó |
| **La encuesta** | `encuesta` · `pregunta` · `tipo_pregunta` · `opcion_pregunta` · `respuesta_encuesta` · `respuesta_item` | La percepción, separada del hecho de asistir |
| **La carga** | `lote_carga_asistente` · `novedad_carga` · `lote_migracion` · `detalle_migracion` · `estado_proceso` | De dónde vino cada fila y qué pasó al cargarla |
| **El sistema** | `usuario` · `rol` · `rol_por_usuario` · `parametro` · `bitacora` | Quién entra, con qué permiso, y qué tocó |

### Las tres decisiones conceptuales que no son obvias

| | Decisión | Por qué |
|---|---|---|
| **1** | **El documento de identidad es una entidad aparte**, no un atributo de `asistente` | Una persona puede cambiar de tipo de documento, y aparece con dos documentos distintos en archivos distintos. Como atributo, el segundo documento obliga a **borrar el primero**; como entidad, los dos conviven y **se puede ver el conflicto** |
| **2** | **`programa_asistente` es una entidad, no una llave foránea** | La relación persona ↔ programa **tiene fecha**. Sin entidad propia no hay dónde poner *desde cuándo* |
| **3** | **`alias_programa` existe porque el dato real llega sucio** | El mismo programa aparece escrito de varias formas en los archivos recibidos. La alternativa era limpiar antes de cargar, y eso **pierde el original**: no se puede auditar lo que se corrigió |

> **Esta etapa no menciona `varchar`, ni índices, ni PostgreSQL.** Si aparece
> algo de eso aquí, la etapa está contaminada: se decidió con el motor a la
> vista antes de saber qué se estaba modelando.

---

## 2. Etapa de selección del motor — **va aquí, no después**

**Esta etapa se mete entre el conceptual y el lógico, y casi todos los cursos
la saltan.** La razón es que el modelo **lógico depende del MODELO DE DATOS —
relacional, documental, de grafos—, no del producto**. Y el modelo de datos es
justamente lo que se elige aquí.

### Qué se decidió

| | |
|---|---|
| **Modelo de datos** | **Relacional** |
| **Motor** | **PostgreSQL 17** |

### Por qué relacional, y qué se descartó

| Alternativa | Por qué no |
|---|---|
| **Documental** (MongoDB y similares) | Los datos son **fuertemente relacionales**: una asistencia amarra persona, sesión, programa y medio de registro. En documental eso se resuelve duplicando o resolviendo en la aplicación, y la regla «una persona se registra **a lo sumo una vez** por sesión» **deja de ser del motor** |
| **De grafos** | Las consultas del proyecto son agregaciones —cuántos asistieron, de qué programa—, no recorridos de camino. Un grafo resuelve bien lo que aquí no se pregunta |
| **Hoja de cálculo** | Es de donde se viene, y es el problema que se está resolviendo |

### Por qué PostgreSQL, y qué se descartó

| | Por qué |
|---|---|
| **A favor** | `CHECK` con expresiones completas · tipos nativos que aquí se usan (`timestamptz`, `inet`, `boolean`) · `GENERATED ALWAYS AS IDENTITY` estándar · sin licencia |
| **MySQL / MariaDB** | Se descartó por el `CHECK`: en el proyecto **47 restricciones** viven en el motor. Un motor que los ignore silenciosamente convierte esas 47 en 47 huecos que nadie ve |
| **SQL Server / Oracle** | Capaces de sobra, pero la licencia no se justifica y **el curso tiene que poder levantarlo en un contenedor** |

> **El costo declarado:** el modelo queda **atado a PostgreSQL** en las 47
> `CHECK`, las 16 funciones y los 7 disparadores. Portarlo a otro motor no es
> cambiar una cadena de conexión: es reescribir esa lógica. **Se aceptó**, y
> queda escrito aquí para que quien lo herede no se sorprenda.

---

## 3. Etapa lógica — el esquema relacional

### 3.1 · La transformación, que es mecánica

Del conceptual al relacional **se aplican reglas, no criterio**: cada entidad
es una tabla, cada relación 1:N es una llave foránea del lado N, y cada N:M es
una tabla puente. Las puente de este modelo son `programa_asistente`,
`rol_por_usuario`, `opcion_pregunta` y `respuesta_item`.

### 3.2 · Normalización: **las dependencias se miden, no se suponen**

**Este es el paso que los cursos cuentan mal.** Una dependencia funcional no se
declara mirando el diagrama: se comprueba contra los datos.

```sql
-- ¿el código de programa determina el nombre del programa?
-- Si esto devuelve una sola fila, hay dos nombres para un código,
-- y la dependencia que usted suponía NO se cumple.
SELECT codigo, COUNT(DISTINCT nombre)
FROM   programa_academico
GROUP  BY codigo
HAVING COUNT(DISTINCT nombre) > 1;
```

> **Un `GROUP BY … HAVING COUNT(*) > 1` sobre los datos reales destapa en un
> minuto lo que una reunión no destapa en una hora.** Así apareció
> `alias_programa`: el código **no** determinaba el nombre, porque el nombre
> venía escrito de varias formas.

**El esquema está en 3FN**, con **una desnormalización declarada**:

| Dónde | Qué | Por qué se aceptó |
|---|---|---|
| `registro_asistencia.fk_programa_snapshot` y `fk_vinculacion_snapshot` *(de `RN-22`)* | Guardan el programa y la vinculación **del momento del registro**, duplicando lo que ya está en las tablas de la persona | **No es redundancia: es un hecho histórico distinto.** Si alguien cambia de programa en marzo, la asistencia de febrero siguió siendo de su programa de febrero. Sin el *snapshot*, un cambio de hoy **reescribiría el pasado** |

### 3.3 · Las diez tablas sin llave foránea — **y por qué son las primeras**

**De las 37, diez no dependen de ninguna otra**: se pueden llenar sin que
exista nada más. Por eso son la **v1**.

`tipo_documento` · `tipo_vinculacion` · `periodo_academico` · `tipo_evento` ·
`sede` · `modalidad` · `encuesta` · `tipo_pregunta` · `estado_proceso` · `rol`

> **`encuesta` es la más distinta de las diez**, y por eso vale: su llave la
> genera el motor, tiene dos fechas de vigencia y un número de versión. Las
> otras nueve se parecen entre sí lo suficiente como para que aparezca la
> pregunta *«¿esto se hace diez veces o se hace uno solo?»* — que es el
> contenido de `D-v1-1`.

El reparto completo por versiones está en
`0_mapa_versiones.md`.

---

## 4. Etapa física — lo que quedó en el motor

**Medido sobre `db/init.sql`:**

| Objeto | Cuántos |
|---|---|
| Tablas | **37** |
| Claves foráneas | **52** |
| Restricciones `CHECK` | **47** |
| Restricciones `UNIQUE` | **23** |
| Índices | **35** |
| Funciones | **16** |
| Disparadores | **7** |
| Vistas | **6** |
| Comentarios `COMMENT ON` | **73** |

### Qué defiende el motor, y qué no

**La regla de negocio se defiende donde no se pueda esquivar.** El criterio de
reparto:

| Se defiende en… | Cuándo | Ejemplo |
|---|---|---|
| **El motor** | La regla es **estructural**: no depende de quién ni de cuándo | `RN-21` — una persona se registra a lo sumo una vez por sesión → `UNIQUE (fk_sesion, fk_asistente)` |
| **El motor, con disparador** | Depende de **otras filas** | Las que necesitan mirar más de una tabla para decidir |
| **El servicio** | Depende del **caso de uso, del rol o del momento** | Una ventana de tiempo para registrarse: el motor no sabe qué hora es «tarde» para este evento |

> **Lo que no puede pasar es que una regla no esté en ninguno de los tres.**
> Las 38 reglas, con dónde se defiende cada una, están en
> [`REGLAS_DE_NEGOCIO.md`](REGLAS_DE_NEGOCIO.md).

### Cómo se comprueba que el diseño quedó bien

No se comprueba leyéndolo. Se comprueba **corriéndolo**:

| | Qué se hace | Qué prueba |
|---|---|---|
| **1** | El script corre **en una base vacía**, de principio a fin | Que no hay dependencias de orden escondidas ni objetos que ya existían en la máquina de quien lo escribió |
| **2** | Cada restricción se prueba **insertando lo que debe rechazar** | Que la restricción **rechaza de verdad**. Una `CHECK` mal escrita se ve idéntica a una bien escrita hasta que se prueba |
| **3** | Cada tabla se rastrea hasta una **entidad del conceptual** | Que no se coló ninguna tabla «porque hacía falta», sin modelo detrás |
| **4** | Cada regla de negocio se rastrea hasta **dónde se defiende** | Que ninguna quedó en el aire |

---

## 5. Lo que este documento **no** decide

| | Dónde va |
|---|---|
| **Qué tablas toca cada versión** | `0_mapa_versiones.md`, y el recorte de cada una en su `5_data_model` |
| **Las filas sembradas y de dónde salieron** | [`DATOS_DE_PRUEBA.md`](DATOS_DE_PRUEBA.md) |
| **Cómo responde la API cuando el motor rechaza algo** | [`POLITICA_DE_ERRORES.md`](POLITICA_DE_ERRORES.md) |
| **En qué capa vive el acceso a datos** | [`ARQUITECTURA.md`](ARQUITECTURA.md) |

---

## 6. Referencias

Las fuentes que sostienen las cuatro etapas, la normalización y la medición de
dependencias están en
[`CONCEPTOS_DISENO_BD.md`](../conceptos/CONCEPTOS_DISENO_BD.md) §9.

# Glosario — Cátedras Abiertas

> **Qué es este archivo.** El vocabulario de este proyecto: cada concepto con
> **una** designación, su definición, **de dónde salió**, y los sinónimos que
> **no** se usan.
>
> **Cómo se hace uno y por qué:** [`CONCEPTOS_GLOSARIO.md`](../conceptos/CONCEPTOS_GLOSARIO.md).
>
> **Regla del proyecto.** Si un término está aquí con estado **acordado**, se
> escribe así en la conversación, en la historia, en el requisito, en la tabla,
> en la clase y en la URL. Sin sinónimos y sin variantes.
>
> Versión 1.0 · 14 de septiembre de 2026.

---

## 0. Antes de la lista: lo que está SIN DECIDIR

Un glosario que solo resuelve lo fácil no sirve.

**«Sin decidir» no quiere decir que alguien esté discutiendo.** Quiere decir
que hay **dos nombres para la misma cosa y nadie con autoridad ha dicho cuál
queda**. Cada entrada de abajo dice, por eso, las cuatro cosas que mueven el
asunto: **qué está sin decidir · quién firma · qué bloquea · qué se escribe
mientras tanto**.

Se deja escrito, y no se resuelve por el camino fácil.

### D-GLO-1 · Cómo se llama el rol principal

| | |
|---|---|
| **Estado** | 🔴 **SIN DECIDIR** |
| **Quién firma** | El profesor **el usuario experto**, como usuario experto |
| **Qué bloquea** | Las **10 historias de usuario**, la tabla `rol` y el modelo. Hay que corregir los tres **a la vez** |
| **Mientras tanto** | Los documentos siguen diciendo «administrador». **Esta entrada es la constancia de que eso está pendiente, no de que se aceptó** |

**La evidencia, de los dos lados:**

> *«si dejamos eso al administrador, **el administrador es la unidad de
> tecnología**, y la unidad de tecnología no debe estar subiendo cátedras ni
> descargando archivos del ASIS, nada de esas cosas»* — `1:44:28`
>
> *«Creo que es como todo eso, **es como el líder**»* — `1:44:46`
>
> *«sería la persona encargada de la cátedra, o sea, la persona que tiene en su
> rol el tema de cátedra, que puede ser la persona asignada por el CIDEH, o en
> este caso también puedo ser yo»* — el usuario experto, `1:43:46`

**Y lo que quedó escrito, contado:**

| Dónde | «administrador» | «líder» |
|---|---|---|
| Las diez historias de usuario | **21 veces** | **0** |
| La tabla `rol` | `Administrador` | — |

**El nombre que la fuente corrigió es el que ganó por inercia.**

**Lo que se recomienda, y es solo una recomendación:** `líder de cátedra`. Es
el término que el experto usó al corregirse, no colisiona con el significado
que «administrador» ya tiene en la Universidad, y deja libre `administrador`
para lo que de verdad es: la unidad de tecnología.

**Mientras no se decida**, los documentos siguen diciendo «administrador» y
**esta entrada es la constancia de que eso está pendiente**, no de que se
aceptó.

---

### D-GLO-2 · ¿`ponente` o `expositor`?

| | |
|---|---|
| **Estado** | 🟡 **SIN DECIDIR** — la destapó el propio auditor |
| **Quién firma** | El profesor **el usuario experto** |
| **Qué bloquea** | La historia propuesta **12**, y el valor `PONENTE` de `ponencia.rol` |
| **Mientras tanto** | Los documentos dicen `ponente`, y el auditor **no marca** `expositor` como error. Marcarlo sería decidir por el experto |

**No la había visto yo: la encontró la comprobación.** Al buscar los términos
prohibidos, `expositor` apareció **10 veces** — y no en documentos viejos, sino
en el análisis de la elicitación:

> *«si el profesor está exponiendo, pues el **expositor**… lo que hace es que se
> le pasa el listado»*

Y el documento de respuestas tiene una sección entera titulada
**«El expositor, un actor que no está en ninguna historia»**, que es de donde
salió la historia propuesta 12.

**O sea:** el usuario experto dice **expositor**; el modelo de datos dice
**`PONENTE`**. Es el mismo patrón exacto de D-GLO-1 —la palabra del sistema le
ganó a la palabra de la fuente—, solo que con menos ruido.

**Lo que se recomienda:** no decidir por comodidad. `ponente` es el término de
la academia y `expositor` es el que usa quien va a operar el sistema. **La
misma pregunta que D-GLO-1, y conviene hacerlas juntas.**

---

## 1. La homonimia que hay que resolver: **`rol`** significa dos cosas

No es una discusión de nombres: son **dos conceptos distintos con la misma
designación, en el mismo sistema**.

| | **`rol` de sistema** | **`rol` en un evento** |
|---|---|---|
| Qué es | Qué puede **hacer** una persona en el software | Qué **función cumple** en una sesión |
| Dónde vive | Tabla `rol` + `rol_por_usuario` | Columna `ponencia.rol` |
| Valores | `ADMIN`, `COORDINADOR`, `CONSULTA` | `PONENTE`, `MODERADOR`, `INVITADO`, `ORGANIZADOR` |
| Quién lo tiene | Un `usuario` del sistema | Un `asistente` |

**Se recomienda desambiguar así**, y queda pendiente de la misma firma que
D-GLO-1:

- `rol` → **`rol de acceso`** (el del sistema, que llega en la v3)
- `ponencia.rol` → **`participación`** (la función en la sesión)

> **Por qué importa de verdad:** un asistente puede ser `PONENTE` en una sesión
> y no tener **ningún** rol de acceso al software. Con el mismo nombre para las
> dos cosas, esa frase suena a contradicción y no lo es.

---

## 2. Dos reglas antes de leer la lista

### 2.1 · «No decir» prohíbe **designar**, no prohíbe la palabra

La columna **No decir** no dice que esa palabra no pueda aparecer nunca: dice
que **no se usa para nombrar ESE concepto**.

La diferencia no es sutil, y se ve midiendo. Al correr la comprobación de la
sección 6 sobre este repositorio:

| Palabra | Veces | ¿Infracción? |
|---|---|---|
| `formulario` | **114** | **Casi ninguna.** Un formulario HTML es un formulario; el formulario del ASIS es un formulario. Solo es infracción cuando se usa **en lugar de `encuesta`** |
| `inscrito` | 1 | **Ninguna.** Es *«el inscrito ante la SIC»*, en el manual de marca: una marca registrada |
| `público` | 18 | **Ninguna.** «Público externo» está en la **definición oficial** de `asistente` |
| `token`, `log` | 33 | **Ninguna.** Son de infraestructura, no del dominio |

**Prohibir la palabra habría obligado a romper el manual de marca y a
renombrar los formularios HTML.** La prohibición es sobre la designación del
concepto, y así está escrita.

### 2.2 · Las citas **no se corrigen**

La transcripción de la reunión es un **registro** —en el sentido de
ISO/IEC/IEEE 15289—: se conserva, **no se edita**. Si el usuario experto dijo
«conferencista», la cita dice «conferencista», aunque el glosario haya
acordado `ponente`.

Lo que sí se corrige es **nuestra prosa alrededor de la cita**.

> Y esto no es una formalidad: **cuando la palabra de la cita y la del glosario
> no coinciden repetidamente, la que suele estar mal es la del glosario.** Ver
> **D-GLO-2**, que salió justamente de ahí.

---

## 3. Los términos del dominio

Las definiciones marcadas **(base)** son las que ya están escritas como
comentario en `db/init.sql`: son la fuente autorizada, no una paráfrasis.

### Las personas

| Término | Definición | Fuente | **No decir** |
|---|---|---|---|
| **asistente** | Persona que asiste a las cátedras abiertas, presencial o virtualmente. Puede ser estudiante, docente, administrativo, egresado, participante de rutas de formación o público externo. **(base)** | `db/init.sql` | ~~participante~~, ~~inscrito~~ *(para designar a esta persona)* |
| **ponente** 🟡 | Asistente que **dicta** una sesión. Es un valor de `ponencia.rol`, no una tabla. **El nombre está SIN DECIDIR: ver D-GLO-2** | `ponencia` | ~~conferencista~~ · `expositor` **sin decidir** |
| **usuario** | Persona que **entra al software**. No todo asistente es usuario, ni al revés | tabla `usuario` | ~~administrador~~ *(cuando se habla de la persona genérica)* |
| **externo** | Asistente **sin vinculación institucional**. Es el que obliga al consentimiento de datos | `consentimiento_datos` | ~~invitado~~ *(colisiona con `ponencia.rol`)* |

### El evento

| Término | Definición | Fuente | **No decir** |
|---|---|---|---|
| **cátedra** | El evento completo. Es la entidad central del dominio y agrupa sus sesiones | `db/init.sql` | ~~evento~~ *(reservado para `tipo_evento`, que es la clasificación del ASIS)*, ~~conferencia~~ |
| **sesión** | Cada una de las reuniones en que se dicta una cátedra. **Entidad débil**: «la reunión 3» no significa nada suelta, es la reunión 3 **de un evento**. **(base)** | `db/init.sql` | ~~encuentro~~, ~~jornada~~, ~~charla~~ |
| **ponencia** | La participación de un asistente en una sesión, con su función. Relación N:M con atributo | `db/init.sql` | ~~intervención~~ |
| **sede** | Lugar físico o virtual donde se dicta una sesión. **Es lo que distingue dos cátedras a la misma hora.** **(base)** | `1:03:33` | ~~lugar~~, ~~campus~~ |
| **modalidad** | Cómo se dicta la sesión. **Gobierna comportamiento, no es una etiqueta**: de ella depende, entre otras cosas, el canal de difusión | `db/init.sql` | ~~tipo~~, ~~formato~~ |
| **tipo de evento** | Clasificación del evento **en el ASIS**, no en este sistema. Ocho valores observados en más de cinco mil filas reales, **uno de ellos con la gran mayoría** y siete marginales. **(base)** | `db/init.sql` | ~~categoría~~ |

### El registro

| Término | Definición | Fuente | **No decir** |
|---|---|---|---|
| **registro de asistencia** | **El hecho** de que un asistente quedó registrado en una sesión, en un instante concreto, **con el programa y la vinculación que tenía en ese momento**. Es la tabla central del sistema. **(base)** | `db/init.sql` | ~~asistencia~~ a secas, ~~check-in~~, ~~inscripción~~ |
| **enlace de registro** | Punto de entrada al registro de una sesión, materializado como **QR o como URL**. El documento exige que «el QR y URL **no pueden ser reutilizables**» y que tengan hora de inicio y de cierre configurables. **(base)** | `.docx` | ~~QR~~ a secas *(el QR es una de sus dos formas, no el concepto)* |
| **clave de acceso** | **No es autenticación: es antisuplantación.** *«Yo me puedo registrar por cualquier persona, por eso tiene que ser con el correo; si no, yo te registro a vos y vos me registras a mí»* | `1:04:51` | ~~contraseña~~, ~~login~~ *(no es autenticación)* |
| **encuesta** | Cuestionario de calidad **en una versión concreta**. Se versiona porque cambiar una pregunta rompería la comparabilidad histórica. **(base)** | `db/init.sql` | ~~formulario~~ *(cuando se refiere a la encuesta)* |

### Lo institucional

| Término | Definición | Fuente | **No decir** |
|---|---|---|---|
| **facultad** | Unidad académica que agrupa programas. La referencia reflexiva produce un **árbol** —cada nodo tiene un solo padre—, no un grafo. **(base)** | `db/init.sql` | ~~unidad~~ |
| **dependencia** | Unidad organizativa, **también en árbol**. Es el ámbito del rol `COORDINADOR` | `db/init.sql` | ~~área~~, ~~departamento~~ |
| **programa académico** | Programa al que está matriculado un asistente. **cerca de cien códigos distintos** en el maestro real. **(base)** | `db/init.sql` | ~~carrera~~, ~~pregrado~~ |
| **periodo académico** | `2026-1`, `2026-2`. Lo pidió el usuario experto por su nombre: *«cuántas personas externas han ingresado en 2026-2»* | `56:16` | ~~semestre~~ |
| **vinculación** | Relación institucional de una persona **a lo largo del tiempo**. Una persona puede tener varias, en distintas fechas. **(base)** | `db/init.sql` | ~~tipo de persona~~, ~~perfil~~ |
| **alias de programa** | Texto libre con que se ha escrito un programa, y el código al que corresponde. Nace de la calidad real del formulario: «Psicologica», «Entrenamiento Deportivo.». **Es una tabla viva.** **(base)** | `db/init.sql` | ~~variante~~, ~~sinónimo~~ |

### Los procesos

| Término | Definición | Fuente | **No decir** |
|---|---|---|---|
| **lote de carga** | Cada **ejecución** de la carga del archivo de personas del ASIS. Una carga de más de dieciséis mil filas necesita **evidencia**, no un mensaje en pantalla. **(base)** | `52:57` | ~~importación~~, ~~subida~~ |
| **migración** | El envío de datos **hacia** el ASIS. Es la dirección contraria a la carga | `lote_migracion` | ~~exportación~~, ~~sincronización~~ |
| **novedad** | Una fila que la carga **no pudo procesar**, con su motivo | `novedad_carga` | ~~error~~, ~~rechazo~~ |
| **bitácora** | Registro de **toda modificación sobre datos sensibles**, con el antes y el después. Es la `RN-35`. **(base)** | `db/init.sql` | ~~log~~ *(cuando se refiere a esta tabla)* |
| **consentimiento** | **Un hecho que se registra**, no un aviso en pantalla. Ley 1581 de 2012. **(base)** | `db/init.sql` | ~~aceptación~~, ~~términos~~ |
| **parámetro** | Configuración que se cambia **sin tocar el código**. **(base)** | `.docx` | ~~ajuste~~, ~~configuración~~ a secas |

---

## 4. Las siglas

| Sigla | Qué es |
|---|---|
| **ASIS** | El sistema institucional del que salen las personas y al que se envían los registros. **Es un sistema externo**: este proyecto no lo modifica |
| **CIDEH** | La instancia que asigna a la persona encargada de una cátedra — ver D-GLO-1 |
| **QR** | Una de las **dos** formas del enlace de registro. La otra es la URL |
| **RN-nn** | Regla de negocio. Las 38 están en [`REGLAS_DE_NEGOCIO.md`](REGLAS_DE_NEGOCIO.md) |
| **D-vN-n** | Decisión de la versión *N*. Van en `4_research.md` de esa versión |
| **[Dn]** | Decisión **de la reunión**. Venían en el material recibido, que **ya no se versiona**. **No confundir con `D-vN-n`: son dos series distintas** |

---

## 5. Los términos del método

Los que el estudiante va a encontrar escritos de otra forma en la
bibliografía. Se dicen **una vez** aquí para que no crea que son cosas
distintas.

| Se dice | No se dice | Por qué |
|---|---|---|
| **requisito** | ~~requerimiento~~ | `requirement` es **una cosa** —una condición necesaria—; «requerimiento» es la **acción** de requerir, y en primera acepción un acto judicial. La norma se llama *Requirements engineering*: **ingeniería de requisitos** |
| **elicitación** | ~~recolección de requisitos~~ | **Se conserva el calco a propósito**: el requisito no está para recogerlo, **hay que hacerlo salir** |
| **spec kit** | — | Se conserva: es el nombre de la herramienta, y traducirlo deja al estudiante sin poder buscar |
| **historia de usuario** | ~~caso de uso~~ | Son cosas distintas, no sinónimos |
| **regla de negocio** | ~~requisito~~ | Una regla es verdad **siempre**; un requisito es algo que el sistema **hace** |

---

## 6. Cómo se comprueba este glosario

```bash
# 1 · Designaciones prohibidas en NUESTRA prosa. Las citas se excluyen aparte.
grep -rniw "participante\|conferencista\|expositor\|check-in" \
     --include=*.md --include=*.cs --include=*.py --include=*.html . \
  | grep -v "^./.git" | grep -v "^>"      # las líneas de cita empiezan por >

# 2 · Las 37 tablas de la base de datos están todas nombradas aquí.
grep -oiE "^CREATE TABLE [a-z_]+" db/init.sql | sed 's/CREATE TABLE //I' | sort
```

### Lo que la comprobación encontró hoy — la deuda, sin maquillar

Corrida el 14 de septiembre de 2026 sobre todo el repositorio:

| Designación | Veces | Dónde | Qué hacer |
|---|---|---|---|
| `expositor` | **10** | Análisis de elicitación e historia propuesta 12 | **Nada todavía** — es D-GLO-2 |
| `conferencista` | 4 | 3 en **citas** —no se tocan— y **1 en prosa nuestra**: `4_research.md:524` | Corregir **esa una** |
| `participante` | 7 | 6 en descripciones **de la transcripción como documento** —«el nombre del otro participante»—, que es su sentido llano | Ninguna |
| `inscrito`, `público`, `formulario`, `token`, `log` | 140 | Usos legítimos | Ninguna |

**Una sola infracción real** en todo el repositorio, y **dos asuntos sin
decidir** que la
comprobación destapó. Eso es lo que hace un glosario: no impone palabras,
**hace visible dónde el sistema y su dueño no se están entendiendo**.

> **Pendiente:** dejar estas comprobaciones dentro de `pruebas_humo/`, para que
> corran con las demás y no dependan de que alguien se acuerde.

---

## 7. Historial

| Versión | Fecha | Qué cambió |
|---|---|---|
| 1.0 | 14 sep 2026 | Primera versión. **31 términos.** Deja **tres asuntos abiertos**: D-GLO-1 (el nombre del rol principal), D-GLO-2 (`ponente` o `expositor`) y la homonimia de `rol`. La comprobación de la sección 6 encontró **una sola infracción real** —`4_research.md:524`— y **destapó D-GLO-2**, que yo no había visto |

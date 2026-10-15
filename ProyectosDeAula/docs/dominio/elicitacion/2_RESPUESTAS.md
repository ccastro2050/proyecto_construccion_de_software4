# Respuestas del usuario experto — Cátedras Abiertas

**Lo que se dijo en la reunión, literal y con el minuto**, ordenado por las
cuatro rondas de [`1_PREGUNTAS.md`](1_PREGUNTAS.md), y con la historia de
usuario que salió de cada respuesta.

> | Vecino | Qué contiene |
> |---|---|
> | [`1_PREGUNTAS.md`](1_PREGUNTAS.md) | El cuestionario, y las seis preguntas que quedaron sin responder |
> | [`3_HISTORIAS_PROPUESTAS.md`](3_HISTORIAS_PROPUESTAS.md) | **Las cinco historias** que salen de aquí y todavía no están escritas |
> | `0_historias_de_usuario.md` | Las diez historias, con sus criterios de aceptación |
> | [`../CONCEPTOS_ELICITACION.md`](../../conceptos/CONCEPTOS_ELICITACION.md) §4 | Cómo se pasa de una respuesta a una historia |

---

## De dónde salen estas citas

**Están citadas con su minuto en el material del proyecto**, y desde ahí se
contrastaron una por una contra la transcripción original. El resultado hay que
decirlo con precisión, porque no es «son literales»:

| De las 30 citas | |
|---|---|
| Aparecen **palabra por palabra** en la transcripción | 12 |
| Aparecen **en parte** — misma frase, con tropiezos de por medio | 6 |
| Están **pulidas o sintetizadas** de varios turnos | 12 |

**Ninguna dice algo que no se haya dicho**, y el minuto de todas cae donde
debe. Pero doce fueron **limpiadas** al pasarlas a los planes: se les quitaron
las repeticiones y las muletillas de la transcripción automática. Es una
práctica legítima —un plan no se lee con tartamudeos—, y aun así conviene
saberlo, porque una cita pulida ya no es una prueba: es una interpretación
fiel.

Más abajo, en **«Lo pulido y lo dicho»**, se muestran tres casos con las dos
versiones al lado.

Estas son las fuentes derivadas de donde salen las citas:

| Documento | Citas que aporta |
|---|---|
| `material_dado/PLAN-BD-CATEDRAS-ABIERTAS.md` | La mayoría: reglas del negocio y acuerdos |
| `material_dado/PLAN-API-CATEDRAS-USBMED.md` | Lo que la API debe exponer |
| `material_dado/SOLUCION/MR/06-Sustentacion.md` | Las que defienden una decisión del modelo |
| `0_historias_de_usuario.md` | Las que encabezan cada historia |
| [`../FUENTES.md`](../FUENTES.md) | **Dónde vive cada fuente primaria**: la grabación, su fecha y qué le falta al `.txt` |

> **La fuente primaria existe, y se contrastó contra ella.** El archivo
> `trascripción del video.txt` —la **fuente normativa n.º 1** según `PLAN-BD`
> §0.1— está en la carpeta de trabajo del proyecto, junto con los cuatro
> insumos que el plan enumera. **No se versiona en este repositorio**, así que
> quien lea solo el repo puede ir de la historia a la cita, pero no de la cita
> al documento que la contiene.
>
> Todas las citas de abajo **se verificaron una por una contra esa
> transcripción**: 445 turnos, de `0:00:03` a `1:52:32`.

> **Es una transcripción automática.** Su primera línea lo advierte: *«El
> contenido generado por IA puede ser incorrecto»*. Confunde «cátedras» con
> «carteras», parte frases y a veces pega el nombre del otro participante al
> final de un turno. **Aquí se citan tal cual.** Y al archivo le falta un tramo:
> entre `1:07:41` y `1:49:13` no hay un solo turno — **41 minutos**. No es un
> hueco de la reunión: **la transcripción completa está en Stream**, y basta
> volver a exportarla.

**Quién habla.** Donde el material lo dice, se atribuye. El **profesor Hugo
Nelson el usuario experto**, director del CIDEH, es el **usuario experto**: contesta por
el negocio. El **el analista** conduce la sesión: sus intervenciones
son de método, y van aparte al final.

---

## Ronda 1 — Por qué existe esto

### 1.1 · ¿Qué problema le está costando tiempo hoy?

**Produjo:** **El OBJETIVO del sistema.** El costo de hoy es tiempo por asistente, no dinero

> *«A veces los estudiantes, como no se saben el ID, colocan la cédula; toca
> buscar en ASIS con la cédula para obtener el ID»*
> — el usuario experto, `42:01`

> *«Él lo digita; si no está en ASIS, no puede seguir»* — `42:19`

**Lo que dice de verdad:** el trabajo hoy es **buscar a mano** lo que el
sistema debería resolver. Ese es el costo, y no es de dinero: es de tiempo por
cada asistente.

### 1.2 · ¿Cómo lo resuelven ahora?

**Produjo:** **El punto de partida**, y de ahí la tabla `lote_carga_asistente`: una carga necesita evidencia

> *«Ese archivo lo cargamos en la base de datos como una tabla»* — `52:57`

### 1.3 · ¿Qué tiene que salir, exactamente?

**Produjo:** **Historia 7** — y el ENTREGABLE del sistema, que no es una pantalla sino un archivo de tres columnas

> *«que la persona se registre y de una vez nos genere el archivo plano para
> pasarlo»* — `1:04:38`

> *«El archivo plano son tres: la reunión consecutivo, el ID y el código del
> programa»* — `32:34`

**→ Historia 7.** Esta es la respuesta más importante de toda la reunión: dice
**cuál es el entregable del sistema**. No es una pantalla: es un archivo de
tres columnas.

### 1.4 · ¿Quién lo recibe y qué hace con él?

**Produjo:** **El «para lograr X»** de todas las historias: el sistema existe para que el ASIS acepte el archivo

> *«Para poder inscribir la cátedra en ASIS, cualquier persona debe estar
> registrada en ASIS con ID. Si no, no lo deja registrar»* — `26:46`

> *«Las cátedras siempre le van a quedar por el ID… el programa es importante
> porque después le da información a Registro»* — `50:14`

**El «para lograr X» de las historias sale de aquí:** todo el sistema existe
para que el ASIS acepte el archivo.

---

## Ronda 2 — Quiénes

### 2.3 · ¿Quién puede entrar y quién no?

**Produjo:** **Historia 6** · y la regla `RN-01`

> *«Solo los que estén en esa tabla pueden ingresar al sistema. Si no, no
> pueden ingresar»* — `55:52`

**→ Historia 10.**

### 2.4 · ¿Hay alguien sin correo institucional?

**Produjo:** **Historia 6** · y la regla `RN-19`

> *«Los que están en Rutas de Paz no tienen correo institucional»* — `53:42`

**→ Historia 9.** Una sola frase, y de ella sale una historia entera: si el
correo institucional fuera obligatorio, este grupo quedaría fuera del sistema.

### 2.5 · ¿Cómo distinguen a un externo?

**Produjo:** **Historia 8** · y la regla `RN-08`

> *«Si son externos, nosotros en el formulario sí preguntamos si es externo o
> no; eso lo podemos depurar antes de pasarlo»* — `43:37`

> *«Que nos genere un registro de externos, pero que no nos lo arroje en el
> archivo plano; que sí podamos descargar cuántos externos asistieron»*
> — `56:16`

**→ Historia 8.** Fíjese en la forma de la respuesta: **el externo se registra
pero no se exporta**. Dos comportamientos distintos para el mismo dato, y eso
solo se sabe preguntando.

---

## Ronda 3 — El día de trabajo

### 3.2 · ¿Dos cátedras el mismo día? ¿A la misma hora?

**Produjo:** **Historia 1** · y la regla `RN-12`: la sede es lo que distingue dos cátedras a la misma hora

> *«El mismo día sí, a la misma hora no… sí es posible: una cátedra en San
> Benito, otra virtual y otra en Bello»* — el usuario experto, `1:03:33`

**→ Historia 1, y es el origen de la versión 1 entera.** Antes de esa frase el
modelo **no tenía sede**: las cátedras se dictaban «en la universidad» y
bastaba. Cuando el director dice que puede haber tres el mismo día en tres
sitios, la sede deja de ser adorno y se vuelve necesaria para no cruzarlas.

Es el mejor ejemplo de lo que enseña
[`CONCEPTOS_ELICITACION.md`](../../conceptos/CONCEPTOS_ELICITACION.md): **nadie pidió una
pantalla de sedes**. Se preguntó por el día de trabajo, y la tabla apareció
sola.

### 3.1 · ¿Qué hace primero, qué sigue?

**Produjo:** **Historia 5** — el orden de programación

> *«Necesitamos el número de reunión, que eso lo podríamos hacer previo.»*
> — `1:03:50`

**→ Historia 5.**

### 3.3 · ¿Virtual o presencial?

**Produjo:** **Historia 5** · y la regla `RN-13`

> *«Cuando es virtual enviamos el enlace; si es presencial, el QR»* — `1:06:53`

**→ Historia 5.** Un solo proceso con dos caminos, no dos procesos.

### 3.5 · ¿Qué es lo que más se equivoca?

**Produjo:** **Un CRITERIO DE ACEPTACIÓN** de las historias 1 a 4: lo que más se equivoca hay que validarlo

> *«yo me puedo registrar por cualquier persona, por eso tiene que ser con el
> correo; si no, yo te registro a vos y vos me registrás a mí»* — `1:04:51`

**→ Historia 6.**

---

## Ronda 4 — Las reglas y los ejemplos

### 4.1 · ¿Qué tiene que pasar siempre?

**Produjo:** **La regla `RN-01`**, que es transversal: no produce una historia, restringe a todas

> *«Sí o sí, el que vaya para cátedra tiene un código en ASIS, así sea que
> venga de otra universidad»* — `43:17`

### 4.2 · ¿Qué no puede pasar nunca?

**Produjo:** **La regla `RN-01`** dicha por el otro lado — es la misma restricción

> *«Una persona es toda aquella que está registrada en Asís»*
> — el analista, `27:24`, recogiendo la regla: *«eso es importante
> porque esa es una restricción»*

### 4.3 · ¿Hay excepciones, y de quién?

**Produjo:** **Historia 9** · y la regla `RN-07`: la excepción de Rutas de Paz

> *«Para el tema de Rutas de Paz sí sería una excepción: ellos entrarían sin
> programa, pero solo ellos»* — `41:16`

**→ Historia 9.** Y note el cuidado con que está respondida la pregunta
correcta: **«pero solo ellos»**. Sin esa coletilla, la excepción se habría
escrito como una regla general.

### 4.4 · Un ejemplo de verdad

**Produjo:** **Los DATOS DE PRUEBA.** Un ejemplo real no produce historia: produce las filas con que se comprueba

> *«En el ASIS está el número de identificación, la cédula… pero no es la
> información que se monta como archivo plano»* — `44:01`

### 4.7 · ¿Qué límites tienen los datos?

**Produjo:** **Criterios de aceptación** de las historias 2 y 3 — y una lección: el tope se acordó a ojo y los datos lo corrigieron

> *«Ni en descripción ni en número de identidad: sin espacios y sin tildes»*
> — `34:10`

> *«pongámosle un tope»* — `33:50`, sobre el largo del código ASIS

**Aquí hay una lección de método.** El tope se acordó **a ojo** en la reunión —
«máximo 10 dígitos»—, y al perfilar los datos reales resultó insuficiente: el
plan lo corrige a 15. Una respuesta del usuario experto **no es infalible**;
se contrasta con los datos.

### El límite que el propio usuario reconoció

> *«esa trampa se puede hacer»* — `1:05:18`

**→ Historia 10.** Le preguntaron si la clave al correo autentica, y contestó
que no del todo: basta pedir el código por teléfono. **Es un riesgo aceptado
explícitamente, no un descuido** — y queda escrito, que es la diferencia.

### El gusto que no es requisito

> *«a mí no me gusta el correo que no sea institucional; déjame, yo lo
> resuelvo»* — `55:32`

Se anota tal cual, y se marca como **preferencia**, no como regla. La columna
`es_correo_institucional` existe para poder aplicarla el día que se decida —
sin que hoy bloquee a nadie.

---

## El tramo recuperado — `1:08:47` a `1:52:32`

Este tramo faltaba en la primera exportación y **se recuperó de Stream**. Trae
lo que el `PLAN-BD` §0.2 sospechaba que se había discutido ahí: **los informes
y la parametrización**. Y trae algo más incómodo — tres requisitos con fuente
clara que **nunca se convirtieron en historia de usuario**.

### Cómo se registra la asistencia hoy

> *«los que están asistiendo lo hacen manualmente, llenan un formulario
> manual»* — el analista, `1:16:04`
>
> *«digamos que lo diligenciaban, eso era antes, pero si lo podemos automatizar
> mucho mejor»* — el usuario experto, `1:16:15`

**Esto contesta la pregunta 1.3**, que hasta ahora estaba deducida del relato.
El proceso de hoy es un formulario que se llena a mano.

### El momento crítico, dicho por el usuario experto

> *«Termina la cátedra y ahí es donde empieza el momento crítico. Es el tema de
> la asistencia. Entonces, en la asistencia, ¿cómo la registramos? Y lo otro es
> después para subir la [ASIS]»* — el usuario experto, `1:12:05`
>
> *«Si se puede hacer todo en la misma aplicación, mejor… una aplicación donde
> yo registre la asistencia y que al mismo tiempo me arroje un archivo plano
> para poder montar[lo], así que el archivo plano solamente debe llevar esos 3
> ítems, el código de reunión, el ID y el código del programa»* — `1:12:25`

**→ Historia 7**, y aquí queda confirmada con las tres columnas nombradas otra
vez, de forma independiente. Es la segunda vez que el usuario experto las
enumera igual: en `0:32:34` y aquí.

### Hay DOS códigos QR, y no son el mismo

Esta distinción no estaba en ninguna parte, y cambia el modelo:

> *«¿cierto que es un QR por cátedra, es decir, 2 cátedras diferentes, tiene un
> QR diferentes?»* — el analista, `1:18:06`, preguntándolo por tercera vez
> para estar seguro
>
> *«el otro QR es simplemente el acceso a Zoom, o sea, el que aparece en la
> programación es el acceso a Zoom. O sea, ellos no tienen que hacer registro
> de nada»* — el usuario experto, `1:17:45`

| | Para qué | Cuándo aparece |
|---|---|---|
| **QR de la programación** | Entrar a Zoom | Antes, en la cartelera de cátedras |
| **QR de registro** | Registrar la asistencia | **Al final**, en la pantalla (`1:17:02`) |

**Uno por cátedra**, confirmado dos veces (`1:17:30`, `1:17:35`). Y el sistema
tiene que distinguirlos: *«el sistema tiene que saber cuál es el QR y cuál es
el link»* — `1:21:40`.

### Los dos roles, por fin separados

**Esto contesta la pregunta 2.4**, que figuraba como respondida a medias:

> *«el de registro es del estudiante, el asistente, esto es lo que el asistente
> hace»* — el analista, `1:22:42`
>
> *«una cosa es el asistente… hay otras dos redacciones que hay que hacerlas y
> es el administrador del sistema»* — `1:23:22`

Y el usuario experto describe el recorrido completo, de una sentada:

> *«vamos a suponer [que] Hugo el usuario experto es asistente… llega a la cátedra
> abierta, ingresa… estuve en la cátedra abierta hasta el final. Al final me
> comparten el QR y yo ingreso al aplicativo para registrarme… Luego el profe
> el analista Castro llega al aplicativo y descarga de esa cátedra abierta el
> archivo plano para luego montarlo»* — el usuario experto, `1:22:48` y `1:23:07`

**Ahí está el «como…» de las historias**, dicho por el cliente: dos actores,
dos momentos, un solo sistema.

---

## El último tramo — `1:31:41` a `1:49:04`

Con este trozo la transcripción queda completa: **692 turnos**, sin ningún
hueco mayor de nueve minutos, y los que quedan son silencios mientras alguien
comparte pantalla.

Es el tramo más denso de requisitos de toda la reunión.

### El QR y el enlace: cuatro reglas duras

> *«eso se puede tener previo, o sea, ya tenerlos todos de una vez, un botón y
> que ya queden listos… que no sea sino repartirlos»* — el analista, `1:33:13`
>
> *«Que no sean el mismo para que no se reutilice… **que los QRs y links no
> sean reutilizables**»* — `1:33:33`
>
> *«el administrador solo toca un botón y genera QRs y links»* — `1:33:50`
>
> *«Y URL es no reutilizables, pues es decir, **se generan por cátedra**»* — `1:34:02`

Cuatro reglas, dichas en cincuenta segundos: **se generan todos de una vez**,
**uno por cátedra**, **no se reutilizan**, y **con un botón**.

### La ventana de registro — y por qué el QR no sirve antes de tiempo

El usuario experto trae la práctica actual:

> *«cuando generamos el formulario… le colocamos un horario en el que
> comienzan a registrarse y otro en el que se cierra la inscripción, que
> normalmente son 15 a 20 minutos del tiempo que uno estima que puede durar la
> cátedra»* — el usuario experto, `1:34:09` y `1:34:29`
>
> *«vamos a suponer que la cátedra comenzó a las 4:00, pero ya son las 5:15.
> Entonces el QR solamente abre el enlace de inscripción, por ejemplo, faltando
> 1/4 para las 5 y cierra, por ejemplo, tipo 5 y 20»* — `1:34:40`

Y el analista lo convierte en regla:

> *«el QR tiene hora de inicio y hora de fin»* — el analista, `1:35:00`
>
> *«así sea que la persona conozca el QR antes, digamos, por alguna razón, **no
> le sirve de nada**»* — `1:35:14`

**Eso es un criterio de aceptación completo**, dicho por el cliente: conocer el
QR antes no sirve; hay que esperar la ventana.

### «Que no queden quemados» — el origen de la tabla `parametro`

> *«Los valores que el administrador considere. Eso significa configurable,
> configurable o parametrizable»* — el analista, `1:38:11` y `1:38:15`
>
> *«porque si no quedarían quemados»* — `1:38:26`

Y enseguida el caso que obliga a que sea **en tiempo real**, no solo
configurable:

> *«podría hacer lo contrario: que la cátedra esté para dos horas y el
> conferencista termine en una. Entonces no podemos esperar hasta que sean las
> seis… es configurable, ya usted espicha un botón»* — el analista, `1:39:03`
>
> *«En tiempo real»* — el usuario experto, `1:39:08`

**La tabla `parametro` del modelo tiene aquí su fuente.** Hasta ahora existía
sin una cita que la respaldara.

### El objetivo del asistente: no llenar nada

> *«el formulario que le va a salir al asistente… que ya tenga, que le salga los
> datos, que no tenga que llenar nada»* — el analista, `1:40:09` y `1:40:15`
>
> *«Los datos salen de ese archivo»* — `1:40:21`

### Por qué existen las cátedras abiertas — la Ronda 1, por fin

Esta es la respuesta que faltaba desde el principio:

> *«la cátedra cuando nació, nació con… 2 fines realmente: primero,
> complementar la formación académica de los estudiantes… pero el segundo fin
> de la cátedra abierta es servir también de proyección de la universidad hacia
> afuera, en una época donde nosotros teníamos cátedras que nos llegaban
> estudiantes de Bolivariana, de la Universidad de Antioquia»*
> — el usuario experto, `1:40:37` y `1:40:57`
>
> *«en las cátedras virtuales sí nos pueden llegar personas de afuera que no
> tienen ninguna relación con la universidad»* — `1:41:19`

**Ahí está el «para lograr X» de todas las historias**, y explica por qué los
externos importan tanto: no son un caso raro, son **la mitad del propósito**.

---

## El rol «administrador» está mal nombrado, y lo dice la fuente

Este es el hallazgo con más consecuencias del tramo.

> *«la idea no hay… que pusimos administrador. Yo creo que… **no lo apuntamos
> bien**, porque no podemos ir a decirle a la unidad de tecnología: vea [la unidad de tecnología],
> suban estos estudiantes»* — el analista, `1:43:23`
>
> *«si dejamos eso al administrador, **el administrador es la unidad de
> tecnología**, y la unidad de tecnología no debe estar subiendo cátedras ni
> descargando archivos del ASIS, nada de esas cosas»* — `1:44:28`
>
> *«Creo que es como el todo eso, **es como el líder**»* — `1:44:46`

Y el usuario experto precisa quién es:

> *«sería la persona encargada de la cátedra, o sea, la persona que tiene en su
> rol el tema de cátedra, que puede ser la persona asignada por el CIDEH, o en
> este caso también puedo ser yo»* — el usuario experto, `1:43:46`

Con la previsión de que haya más:

> *«inicialmente lo vamos a tener solo con **2 roles**, es la primera versión,
> pero si hay que crear otros dos roles, si es como el líder o el asistente o el
> delegado»* — el analista, `1:44:08`

**Y sin embargo:**

| Dónde | Qué dice |
|---|---|
| Las diez historias | «administrador», **21 veces**. «líder», **cero** |
| La tabla `rol` | `Administrador`, `Consulta`, `Coordinador de dependencia` |

O sea: **el nombre que la fuente corrigió expresamente es el que quedó en todo
el sistema.** No es un problema de etiqueta: si «administrador» se entiende como
la unidad de tecnología —que es lo que significa en la Universidad—, el rol se
le asigna a quien no debe tenerlo. La reunión lo vio venir y lo dijo; el
documento no lo recogió.

**Qué hacer con esto:** no renombrar a la ligera. Llevarlo a la próxima sesión
—que quedó citada *«Dentro de 8 días nos reunimos hacia las 3:00 otra vez»*, `1:48:45`— y decidir el
nombre con el usuario experto. Lo que no puede quedar es como está: con la
fuente diciendo una cosa y el modelo otra.

---

## Tres requisitos con fuente y sin historia

Lo más valioso del tramo recuperado es lo que **falta** aguas abajo.

### 1 · La encuesta rápida

> *«Sería bueno que contestara una encuesta rápida, o sea, ahí mismo entra y
> que conteste una encuesta rápida. Hay que crear esa encuesta»*
> — el analista, `1:24:17`
>
> *«Muy rápida, pero que a ustedes sí les sirva… Como 3 o cuatro estrellitas y
> ya y enviar y listo»* — `1:24:38` y `1:24:45`

**No hay ninguna historia de usuario sobre la encuesta.** Y sin embargo:

| | |
|---|---|
| En la base de datos | Existen `encuesta`, `pregunta`, `opcion_pregunta`, `respuesta_encuesta`, `respuesta_item` |
| En la API v2 | `/api/encuesta` y `/api/tipo_pregunta` responden `200` |
| En las historias | **cero menciones** |

O sea: **se construyó sin historia que lo respaldara**. No está mal construido
—la fuente existe, y es esta—, pero el hilo se rompió: del requisito al código,
sin pasar por la historia. Es exactamente lo que el spec kit existe para
evitar, y por eso conviene escribir esa historia ahora, con estas citas.

### 2 · El expositor, un actor que no está en ninguna historia

> *«si el profesor está exponiendo, pues el expositor… lo que hace es que se le
> hace llegar el QR, ¿cierto? antes de la conferencia, se le dice muestra este
> QR en la[ pantalla]»* — el analista, `1:31:41`

Las diez historias tienen **dos** actores: el administrador y el asistente.
Aquí aparece un **tercero** —el expositor o ponente—, con una tarea concreta:
recibir el QR antes de la conferencia y mostrarlo al final. Cero menciones en
las historias.

### 3 · Las estadísticas para decidir

> *«pongamos las 2 o 3 consultas que vos necesitás, por ejemplo, ¿cuáles son
> institucionales? ¿Cuáles son externos?»* — el analista, `1:28:39`
>
> *«estadísticas [de] institucionales, estadísticas de externos… te gustaría
> mucho tener unas estadísticas ahí a la mano para tomar decisiones con las
> cátedras»* — `1:28:55`
>
> *«un botón y que lo saque ya, o sea, que no se ponga el administrador a armar
> nada, que eso lo haga el sistema»* — `1:29:42`

La **Historia 8** cubre una parte —descargar cuántos externos asistieron—, pero
la petición es más amplia: **estadísticas para tomar decisiones**, y el propio
analista pide que se piense la lista completa: *«sentate este fin de semana con
la almohada a pensar qué reporte le puedo sacar»* — `1:29:57`.

---

## Y una petición que cambia la Historia 7

El informe que hoy entrega el ASIS **se queda corto**, y en la reunión se
acuerda ampliarlo:

> *«el que me enseñaron saca solamente esos 3»* — el usuario experto, `1:50:18`
>
> *«esos 3 más los nombres y el correo electrónico»* — el analista, `1:50:25`
>
> *«serían 3 porque creo que nos entrega correo institucional y correo
> personal»* — el usuario experto, `1:50:37`
>
> *«que entregue esos tres[:] correo institucional, correo personal y el nombre…
> la persona cuando entre con su código se le muestre todo el formulario y con
> los datos de una vez, pues no va a llenar nada. Lo único que tiene que digitar
> es o la cédula […] o el código institucional o el correo electrónico»*
> — el analista, `1:50:43` y `1:51:02`

**Ese es el objetivo real del sistema visto desde el asistente: no llenar
nada.** Entra con un dato y el resto ya está. Y depende de que el ASIS amplíe
su informe — una dependencia externa que ninguna historia menciona.

> **Y queda anotado un riesgo técnico, en boca del analista:** *«voy a mirar
> cómo resuelvo algo que son asuntos de seguridad cuando el correo electrónico
> no es institucional… a esos sistemas de seguridad no les gusta mucho»*
> — `1:51:21`. Es el mismo asunto de `0:55:32`, todavía abierto al final de la
> reunión.

---

## Lo pulido y lo dicho

Tres ejemplos, con la cita del plan arriba y lo que quedó en la transcripción
abajo. No para desautorizar el plan —dice lo mismo—, sino para que se vea qué
significa «pulir».

### Caso 1 — se le quitaron los tropiezos

| | |
|---|---|
| **El plan cita** | *«Los que están en Rutas de Paz no tienen correo institucional»* — `53:42` |
| **La transcripción dice** | *«Y no todos, por ejemplo, los que están en las en la ruta de paz en rutas de paz no tienen correo institucional»* — `0:53:42` |

Misma frase, mismo minuto, misma persona. Le sobraban un «en las» y una
repetición.

### Caso 2 — está compuesta de varios turnos

| | |
|---|---|
| **El plan cita** | *«Para el tema de Rutas de Paz sí sería una excepción: ellos entrarían sin programa, pero solo ellos»* — `41:16` |
| **La transcripción dice** | `0:41:16` el usuario experto: *«…el formato del archivo plano lo único que te pide el estudiante es el ID y el código de programa pero pero aquí es donde pues Andrea me…»* · `0:41:35` el analista: *«hay quienes no tienen programa»* · `0:41:35` el usuario experto: *«Solo ellos»* |

**Esta no es una cita: es un resumen de tres turnos**, y el «pero solo ellos»
—que es la parte decisiva, la que evita que la excepción se vuelva regla—
aparece diecinueve segundos después, como una frase suelta de dos palabras.
El plan hizo bien en juntarlo; pero quien lo lea debe saber que lo juntó
alguien.

### Caso 3 — la respuesta llega antes que la pregunta termine

| | |
|---|---|
| **El plan cita** | *«Para poder inscribir la cátedra en ASIS, cualquier persona debe estar registrada en ASIS con ID»* — `26:46` |
| **La transcripción dice** | *«No hay profe, solamente es el código de identificación en el sistema Asís, porque la cédula, sí, profe, o sea, todo, o sea, para poder inscribir el ca…»* — `0:26:46` |

Aquí se ve cómo habla la gente cuando explica algo que tiene clarísimo: empieza
por la negación, se corrige dos veces y llega al final. El plan se quedó con el
final, que es donde está la regla.

---

## Las tres historias que no salen de una cita

| Historia | De dónde sale |
|---|---|
| **2** — registrar una sede nueva | Derivada de la 1: si hay catálogo de sedes, alguien las da de alta |
| **3** — corregir una sede | Derivada de la 1 |
| **4** — retirar una sede del catálogo | Derivada de la 1, más la regla de borrado lógico del modelo |

**Se dice que son derivadas, y no se les inventa una cita.** Son el CRUD que
acompaña a un catálogo: legítimas, pero de otro origen. Confundir una historia
dicha por el usuario con una deducida por el equipo es exactamente lo que este
documento existe para evitar.

---

## El método, que no es el dominio

Estas intervenciones son del **el analista** conduciendo la sesión.
No son requisitos: son cómo se trabaja.

> *«Hacer un modelo entidad-relación… para mí es de las cosas más importantes,
> y algunos las subestiman»* — `0:03`

> *«Con la inteligencia artificial ya no se codifica… uno interactúa con los
> requerimientos y ya va entregando el código»* — `0:11`

> *«Cada entidad representa un solo objeto de estudio… es la información que
> vamos a guardar para después consultar»* — `5:44`

> *«Hay que identificar qué atributos necesitamos… y cuál de esos atributos es
> el identificador»* — `5:59`

> *«Estos rombos son las relaciones, que son acciones. La idea es que esas
> relaciones sean verbos»* — `7:22`

> *«Lo más parecidos posibles a la realidad. La idea es no usar verbos como
> haber o tener»* — `7:34`

> *«esto va a ser para la IA… así se va a hacer el prompt para la IA; entre
> menos espacios tenga, más cortico es el prompt»* — `58:52`

**Por qué importa separarlas.** El `.docx` que salió de la reunión no es un
documento de requisitos: es **un prompt**, y está escrito para la IA. Tratarlo
como especificación es el error que el spec kit de este proyecto existe para
evitar.

---

## Qué falta

Seis preguntas de [`1_PREGUNTAS.md`](1_PREGUNTAS.md) no tienen respuesta en lo
transcrito:

| | Qué se decide con ella |
|---|---|
| **1.4** ¿Qué número cambia? | Si el sistema sirvió. Es la medida de éxito |
| **1.5** ¿Qué pasa si no hacemos nada? | La prioridad real del proyecto |
| **2.5** ¿Quién sale perjudicado? | Los conflictos que aparecen al desplegar |
| **3.6** ¿A quién llama cuando algo falla? | El manejo de errores y el soporte |
| **3.7** ¿Cuántas cátedras al semestre? | El volumen: índices, paginación, escala |
| **4.8** ¿Se puede corregir? | Si hace falta auditoría, y si el borrado es lógico |

**Pero «sin respuesta» aquí no quiere decir «no se preguntó».** Al `.txt`
exportado le faltan **41 min 32 s** —de `1:07:41` a `1:49:13`—, y ese es
justamente el tramo donde el `PLAN-BD` §0.2 supone que se discutieron los
informes y la parametrización. **La transcripción completa está en Stream**:
antes de dar estas seis por no hechas, hay que volver a exportarla. Está
explicado en [`../FUENTES.md`](../FUENTES.md).

Y las que sigan sin respuesta después de eso **no se rellenan a ojo**: van al
spec kit como `[NECESITA ACLARACIÓN]` y son la agenda de la próxima sesión.

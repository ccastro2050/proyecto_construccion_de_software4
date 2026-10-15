# Historias propuestas — las que la fuente pide y no están escritas

**Cinco historias, del 11 al 15**, con el mismo formato de
`0_historias_de_usuario.md`. Todas
salen del tramo de la reunión que faltaba en la primera exportación y se
recuperó de Stream.

> **Esto es una propuesta, no el documento oficial.** Las diez historias
> vigentes están firmadas y revisadas por el usuario experto. Estas cinco
> **no**: se escriben aquí para que él las revise, las corrija o las descarte,
> y solo entonces pasen a `0_historias_de_usuario.md` como versión 3.0.

---

## Antes de leerlas: tres advertencias

**1 · Ninguna tiene número de decisión.** Las historias vigentes citan `[D1]` a
`[D17]`, la tabla de decisiones de
`PLAN-BD-CATEDRAS-ABIERTAS.md`.
Esa tabla se escribió **cuando el tramo todavía faltaba**, así que estos
requisitos no están en ella. Habrá que agregarlos allá, o decir por qué no.

**2 · No todas las pidió el usuario experto.** Y la diferencia importa:

| Historia | Quién la propuso | Qué contestó el otro |
|---|---|---|
| 11 · La encuesta | **El analista** (`1:24:17`) | *«bueno, sí se puede»* — `1:24:31` |
| 12 · El expositor | **El analista** (`1:31:41`) | *«Exactamente»* — `1:33:25` |
| 13 · Las estadísticas | **El analista** pregunta (`1:28:39`); el usuario las escribe | *«Eso es clave, eso que estás escribiendo ahí es»* — `1:29:19` |
| 14 · La ventana del QR | **El usuario experto** trae la práctica (`1:34:09`) | El analista la vuelve regla — `1:35:00` |
| 15 · La parametrización | **El analista** (`1:38:11`) | *«Los valores que considere perfecto, sí»* — `1:38:26` |

**Una idea aceptada no es lo mismo que una necesidad enunciada.** Cuatro de
estas cinco nacieron del analista y el usuario dijo que sí. Eso no las invalida
—son buenas ideas y el cliente las aprobó—, pero al revisarlas hay que
preguntar por cada una: *¿esto resuelve un problema suyo, o le pareció bien
porque se lo propusieron?*

**3 · La 11 ya está construida.** Existen las tablas `encuesta`, `pregunta`,
`opcion_pregunta`, `respuesta_encuesta` y `respuesta_item`, y `/api/encuesta`
responde. Se construyó sin historia detrás; esta historia llega tarde, a
ponerle el respaldo que le faltaba.

---

| **Número:** 11 | **Usuario:** quien asiste a la cátedra |
| **Nombre historia:** La encuesta rápida al final del registro ||
| **Diseñada por:** Propuesta por el analista (analista) · Aceptada por el usuario experto ||
| **Prioridad:** Media | **Riesgo en desarrollo:** Bajo |
| **Puntos estimados:** 3 · **Horas estimadas:** 10 | **Iteración asignada:** por definir |
| **Programador responsable:** Equipo del proyecto de aula ||

**Descripción:**

Yo, como asistente a una cátedra abierta, quiero **contestar una encuesta muy corta justo después de registrar mi asistencia**, sin salir de la misma pantalla, para dejar mi opinión sin que me cueste tiempo.

> *«Sería bueno que contestara una encuesta rápida, o sea, ahí mismo entra y que conteste una encuesta rápida. Hay que crear esa encuesta»* — el analista, `1:24:17`

> *«Muy rápida, pero que a ustedes sí les sirva.»* — `1:24:38`

> *«Como 3 o cuatro estrellitas y ya y enviar y listo.»* — `1:24:45`

**Observaciones:**

- **«Ahí mismo»** es el requisito, no un adorno: la encuesta va **dentro del registro**, no en un correo posterior. Una encuesta que se manda después no la contesta nadie.
- **«3 o cuatro estrellitas»** fija el tamaño, no el diseño. Lo que se pide es que se conteste en segundos; si la encuesta crece, deja de cumplir su propio requisito.
- *«pero que a ustedes sí les sirva»* pone la condición difícil: **sirve si se usa para decidir**. Conviene atarla a la historia 13 — si nadie mira el resultado, la encuesta sobra.
- **Esta historia llega después del código.** Las cinco tablas de encuesta ya existen. Al revisarla hay que comprobar lo contrario de lo habitual: que lo construido corresponda a lo que aquí se pide, y no al revés.

**Criterios de aceptación:**

- Que la encuesta aparezca inmediatamente después de registrar la asistencia, en la misma pantalla.
- Que se pueda contestar sin escribir texto, solo eligiendo.
- Que se pueda **omitir**: la asistencia ya quedó registrada, y no se le puede negar por no opinar.
- Que una persona no pueda contestarla dos veces para la misma sesión.
- Que el resultado sea consultable por quien administra las cátedras.

---

| **Número:** 12 | **Usuario:** el expositor o ponente de la cátedra |
| **Nombre historia:** El expositor recibe el QR antes y lo muestra al final ||
| **Diseñada por:** Propuesta por el analista (analista) · Aceptada por el usuario experto ||
| **Prioridad:** Alta | **Riesgo en desarrollo:** Bajo |
| **Puntos estimados:** 2 · **Horas estimadas:** 6 | **Iteración asignada:** por definir |
| **Programador responsable:** Equipo del proyecto de aula ||

**Descripción:**

Yo, como expositor de una cátedra abierta, quiero **recibir el código QR de mi cátedra antes de la conferencia**, para mostrarlo en pantalla al terminar y que los asistentes registren su asistencia sin que nadie tenga que buscarlo en ese momento.

> *«si el profesor está exponiendo, pues el expositor… lo que hace es que se le hace llegar el QR, ¿cierto? antes de la conferencia, se le dice muestra este QR en la[ pantalla], ah, esa idea está buena»* — el analista, `1:31:41`

> *«eso se puede tener previo, o sea, ya tenerlos todos de una vez, un botón y que ya queden listos… que no sea sino repartirlos»* — `1:33:13`

**Observaciones:**

- **Aparece un tercer actor.** Las diez historias vigentes solo tienen dos: quien administra y quien asiste. El expositor no es ninguno de los dos — no entra al sistema, pero sin él la asistencia no se registra.
- **«Antes de la conferencia»** es lo que obliga a generar los QR por anticipado, y enlaza con la historia 14: el QR existe antes, pero **no funciona** antes de su ventana.
- Falta decidir **cómo le llega**: correo, descarga, o se lo entrega quien coordina. La reunión no lo dice.

**Criterios de aceptación:**

- Que el QR de una cátedra se pueda obtener **antes** de que la cátedra ocurra.
- Que sea el de **esa** cátedra y no sirva para otra.
- Que se pueda entregar en un formato que se pueda proyectar.
- Que tenerlo antes **no permita** registrarse antes: eso lo decide la historia 14.

---

| **Número:** 13 | **Usuario:** el usuario experto · Director del CIDEH |
| **Nombre historia:** Las estadísticas para decidir sobre las cátedras ||
| **Diseñada por:** Propuesta por el analista (analista) · Los indicadores los enumeró el usuario experto ||
| **Prioridad:** Media | **Riesgo en desarrollo:** Medio |
| **Puntos estimados:** 5 · **Horas estimadas:** 16 | **Iteración asignada:** por definir |
| **Programador responsable:** Equipo del proyecto de aula ||

**Descripción:**

Yo, el usuario experto, como director del CIDEH, quiero **ver cuántos asistentes fueron institucionales y cuántos externos**, sin armar nada a mano, para decidir qué cátedras programar y en qué modalidad.

> *«pongamos las 2 o 3 consultas que vos necesitás, por ejemplo, ¿cuáles son institucionales? ¿Cuáles son externos?»* — el analista, `1:28:39`

> *«estadísticas [de] institucionales, estadísticas de externos… te gustaría mucho tener unas estadísticas ahí a la mano para tomar decisiones con las cátedras»* — `1:28:55`

> *«un botón y que lo saque ya, o sea, que no se ponga el administrador a armar nada, que eso lo haga el sistema»* — `1:29:42`

**Observaciones:**

- **«Para tomar decisiones»** es la razón de ser, y también el criterio para descartar indicadores: si un número no cambia ninguna decisión, no va.
- **La lista está incompleta a propósito.** El analista pidió pensarla: *«sentate este fin de semana con la almohada a pensar qué reporte le puedo sacar»* — `1:29:57`. **Es una tarea pendiente del usuario experto**, no del equipo.
- Se solapa en parte con la **historia 8** —descargar cuántos externos asistieron—, que aquí queda como un caso particular.
- **Y conecta con el propósito de la cátedra** (`1:40:57`): si el segundo fin es la proyección hacia afuera, el número de externos **mide si eso está funcionando**. Es el indicador que la Ronda 1 pedía y nadie había nombrado.

**Criterios de aceptación:**

- Que se pueda ver, por periodo, cuántos asistentes fueron institucionales y cuántos externos.
- Que salga con una acción, sin exportar ni cruzar archivos a mano.
- Que los números se puedan ver por cátedra y agregados.
- Que la lista de indicadores quede abierta: agregar uno nuevo no puede exigir rehacer la pantalla.

---

| **Número:** 14 | **Usuario:** el usuario experto · Encargado de la cátedra |
| **Nombre historia:** El QR solo sirve dentro de su ventana ||
| **Diseñada por:** La práctica la trajo el usuario experto · La regla la formuló el analista ||
| **Prioridad:** Alta | **Riesgo en desarrollo:** Medio |
| **Puntos estimados:** 5 · **Horas estimadas:** 14 | **Iteración asignada:** por definir |
| **Programador responsable:** Equipo del proyecto de aula ||

**Descripción:**

Yo, como encargado de la cátedra, quiero que **el registro solo se pueda hacer entre una hora de inicio y una hora de fin**, para que nadie registre asistencia a una cátedra a la que no fue.

> *«cuando generamos el formulario… le colocamos un horario en el que comienzan a registrarse y otro en el que se cierra la inscripción, que normalmente son 15 a 20 minutos del tiempo que uno estima que puede durar la cátedra»* — el usuario experto, `1:34:09` y `1:34:29`

> *«vamos a suponer que la cátedra comenzó a las 4:00, pero ya son las 5:15. Entonces el aplicativo o el QR solamente abre el enlace de inscripción, por ejemplo, faltando 1/4 para las 5 y cierra, por ejemplo, tipo 5 y 20»* — `1:34:40`

> *«el QR tiene hora de inicio y hora de fin»* — el analista, `1:35:00`

> *«así sea que la persona conozca el QR antes, digamos, por alguna razón, no le sirve de nada»* — `1:35:14`

**Observaciones:**

- **Esta historia es la que hace honesto el registro.** Sin ventana, el QR circula y cualquiera se registra desde su casa. Con ventana, hay que estar cuando toca.
- **Es una práctica que ya existe**, en los formularios de hoy. No se está inventando un control: se está conservando uno que funciona.
- **Los 15 a 20 minutos son un ejemplo, no la regla.** La duración la fija quien programa — y de ahí nace la historia 15.
- Se apoya en la historia 12: el expositor **tiene el QR antes**, y aun así no puede usarse antes.

**Criterios de aceptación:**

- Que cada cátedra tenga una hora de apertura y una de cierre del registro.
- Que fuera de esa ventana el QR y el enlace **no permitan registrarse**, y digan por qué.
- Que el rechazo distinga **«todavía no»** de **«ya se cerró»**: no es el mismo problema para quien lo lee.
- Que la ventana quede registrada junto con la asistencia, para poder auditar después.

---

| **Número:** 15 | **Usuario:** el usuario experto · Encargado de la cátedra |
| **Nombre historia:** Los tiempos se parametrizan, y se pueden cambiar en vivo ||
| **Diseñada por:** Propuesta por el analista (analista) · Aceptada por el usuario experto ||
| **Prioridad:** Alta | **Riesgo en desarrollo:** Medio |
| **Puntos estimados:** 5 · **Horas estimadas:** 14 | **Iteración asignada:** por definir |
| **Programador responsable:** Equipo del proyecto de aula ||

**Descripción:**

Yo, como encargado de la cátedra, quiero **poder fijar los tiempos de la ventana de registro, y cambiarlos mientras la cátedra ocurre**, porque el ponente puede alargarse o terminar antes.

> *«Los valores que el administrador considere. Eso significa configurable, configurable o parametrizable.»* — el analista, `1:38:11` y `1:38:15`

> *«porque si no quedarían quemados»* — `1:38:26`

> *«podría hacer lo contrario: que la cátedra esté para dos horas y el conferencista termine en una. Entonces no podemos esperar hasta que sean las seis… es configurable, ya usted espicha un botón»* — `1:39:03`

> *«En tiempo real.»* — el usuario experto, `1:39:08`

**Observaciones:**

- **«Quemados»** es la palabra que hay que respetar: los tiempos **no** van escritos en el código. Van en datos, y los cambia quien administra, sin que nadie despliegue nada.
- **Hay dos necesidades, no una**, y conviene no confundirlas: *configurar antes* (la ventana prevista) y *ajustar durante* (la cátedra se alargó o se acortó). La segunda es la difícil, y es la que el usuario confirmó con dos palabras: *«En tiempo real»*.
- El caso real que lo motiva lo cuental usuario experto: *«vamos a suponer que es una persona, un invitado especial y no somos capaces de parar y sigue y sigue hablando»* — `1:38:46`.
- **La tabla `parametro` del modelo sale de aquí.** Existía sin ninguna cita que la respaldara; esta es.

**Criterios de aceptación:**

- Que la ventana de registro de una cátedra se pueda definir al programarla.
- Que se pueda **modificar mientras la cátedra está en curso**, con una acción.
- Que ningún tiempo esté escrito en el código.
- Que quede registro de quién cambió la ventana y cuándo: es la diferencia entre ajustar y manipular.

---

## Lo que estas cinco no resuelven

**El nombre del rol.** La reunión dice que «administrador» está mal puesto
—*«el administrador es la unidad de tecnología, y la unidad de tecnología no
debe estar subiendo cátedras»*, `1:44:28`; *«es como el líder»*, `1:44:46`— y
las diez historias vigentes lo usan 21 veces. **Eso no se arregla con una
historia nueva**: hay que decidir el nombre con el usuario experto y corregir
las diez, el modelo y la tabla `rol`.

En estas cinco se usó **«encargado de la cátedra»**, que es como lo describió
él mismo (`1:43:46`), en vez de «administrador». Si al revisar se acuerda otro
nombre, hay que cambiarlo aquí también.

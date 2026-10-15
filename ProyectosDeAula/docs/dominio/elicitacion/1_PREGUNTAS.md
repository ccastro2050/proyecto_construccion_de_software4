# Preguntas al usuario experto — Cátedras Abiertas

**Las preguntas que de verdad se hicieron en la reunión**, ordenadas por las
cuatro rondas que fija [`CONCEPTOS_ELICITACION.md`](../../conceptos/CONCEPTOS_ELICITACION.md)
§3 — y, al final, **las que el método pide y nadie hizo**.

> | Vecino | Qué contiene |
> |---|---|
> | [`2_RESPUESTAS.md`](2_RESPUESTAS.md) | Lo que se contestó, literal y con el minuto |
> | [`3_HISTORIAS_PROPUESTAS.md`](3_HISTORIAS_PROPUESTAS.md) | Las cinco historias que faltan, para revisar |
> | [`../CONCEPTOS_ELICITACION.md`](../../conceptos/CONCEPTOS_ELICITACION.md) | El método: por qué cuatro rondas y qué sale de cada una |
> | `0_historias_de_usuario.md` | Las diez historias que salieron de estas respuestas |

---

## De dónde sale este documento

De la **transcripción de la reunión** —`trascripción del video.txt`, 445
turnos, de `0:00:03` a `1:52:32`—, que es la fuente normativa n.º 1 según
`PLAN-BD` §0.1.

Dos advertencias, y las dos importan:

> **La transcripción es automática, y lo dice ella misma.** Su primera línea
> es *«El contenido generado por IA puede ser incorrecto»*. Se nota: confunde
> «cátedras» con «carteras», parte frases a mitad y a veces pega el nombre del
> otro participante al final de un turno. **Las citas de aquí se transcriben
> tal cual**, con sus tropiezos, porque corregirlas sería inventar. Cuando el
> sentido no se entiende sin contexto, se dice.

> **El tramo que faltaba se recuperó de Stream.** La primera exportación no
> tenía nada entre `1:07:41` y `1:49:13`. Ya está el trozo `1:08:47`–`1:52:32`,
> y con él aparecieron los informes y la parametrización que el `PLAN-BD`
> sospechaba. **Queda un hueco menor: `1:31:41`–`1:49:13`, 17 min 32 s.**
>
> Lo recuperado está en
> [`2_RESPUESTAS.md` § El tramo recuperado](2_RESPUESTAS.md), con tres
> requisitos que tienen fuente y **no tienen historia de usuario**.
>
> **Por eso las seis preguntas que abajo figuran como «nunca se hicieron» hay
> que leerlas con cautela:** puede que sí se hicieran, dentro de esos 41
> minutos. Volver a exportar la transcripción completa lo resuelve, y es el
> primer paso antes de citar a una nueva sesión.
>
> *(El `PLAN-BD` §0.2 lo presenta como una limitación de la fuente y sitúa el
> reinicio en `1:46:42`. En este archivo el hueco va de `1:07:41` a `1:49:13`.)*

Cada pregunta lleva su estado:

| Marca | Significa |
|---|---|
| **✔** | Se hizo y se contestó — la respuesta está en [`2_RESPUESTAS.md`](2_RESPUESTAS.md) |
| **◑** | Se hizo, y la respuesta quedó incompleta o se fue por otro lado |
| **✘** | **No está en lo transcrito.** El método la pide y no aparece — puede estar en los 41 minutos que faltan |

---

## Ronda 1 — Por qué existe esto

Preguntas sobre el **negocio**. De aquí sale el **«…para lograr X»** que cierra
todas las historias.

| # | Pregunta, tal como se hizo | Quién · minuto | |
|---|---|---|---|
| 1.1 | *«Para pasar las [cátedras] abiertas, ¿cuáles eran los ítems que nos pedía?»* | el usuario experto · `0:31:41` | ✔ |
| 1.2 | *«¿El otro es el ID y me está faltando uno, cuál es?»* | el usuario experto · `0:31:59` | ✔ |
| 1.3 | *«¿ahora hoy cómo se hace? Si hoy hay cátedra, los que son asistentes presencial, ¿cómo llenan su registro?»* | el analista · `1:16:23` | ✔ |
| 1.4 | Si esto funciona, **¿qué número cambia?** | — | ✘ |
| 1.5 | ¿Qué pasa si no hacemos nada? | — | ✘ |

> **Fíjese quién pregunta en 1.1.** No es el analista: es el **usuario experto**
> preguntándole a su propio equipo qué pide el ASIS. La respuesta define el
> entregable de todo el sistema —el archivo plano de tres columnas—, y sale de
> una duda del propio cliente. Eso pasa, y hay que dejar hablar.

---

## Ronda 2 — Quiénes

De aquí sale el **«como…»** de cada historia.

| # | Pregunta, tal como se hizo | Quién · minuto | |
|---|---|---|---|
| 2.1 | *«Una persona es toda aquella que esté registrada en Asís, ¿cierto?»* | el analista · `0:27:07` | ✔ |
| 2.2 | *«¿Incluso yo tengo mi código estudiantil viejísimo, pero pues ahí tengo mi código, no es cierto?»* | el usuario experto · `0:27:47` | ✔ |
| 2.3 | *«¿Cuántas personas externas, por ejemplo, han ingresado?»* | el usuario experto · `0:56:16` | ✔ |
| 2.4 | ¿Quién decide, quién ejecuta, quién solo consulta? | el analista · `1:22:42` y `1:23:22` | ✔ |
| 2.5 | **¿Quién sale perjudicado si esto se hace?** | — | ✘ |

> **2.1 y 2.2 son la misma pregunta hecha dos veces**, y así es como se
> confirma una regla: el analista la enuncia, y el usuario la pone a prueba con
> **su propio caso** —«yo tengo mi código viejísimo»—. Una regla que sobrevive
> al caso raro del propio cliente, sirve.

---

## Ronda 3 — El día de trabajo

Se recorre el trabajo **en el orden en que pasa**. De aquí sale el
**«quiero…»**, y el orden del viaje sugiere el orden de las versiones.

| # | Pregunta, tal como se hizo | Quién · minuto | |
|---|---|---|---|
| 3.1 | *«él entra al sistema. ¿Qué le debe mostrar? ¿Qué le debe mostrar una pantalla con qué?»* | el analista · `0:58:35` | ✔ |
| 3.2 | *«¿Y luego qué se imagina usted?»* | el analista · `0:59:41` | ✔ |
| 3.3 | *«¿Es el me estás diciendo que a la misma hora pueden haber que a la misma hora pueden haber varias cátedras?»* | el analista · `1:03:17` | ✔ |
| 3.4 | *«ahora yo pienso con los correos personales. ¿Cómo lo hacemos?»* | el analista · `0:53:50` | ✔ |
| 3.5 | ¿En qué paso pierde más tiempo? | — *contestada sin preguntar* | ✔ |
| 3.6 | ¿Qué hace cuando algo sale mal? ¿A quién llama? | — | ✘ |
| 3.7 | ¿Cuántas cátedras al semestre? | — | ✘ |

> **La pregunta 3.3 es la que construyó la versión 1**, y véala como quedó
> transcrita: repetida, con un «¿Es el» suelto al principio. Es una pregunta de
> confirmación —«¿me estás diciendo que…?»—, de las que parecen trámite. La
> respuesta trajo la tabla `sede`, que antes no existía en el modelo. Así de
> desaliñado suena el momento en que un proyecto encuentra su primera tabla.

---

## Ronda 4 — Las reglas y los ejemplos

De aquí salen los **criterios de aceptación**.

| # | Pregunta, tal como se hizo | Quién · minuto | |
|---|---|---|---|
| 4.1 | *«¿Cuáles serían los atributos de asistente… los que sí o sí se tengan que manejar obligatorios?»* | el analista · `0:25:35` | ✔ |
| 4.2 | *«Listo, ¿qué es el ID? Describámoslo.»* | el analista · `0:26:26` | ✔ |
| 4.3 | *«¿Qué otro campo? Necesitamos que sea obligatorio»* | el analista · `0:28:06` | ✔ |
| 4.4 | *«¿Que siempre son cuántos dígitos?»* | el analista · `0:33:21` | ◑ |
| 4.5 | *«a mí no me gusta el correo electrónico que no sea institucional. ¿Cómo resuelvo yo eso?»* | el analista · `0:55:32` | ✔ |
| 4.6 | *«¿O el qué más?»* (qué identifica a una persona) | el analista · `0:57:16` | ✔ |
| 4.7 | ¿Y si el dato llega vacío? ¿Y si llega dos veces? | — | ◑ |
| 4.8 | ¿Y si quien lo hizo se equivocó — se corrige o se rehace? | — | ✘ |

> **La 4.4 está marcada a medias a propósito.** Se preguntó bien, y la
> respuesta fue *«Yo sé que hay 30000 y 300000 no, pero digamos que son 334»* —
> o sea, **el usuario no lo sabía y se acordó un tope a ojo**. Al perfilar los
> datos reales el tope resultó corto y el plan lo corrigió a 15 caracteres. Una
> respuesta del experto no es infalible: se contrasta con los datos.

---

## Lo que quedó sin preguntar — o sin transcribir

Seis preguntas del método no aparecen en lo transcrito. **Antes de darlas por
no hechas**, hay que exportar de Stream los 41 minutos que le faltan al `.txt`:
es justo el tramo donde, según el `PLAN-BD`, se habrían discutido los informes
y la parametrización.

| Pregunta | Qué se decide con ella |
|---|---|
| **1.4** ¿Qué número cambia? | Si el sistema sirvió. Es la medida de éxito |
| **1.5** ¿Qué pasa si no hacemos nada? | La prioridad real del proyecto |
| **2.5** ¿Quién sale perjudicado? | Los conflictos que aparecen al desplegar, no al programar |
| **3.6** ¿A quién llama cuando algo falla? | El manejo de errores y el soporte |
| **3.7** ¿Cuántas cátedras al semestre? | El volumen: define índices, paginación y si algo escala |
| **4.8** ¿Se puede corregir? | Si hace falta auditoría, y si el borrado es lógico o físico |

**No se rellenan a ojo.** Van al spec kit como `[NECESITA ACLARACIÓN]` y son la
agenda de la próxima sesión — que quedó acordada en la misma reunión:
*«yo tengo para dentro de 8 días un prototipo»*, el analista, `1:49:55`.

> **Corrección: la advertencia del `PLAN-BD` §0.2 era correcta.** En una
> versión anterior de este documento yo escribí que el minuto `1:47:35` «no
> existe en la transcripción». Era falso: no existía **en la exportación
> incompleta** que teníamos. Recuperado el tramo, ahí está, y confirma lo que
> el plan decía — con más detalle:
>
> > *«¿La otra grabación? Sabes que no la pude encontrar, ¿vos me puedes
> > recordar?»* — el analista, `1:46:20`
> >
> > *«24 de marzo y acá está la grabación, profe»* — el usuario experto, `1:47:44`
> >
> > *«¿Y todavía está activa, cierto?»* — el analista, `1:47:48` ·
> > *«Sí, profe»* — `1:47:51`
> >
> > *«esos videos siempre duran bastante tiempo ahí, incluso uno le puede
> > modificar el tiempo… porque eso tiene un límite que creo que son como **2 o
> > 3 meses, pero uno le puede ampliar**»* — el usuario experto, `1:48:13`
> >
> > *«será antes de la Semana Santa»* — `1:48:36` · *«eso fue antes de Semana
> > Santa, sí señor»* — `1:48:42`
>
> Y se compartió **por el chat de la reunión** (`1:47:59`), que sigue sin estar
> en el repositorio.
>
> **El plazo, entonces:** la sesión fue el **24 de marzo de 2026**; al 18 de
> junio ya estaba activa. Si el límite son dos o tres meses desde marzo y nadie
> lo amplió, **venció en junio**. Vale la pena mirarlo hoy mismo: se amplía en
> la misma página de la grabación.

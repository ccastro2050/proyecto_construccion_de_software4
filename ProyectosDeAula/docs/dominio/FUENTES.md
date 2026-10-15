# Las fuentes primarias — dónde están y qué contiene cada una

Los planes de esta carpeta citan documentos con un **orden de autoridad**, y
las historias de usuario citan minutos de una reunión. Este archivo dice
**dónde vive cada una de esas fuentes**, para que la cadena se pueda recorrer
hacia atrás: de la historia, a la cita; de la cita, al minuto; y del minuto, a
la grabación.

> **Ninguna se versiona en este repositorio**, y las razones están abajo. Lo que
> se versiona es este registro: una fuente citada y no localizable es una cita
> que nadie puede comprobar.

---

## 1 · La reunión — la fuente normativa

| | |
|---|---|
| **Qué es** | La sesión de elicitación completa, con el usuario experto |
| **Cuándo** | **18 de junio de 2026, 15:10** |
| **Duración** | ≈ 1 h 53 min (el último turno transcrito es `1:52:32`) |
| **Quiénes** | Profesor **el usuario experto Ruiz**, director del CIDEH — *usuario experto*. Profesor **el analista** — *conduce la sesión* |
| **Dónde** | Grabaciones de Teams, en el OneDrive institucional del director del CIDEH |

**Enlace** (requiere permiso institucional):
<https://usbmedellin-my.sharepoint.com/personal/direccion_cideh_usbmed_edu_co/Documents/Grabaciones/>
· archivo `Reunión de Teams-20260618_151004-Grabación de la reunión.mp4`

> **La fecha sale del nombre del archivo**, no de un dato suelto:
> `20260618_151004` es como Teams nombra sus grabaciones. Se deja anotado
> porque **ningún documento del proyecto decía cuándo fue la reunión**, y una
> cita sin fecha se vuelve inverificable el día que haya una segunda sesión.

### Su transcripción

| | |
|---|---|
| **Archivo** | `transcripcion_completa.txt` — reemplaza a `trascripción del video.txt` |
| **Dónde** | Carpeta de trabajo del proyecto, junto a los demás insumos |
| **Qué trae** | **570 turnos**, de `0:00:03` a `1:52:32` |

Dos cosas que hay que saber antes de citarla:

1. **Es automática.** Su primera línea lo advierte: *«El contenido generado por
   IA puede ser incorrecto»*. Confunde «cátedras» con «carteras», parte frases y
   a veces pega el nombre de la otra persona al final de un turno.
2. **Le faltaba un tramo, y se recuperó.** La primera exportación no tenía nada
   entre `1:07:41` y `1:49:13` — 41 min 32 s. Se recuperó de Stream el trozo
   `1:08:47`–`1:52:32` y se unió: de 445 turnos se pasó a **570**. **Queda un
   hueco de `1:31:41` a `1:49:13`: 17 min 32 s.**

> **El `PLAN-BD` §0.2 tenía razón en lo que suponía.** Decía que en ese tramo
> «con alta probabilidad se discutieron los informes y la parametrización», y
> así fue: aparecieron las estadísticas, la encuesta al asistente, los dos QR
> distintos y la separación de los dos roles. Está recogido en
> [`elicitacion/2_RESPUESTAS.md`](elicitacion/2_RESPUESTAS.md).
>
> **Los 17 minutos que aún faltan** (`1:31:41`–`1:49:13`) se sacan igual, de
> Stream. Ahí cae `1:47:35`, que es lo único que permitiría confirmar o
> descartar la advertencia de la sección 5.

> **Corrección al `PLAN-BD` §0.2.** Ese documento presenta el hueco como una
> limitación de la fuente —«queda un hueco de 39 minutos»—, y sitúa el reinicio
> en `1:46:42`. Son dos cosas distintas: en este `.txt` el hueco va de `1:07:41`
> a `1:49:13` (41 min 32 s, sin ningún turno antes de `1:49:13`), y **no es un
> hueco de la reunión sino de la exportación**.

---

## 2 · Los insumos normativos

En el orden de autoridad que fija
el plan de base de datos recibido *(ya no versionado)*, §0.1:

| # | Archivo | Tamaño | Qué aporta |
|---|---|---|---|
| 1 | `trascripción del video.txt` | 77 KB | La reunión: reglas del negocio y acuerdos. **Fuente normativa** |
| 2 | `USBME_LCONTROL_CATEDRAS_1076195337.xlsx` | 197 KB | La estructura real del evento: ID, reunión, descripción, tipo |
| 3 | `PRIMERA VERSIÓN Ideas software.docx` | 14 KB | El acta: el flujo, los dos roles, el QR con ventana, los cuatro informes |
| 4 | `Cätedras abiertas DIagrama Entidad -relación.xlsx` | 9 KB | El MER construido **en vivo** durante la reunión |

### De apoyo

| Archivo | Tamaño | Para qué |
|---|---|---|
| `Documentación de migración de asistencia de cátedra por archivo.pdf` | 779 KB | El proceso manual que se reemplaza, y los cinco estados del ASIS |
| `USBME_EMPLID_CATED_V1_1504983333.xlsx` | 353 KB | El maestro de personas que **va a ser reemplazado** |
| `Pedagogía Electoral(1-164).xlsx` | 32 KB | La calidad real de lo que hoy se captura |

---

## 3 · Lo que está en la carpeta y NO es de este proyecto

Conviene decirlo, porque estar en la misma carpeta no es pertenecer al mismo
proyecto:

| Archivo | Qué es |
|---|---|
| `Trabajo_final_análisis8_IES.ipynb` | Trabajo final del curso de IA de TalentoTech. Ya lo advierte el `PLAN-BD` §0.1 |
| `Historias de usuario micro proyecto gestión del desempeño V 2.5.docx` | De **otro** sistema: gestión del desempeño |
| `Plan de desarrollo sistema gestion de desempeño V.2.docx` | Del mismo otro sistema |
| `Cronograma Proyecto_Gestion_De_Investigación (1).xlsx` | De un tercer proyecto |

> **Ojo:** el primero de esos `.docx` **está también en la raíz de este
> repositorio**, y no se menciona en ningún documento. Sirvió de referencia de
> formato para redactar las historias de cátedras, pero eso no está escrito en
> ninguna parte — y un archivo de 4,6 MB sin explicación es una pregunta
> esperando a que alguien la haga.

---

## 4 · Por qué no se versionan aquí — **y ahora tampoco el material**

| Razón | A cuáles aplica |
|---|---|
| **Nombres de personas reales** | La transcripción, los planes recibidos, el maestro del sistema institucional |
| **Datos personales** | Los archivos de personas |
| **No es nuestro** | Lo entregó la institución; publicarlo no nos corresponde |

> **Desde el 17 de septiembre de 2026, la carpeta `material_dado/` tampoco se
> versiona.** Lo pidió el usuario experto: **sin nombres ni datos reales, solo
> hipotéticos** — los reales los carga él cuando reciba el sistema.
>
> **Se sacó del repositorio, no se editó.** Un material recibido es un
> **registro**: editarlo lo convierte en algo que ya no prueba qué se entregó.
> Sigue en el computador de quien lo recibió, y **este archivo es lo que queda
> en su lugar**.

### Lo que contenía, y dónde vive ahora lo que hacía falta

| Qué había en `material_dado/` | Dónde está ahora |
|---|---|
| Las **38 reglas de negocio** (`RN-01` a `RN-38`) | [`REGLAS_DE_NEGOCIO.md`](REGLAS_DE_NEGOCIO.md) — **trasladadas con un script**, no copiadas a mano |
| El modelo entidad-relación, el relacional y el físico | El resultado está en `db/init.sql`; el método, en [`../conceptos/CONCEPTOS_DISENO_BD.md`](../conceptos/CONCEPTOS_DISENO_BD.md) |
| Los planes de base de datos y de API recibidos | Lo que decidían quedó en las decisiones `D-vN-n` de cada versión |
| El maestro de personas del sistema institucional | **Nunca entró.** Los datos sembrados son hipotéticos — ver [`DATOS_DE_PRUEBA.md`](DATOS_DE_PRUEBA.md) |

> **Lo que se perdió al sacarlo, dicho sin adornos:** ya no se puede ir **de la
> cita al documento que la contiene**. La trazabilidad hacia atrás llega hasta
> la marca de tiempo y ahí se detiene. Es el precio, y se paga a conciencia.

## 5 · Una advertencia que no se pudo comprobar

El `PLAN-BD` §0.2 afirma que en `1:47:35` el usuario experto comparte por el
chat la grabación de **otra sesión, del 24 de marzo** —*«antes de Semana
Santa»*—, y advierte que esas grabaciones **caducan a los dos o tres meses**
salvo que se amplíe el plazo.

**Comprobado: el plan tenía razón.** Recuperada la transcripción completa, el
minuto `1:47:35` existe y dice exactamente eso. Más aún, el intercambio
completo (`1:46:20`–`1:48:42`) confirma los cuatro datos: que la sesión fue el
**24 de marzo**, antes de Semana Santa; que al 18 de junio la grabación
**seguía activa**; que se compartió **por el chat de la reunión**; y que el
límite son *«como 2 o 3 meses, pero uno le puede ampliar»*.

*(En una versión anterior de este archivo yo había escrito que ese minuto no
existía. Era un artefacto de la exportación incompleta, no un error del plan.)*

**Y el tiempo corre en contra.** La sesión fue el **24 de marzo de 2026**. El
18 de junio la grabación seguía activa, pero el propio usuario experto dijo que
el límite son dos o tres meses **salvo que se amplíe**. Contando desde marzo,
ese plazo **venció en junio**, y nadie dijo haberlo ampliado.

Es la única fuente primaria que todavía puede perderse por no hacer nada — y se
amplía desde la misma página de la grabación, en dos clics. **Conviene mirarlo
hoy.**

# Requisitos funcionales — Cátedras Abiertas

> **Qué es este archivo.** El catálogo completo de lo que el sistema **hace**,
> a lo largo de sus cuatro versiones. Es el índice; el contrato de cada versión
> está en su `2_spec`.
>
> **Cómo se escribe uno y cómo se comprueba el catálogo:**
> [`../conceptos/CONCEPTOS_REQUISITOS.md`](../conceptos/CONCEPTOS_REQUISITOS.md).
>
> Versión 1.0 · 14 de septiembre de 2026.

---

## 0. Cómo leer este catálogo

| Columna | Qué dice |
|---|---|
| **ID** | `RF-nn`. **Estable**: no cambia al pasar de versión, y **no se reutiliza** aunque el requisito se derogue |
| **Origen** | La historia de usuario de la que sale. Sin origen, el requisito no se puede priorizar |
| **Restringido por** | Las reglas de negocio `RN-nn` que le aplican. Están en `PLAN-BD-CATEDRAS-ABIERTAS.md` |
| **Versión** | En cuál se realiza |
| **Estado** | `acordado` · `construido` · `verificado` · `fuera de alcance` |

> **Regla del catálogo:** ningún requisito se queda sin versión asignada. Si no
> entra en ninguna, se marca **`fuera de alcance`** y se dice por qué. Un
> requisito en silencio es uno que nadie está construyendo **y nadie sabe que
> nadie está construyendo**.

### Aviso sobre los datos

> **Todos los datos de este proyecto son hipotéticos.** Los nombres, correos y
> documentos de los ejemplos están inventados; las cifras de volumen se dan
> **redondeadas y sin identificar la fuente**. Es una condición del usuario
> experto: el sistema se entrega con datos de ejemplo, y **los reales los carga
> él** cuando lo reciba. Ver el Artículo 8 de la constitución.

---

## 1. Versión 1 — los diez catálogos sin clave foránea

**Origen:** historias 1 a 4 y 9. **Estado del conjunto:** construido y
verificado.

Los diez recursos son `sede`, `modalidad`, `tipo_evento`, `tipo_documento`,
`tipo_vinculacion`, `tipo_pregunta`, `estado_proceso`, `rol`,
`periodo_academico` y `encuesta`.

| ID | Requisito | Origen | Restringido por | Versión | Estado |
|---|---|---|---|---|---|
| **RF-01** | El sistema **lista** las fichas **activas** de un catálogo, con las columnas de esa tabla, y acepta un `limite` opcional | H1 | RN-32 | v1 | verificado |
| **RF-02** | El sistema **entrega una** ficha por su código | H1, H3 | — | v1 | verificado |
| **RF-03** | El sistema **agrega** una ficha con los campos obligatorios de esa tabla | H2 | RN-09, RN-11 | v1 | verificado |
| **RF-04** | El sistema **reemplaza por completo** una ficha, y rechaza el reemplazo si falta un campo obligatorio | H3 | — | v1 | verificado |
| **RF-05** | El sistema **modifica parcialmente** una ficha, cambiando solo los campos enviados | H3 | — | v1 | verificado |
| **RF-06** | El sistema **retira del uso** una ficha **en los catálogos que admiten baja**, sin borrarla | H4 | **RN-32** | v1 | verificado |
| **RF-07** | El sistema **no ofrece la operación de baja** en `rol`, `estado_proceso` y `tipo_pregunta` | H4 | RN-30 | v1 | verificado |
| **RF-08** | El sistema **publica su contrato** de forma legible: cada recurso con su nombre y cada cuerpo con su esquema | — | — | v1 | verificado |
| **RF-09** | El sistema **distingue las vinculaciones que no exigen programa** de las que sí | **H9** | **RN-07** | v1 | verificado |

> **RF-07 merece una nota, porque es la decisión más fina de esta versión.**
> No dice «el sistema rechaza la baja»: dice que **la operación no existe**. Es
> el esquema el que lo decidió, no el equipo — esas tres tablas no tienen
> columna de estado. Y se expresa de la forma más fuerte posible: la interfaz
> `IServicioRol` **no declara** `Eliminar`, así que intentarlo **no compila**.
>
> **RF-09 es la excepción de Rutas de Paz**, y en la v1 solo entra el dato: el
> catálogo `tipo_vinculacion` con su bandera `exige_programa`. El registro que
> la usa llega en la v4.

---

## 2. Versión 2 — las tablas con clave foránea

**Origen:** historia 5. **Estado:** acordado, sin construir.

| ID | Requisito | Origen | Restringido por | Versión | Estado |
|---|---|---|---|---|---|
| **RF-10** | El sistema **programa una cátedra** con su identificador del ASIS, su tipo de evento y su periodo | H5 | RN-09, RN-11 | v2 | acordado |
| **RF-11** | El sistema **agrega sesiones** a una cátedra, numeradas **de corrido desde 1** dentro de esa cátedra | H5 | **RN-10** | v2 | acordado |
| **RF-12** | El sistema **admite cátedras simultáneas en sedes distintas**, y no lo trata como un choque | H5 | **RN-12** | v2 | acordado |
| **RF-13** | El sistema **rechaza una sesión cuyo fin sea anterior a su inicio** | H5 | RN-37 | v2 | acordado |
| **RF-14** | El sistema **ofrece las llaves foráneas como lista desplegable**, cargada de la API: no se digitan | H5 | — | v2 | acordado |
| **RF-15** | El sistema **registra a un asistente como ponente** de una sesión, con su función | H5 | — | v2 | acordado |

> **RF-14 es lo que esta versión enseña**, y por eso es un requisito y no un
> detalle de pantalla: **la integridad referencial se cumple en la interfaz o
> no se cumple**. Un campo de texto donde el usuario escribe el código de la
> sede produce un error que la base de datos rechaza y el usuario no entiende.

---

## 3. Versión 3 — el acceso

**Origen:** historia 10. **Estado:** acordado, sin construir.

| ID | Requisito | Origen | Restringido por | Versión | Estado |
|---|---|---|---|---|---|
| **RF-16** | El sistema **autentica** a un usuario contra su credencial guardada | H10 | **RN-34** | v3 | acordado |
| **RF-17** | El sistema **asigna y revoca roles** a un usuario, conservando el histórico | H10 | **RN-31**, RN-32 | v3 | acordado |
| **RF-18** | El sistema **niega en el servicio** toda operación que el rol no permita, **aunque la pantalla no muestre el botón** | H10 | — | v3 | acordado |
| **RF-19** | El sistema **limita al coordinador a su propia dependencia** | H10 | — | v3 | acordado |

> **RF-18 es el requisito que da sentido a la versión.** Esconder un menú **no
> protege nada**: mientras el control no esté en el servicio, quien escriba la
> URL entra igual. Se enuncia así a propósito, para que su criterio de
> aceptación sea **llamar la ruta directamente sin pasar por la pantalla**.
>
> **El nombre del rol principal está SIN DECIDIR** — ver `D-GLO-1` en el
> [glosario](GLOSARIO.md). Mientras tanto estos requisitos dicen «administrador»,
> que es lo que quedó escrito, **no lo que la fuente corrigió**.

---

## 4. Versión 4 — el aplicativo

**Origen:** historias 6, 7, 8 y 9. **Estado:** acordado, sin construir.

| ID | Requisito | Origen | Restringido por | Versión | Estado |
|---|---|---|---|---|---|
| **RF-20** | El sistema **publica un punto de entrada por sesión**, como QR si es presencial y como enlace si es virtual | H6 | **RN-13**, RN-14 | v4 | acordado |
| **RF-21** | El sistema **admite registros solo dentro de la ventana** de la sesión, y la ventana **se puede cambiar mientras la cátedra ocurre** | H6 | **RN-15**, RN-36 | v4 | acordado |
| **RF-22** | El sistema **identifica al asistente** por código, documento o correo, indistintamente | H6 | **RN-06** | v4 | acordado |
| **RF-23** | El sistema **envía una clave al correo** antes de aceptar el registro | H6 | **RN-17**, RN-18, RN-20 | v4 | acordado |
| **RF-24** | El sistema **admite un registro por asistente y sesión**, y no más | H6 | **RN-21** | v4 | acordado |
| **RF-25** | El sistema **guarda copia del programa y la vinculación** que el asistente tenía **en ese instante** | H6 | **RN-22** | v4 | acordado |
| **RF-26** | El sistema **marca el correo que no es institucional**, sin rechazarlo | H6 | **RN-19** | v4 | acordado |
| **RF-27** | El sistema **genera el archivo plano del ASIS** con exactamente tres columnas: reunión, identificador y programa | H7 | **RN-27** | v4 | acordado |
| **RF-28** | El sistema **excluye del archivo plano** a quien no tenga identificador o no tenga programa | H7 | **RN-08**, RN-28 | v4 | acordado |
| **RF-29** | El sistema **deja evidencia de cada carga y de cada envío**: cuántas filas entraron, cuántas fallaron y por qué | H7 | RN-29, RN-30 | v4 | acordado |
| **RF-30** | El sistema **informa cuántos externos ingresaron en un periodo** | H8 | — | v4 | acordado |
| **RF-31** | El sistema **registra a quien no tiene programa** cuando su vinculación no lo exige | **H9** | **RN-07** | v4 | acordado |
| **RF-32** | El sistema **presenta una encuesta por registro**, como máximo una | H6 | RN-23, RN-24 | v4 | acordado |
| **RF-33** | El sistema **registra el consentimiento de datos** como un hecho, con su fecha | — *(Ley 1581 de 2012)* | **RN-33** | v4 | acordado |
| **RF-34** | El sistema **deja en bitácora** toda modificación sobre un registro de asistencia, con el antes y el después | — | **RN-35** | v4 | acordado |

> **RF-28 es el que nadie ve venir.** El asistente externo **se registra y se
> cuenta**, pero **nunca entra al archivo plano** — porque el ASIS no lo
> reconoce. Es una regla del sistema de destino, no de este, y por eso está
> escrita: alguien que no lo sepa va a «arreglar» la exportación.
>
> **RF-33 no tiene historia de origen, y eso es correcto.** Sale de la Ley 1581
> de 2012, no de un interesado. Las obligaciones legales son requisitos aunque
> nadie las pida.

---

## 5. Fuera de alcance, y por qué

Se escriben para que la ausencia sea **una decisión** y no un olvido.

| Requisito que NO se hace | Por qué |
|---|---|
| Notificar por correo el resultado de la migración | Nadie lo pidió, y agrega un canal que hay que operar |
| Cupo máximo por sesión (`RN-38`) | La regla existe en el modelo, pero **ninguna historia la pide**. Se deja preparada, sin realizar |
| Borrado físico de cualquier registro | Lo prohíbe `RN-32` y el Artículo 6 de la constitución |
| Autenticación federada con la universidad | Fuera del alcance del ejercicio |

---

## 6. Matriz de trazabilidad

**Hacia adelante** — *¿lo que se pidió está cubierto?*

| Historia | Requisitos | Versión |
|---|---|---|
| H1 · Consulta del catálogo | RF-01, RF-02 | v1 |
| H2 · Registro de una ficha | RF-03 | v1 |
| H3 · Corrección | RF-02, RF-04, RF-05 | v1 |
| H4 · Retiro | RF-06, **RF-07** | v1 |
| H5 · Programación de cátedra y sesiones | RF-10 a RF-15 | v2 |
| H6 · Registro de la propia asistencia | RF-20 a RF-26, RF-32 | v4 |
| H7 · Archivo plano del ASIS | RF-27, RF-28, RF-29 | v4 |
| H8 · Informe de externos | RF-30 | v4 |
| H9 · Excepción de Rutas de Paz | **RF-09** (el dato, v1) · **RF-31** (el registro, v4) | v1 y v4 |
| H10 · Control de acceso por rol | RF-16 a RF-19 | v3 |

**Cobertura: 10 de 10 historias firmadas.** Ninguna se quedó sin requisito.

**Hacia atrás** — *¿lo que se hizo lo pidió alguien?*

Todos los `RF-nn` tienen historia de origen **salvo dos**, y los dos están
justificados:

- **RF-08** (publicar el contrato) — sale del Artículo 10.1 de la constitución,
  no de un interesado.
- **RF-33** (consentimiento) — sale de la Ley 1581 de 2012.

**Requisitos sin versión asignada: 0.**

### Las historias propuestas, que todavía no están aquí

Las historias **11 a 15** —de `../dominio/elicitacion/3_HISTORIAS_PROPUESTAS.md`—
**no generan requisitos todavía**, porque **no están firmadas**. Cuando el
usuario experto las firme, entran como `RF-35` en adelante.

> **Esto es a propósito.** Un requisito derivado de una historia sin firmar es
> un requisito que alguien se inventó con buena letra.

---

## 7. Cómo se comprueba este catálogo

```bash
# 1 · Toda historia firmada tiene al menos un RF.
grep -c "^### Historia de Usuario" docs/spec_kit/0_historias_de_usuario.md   # 10
grep -oE "H[0-9]+" docs/dominio/REQUISITOS_FUNCIONALES.md | sort -u          # H1..H10

# 2 · Ningún RF cita una regla que no existe.
python pruebas_humo/auditar_documentacion.py      # comprobación 2

# 3 · Ningún RF sin versión: la tabla no tiene celdas vacías en esa columna.
```

---

## 8. Historial

| Versión | Fecha | Qué cambió |
|---|---|---|
| 1.0 | 14 sep 2026 | Primera versión. **34 requisitos** sobre las cuatro versiones, derivados de las 10 historias firmadas y de las 38 reglas de negocio. Cobertura 10/10; 0 requisitos sin versión. Las historias 11 a 15 quedan fuera hasta que se firmen |

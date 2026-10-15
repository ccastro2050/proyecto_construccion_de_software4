# Reglas de negocio — Cátedras Abiertas

> **Qué es este archivo.** Las **38 reglas** que son verdad **siempre** en este
> dominio, sin importar qué función se ejecute. No son requisitos: un requisito
> es algo que el sistema **hace**; una regla es algo que **no puede dejar de
> ser cierto**.
>
> **Qué es una regla de negocio y en qué se diferencia de un requisito:**
> [`../conceptos/CONCEPTOS_REGLAS_DE_NEGOCIO.md`](../conceptos/CONCEPTOS_REGLAS_DE_NEGOCIO.md).
>
> Versión 1.0 · 14 de septiembre de 2026.

---

## 0. De dónde salen, y por qué están aquí ahora

Estas reglas **no se elicitaron en este documento**: llegaron con el material
que entregó el usuario experto, en `PLAN-BD-CATEDRAS-ABIERTAS.md`, ya
numeradas y trazadas. **Se trasladan aquí tal cual**, extraídas con un script y
no copiadas a mano, porque el material recibido deja de versionarse en este
repositorio.

> **Y hay que decir lo que eso significa para la metodología:** en este
> proyecto las reglas **vinieron dadas**. Lo normal es lo contrario — que haya
> que hacerlas salir de la entrevista, una por una, y que nadie las diga
> completas. El conceptual explica cómo se producen cuando **no** vienen dadas,
> que es el caso habitual.

### Cómo leer la tabla

| Columna | Qué dice |
|---|---|
| **#** | `RN-nn`. Estable y **no se reutiliza** |
| **Regla** | Lo que tiene que ser cierto siempre |
| **Origen** | `Dn` una decisión de la reunión · `Hn` una historia · una marca de tiempo · `.docx` o `PDF` el material entregado |
| **Mecanismo** | **Dónde se defiende**: el esquema, un disparador o un procedimiento |

> **La marca ◆ señala las que salen textualmente de la reunión**, no de una
> deducción de quien modeló. Son **catorce de las treinta y ocho**, y son las
> que no se pueden negociar sin volver a preguntar.

---

## 1. Las 38 reglas

| # | Regla | Origen | Mecanismo |
|---|---|---|---|
| **RN-01** ◆ | Nadie se registra si no está en la tabla del ASIS | D1, D2, D3 | Clave foránea + procedimiento de acceso |
| **RN-02** ◆ | El `id_asis` es único y es la clave candidata natural del asistente | D1 | `UNIQUE` |
| **RN-03** | El `id_asis` es alfanumérico de 6 a 15 caracteres | H8, H9 | Dominio + `CHECK` |
| **RN-04** | Un número de documento identifica a lo sumo un asistente | H10 | `UNIQUE (tipo, numero)` |
| **RN-05** | Un asistente puede tener varios documentos, **solo uno vigente** | H10 | Índice único parcial |
| **RN-06** ◆ | El acceso se puede intentar por **código, documento o correo**, indistintamente | D11, `57:20` | Tres índices únicos + función de resolución |
| **RN-07** ◆ | El programa es obligatorio salvo para las vinculaciones que no lo exigen | D7 | **`CHECK` condicional** contra `tipo_vinculacion.exige_programa` |
| **RN-08** ◆ | El asistente externo se registra y se cuenta, pero **nunca entra al archivo plano** | D5, D6 | Vista + `WHERE` en la exportación |
| **RN-09** | El ID de evento del ASIS son 9 caracteres con ceros a la izquierda | H1 | `char(9)` + `CHECK` |
| **RN-10** ◆ | El número de reunión es consecutivo por cátedra y empieza en 1 | H4, D16 | `UNIQUE` + función `fn_siguiente_reunion` |
| **RN-11** | El nombre corto que viaja al ASIS se **deriva** a 30 caracteres | H2 | Columna generada |
| **RN-12** ◆ | Pueden existir cátedras simultáneas en sedes distintas | D14 | Sin restricción de exclusión entre sedes; **sí** dentro de la misma |
| **RN-13** ◆ | Una sesión presencial se difunde por QR; una virtual, por enlace | D15 | `CHECK` entre `modalidad` y `enlace_registro.canal` |
| **RN-14** | Cada sesión tiene a lo sumo un enlace vigente | — | Índice único parcial |
| **RN-15** | Solo se admiten registros dentro de la ventana | `.docx` | `CHECK` sobre el rango + disparador |
| **RN-16** | El token es aleatorio, único y no adivinable | `.docx` | `gen_random_uuid()` / `pgcrypto` |
| **RN-17** ◆ | La clave se envía al correo **para impedir que uno registre a otro** | `1:04:51` | Procedimiento de emisión |
| **RN-18** | La clave se guarda **solo cifrada**, expira y admite un máximo de intentos | — | `digest()` + `CHECK` + disparador |
| **RN-19** ◆ | Si el correo no es institucional, **queda marcado** | `55:32`, D8 | Columna `es_correo_institucional` derivada por disparador |
| **RN-20** | Una clave usada no se reutiliza | — | Disparador |
| **RN-21** ◆ | Un asistente se registra **a lo sumo una vez** por sesión | `1:04:51` | `UNIQUE (sesion, asistente)` |
| **RN-22** ◆ | El registro guarda **copia** del programa y la vinculación del momento | D12 | Disparador de derivación |
| **RN-23** | Una encuesta por registro, como máximo | `.docx` | `UNIQUE` |
| **RN-24** | Solo se responden preguntas de la encuesta de esa sesión | — | Clave foránea compuesta |
| **RN-25** | El tipo de pregunta determina la columna de la respuesta | — | **`CHECK` condicional** |
| **RN-26** | Toda pregunta obligatoria debe tener respuesta | — | **No declarativa.** Procedimiento de cierre |
| **RN-27** ◆ | El archivo plano lleva **exactamente tres columnas**: reunión, ID, programa | D9, PDF | Función de exportación, probada carácter a carácter |
| **RN-28** ◆ | Solo se migran registros con ID y con programa | D9, D12 | Vista + procedimiento |
| **RN-29** | Un registro puede intentarse varias veces pero solo queda **aceptado en un lote** | — | Índice único parcial |
| **RN-30** | Los estados del proceso del ASIS son los cinco del manual | PDF | Clave foránea |
| **RN-31** | Un usuario puede volver a tener un rol que ya tuvo | — | **`fecha_ini` en la clave primaria** |
| **RN-32** | Nadie se elimina: baja lógica | — | `activo` + revocación de `DELETE` |
| **RN-33** | Todo dato personal exige consentimiento vigente | Ley 1581 | Vista de control |
| **RN-34** | Ninguna clave en claro | — | Revisión de esquema |
| **RN-35** | Toda modificación de un registro de asistencia queda en bitácora | — | Disparador `AFTER` |
| **RN-36** | Dos enlaces de la misma sesión no se solapan en el tiempo | — | **`EXCLUDE USING gist`** |
| **RN-37** | El fin de una sesión no puede ser anterior a su inicio | — | `CHECK` |
| **RN-38** | Si la sesión tiene cupo, no se admiten más registros | — | **No declarativa.** Disparador con bloqueo |

**Reparto por dónde se defiende cada una:** ~18 declarativas —el esquema solo
las hace cumplir— · ~5 parciales · ~15 que necesitan **un disparador, una
función o un procedimiento**, porque no caben en una restricción.

---

## 2. Las que NO se pueden declarar, y por qué importa

Quince de las treinta y ocho **no caben en el esquema**. Vale la pena mirar tres,
porque explican por qué el modelo de datos no basta:

| Regla | Por qué no cabe |
|---|---|
| **RN-26** · toda pregunta obligatoria debe tener respuesta | Es una condición sobre **el conjunto** de respuestas, y solo se puede evaluar **al cerrar** la encuesta. Ninguna fila sola la viola |
| **RN-38** · si la sesión tiene cupo, no se admiten más registros | Depende de **contar** lo que ya hay, y entre contar e insertar cabe otra transacción. Necesita bloqueo |
| **RN-15** · solo se admiten registros dentro de la ventana | La ventana **se puede cambiar mientras la cátedra ocurre**, así que el `CHECK` tendría que leer otra tabla |

> **Esto es lo que separa un modelo de datos de un sistema.** Una regla que
> necesita disparador es una regla que alguien tiene que **acordarse** de
> programar — y si no está escrita como regla, nadie se acuerda.

---

## 3. Dónde vive cada regla: la decisión que hay que tomar

Toda regla se puede defender en tres sitios, y **elegir mal se paga**:

| Dónde | Ventaja | Costo |
|---|---|---|
| **La base de datos** | Nadie la puede saltar, ni por error ni a propósito | El mensaje de error es ilegible para el usuario |
| **El servicio** | El error se puede explicar en español | Quien entre por otro camino se la salta |
| **La pantalla** | El usuario lo ve antes de enviar | **No protege nada**: es comodidad, no control |

**La regla del proyecto, y está en la constitución:** lo que protege la
integridad va en **la base de datos**; lo que explica va en **el servicio**; la pantalla
**ayuda pero nunca es la única defensa**.

> **El caso que lo ilustra es `RN-07`** —el programa es obligatorio salvo para
> las vinculaciones que no lo exigen—. Está en la base de datos como `CHECK`
> condicional contra `tipo_vinculacion.exige_programa`. **Si estuviera solo en
> la pantalla**, cualquier carga masiva la saltaría — y las cargas masivas son
> exactamente por donde entran los datos aquí.

---

## 4. Cómo se comprueba

```bash
# 1 · Ninguna regla citada en el spec kit o en los requisitos deja de existir.
python pruebas_humo/auditar_documentacion.py      # comprobación 2

# 2 · Las reglas que el esquema defiende, están en el esquema.
grep -c "CHECK (" db/init.sql        # 47 restricciones
grep -ci "CREATE TRIGGER" db/init.sql   # 7 disparadores
grep -ci "CREATE OR REPLACE FUNCTION" db/init.sql   # 16 funciones
```

> **La segunda comprobación es floja a propósito**, y conviene decirlo: cuenta
> mecanismos, no comprueba que cada regla tenga el suyo. **La trazabilidad
> regla → mecanismo está pendiente**, y es lo primero que habría que agregar.

---

## 5. Historial

| Versión | Fecha | Qué cambió |
|---|---|---|
| 1.0 | 14 sep 2026 | Traslado de las 38 reglas desde el material recibido, extraídas con script y no copiadas a mano. Se agregan las secciones 2, 3 y 4, que no existían: qué reglas no se pueden declarar, dónde vive cada una y cómo se comprueba |

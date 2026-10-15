# Datos de prueba — de dónde salió cada fila

> **Qué es este archivo.** El registro de **procedencia** de los datos con que
> arranca el sistema: cuáles se derivaron de una fuente, cuáles se inventaron,
> y **cuáles se inventaron a propósito para que una regla se pueda probar**.
>
> **Por qué existe.** Una prueba que pasa con datos inventados sin decirlo no
> prueba nada. Y un dato sembrado sin procedencia es un dato que alguien va a
> tomar por real.
>
> Versión 1.0 · 14 de septiembre de 2026.

---

## 1. La regla, antes del inventario

> **Ningún dato de persona real entra a este repositorio** (Artículo 8 de la
> constitución, `RNF-18`).
>
> **Y desde el 14 de septiembre de 2026, una condición más del usuario
> experto:** **nombres y datos hipotéticos, no reales**. El sistema se entrega
> con datos de ejemplo; **los reales los carga él** cuando lo reciba.

Las tres procedencias posibles, y las tres se declaran:

| Marca | Qué quiere decir |
|---|---|
| **derivado** | Sale de una fuente real, pero **sin identificar a nadie**: un catálogo, un código, una lista cerrada |
| **inventado** | Lo escribió el equipo. **Verosímil, y falso** |
| **inventado para probar** | Existe **solo** para que una regla concreta se pueda ejercitar. Es el más importante de los tres |

---

## 2. El inventario, contado del propio `init.sql`

**97 filas en 26 tablas.** Contadas con script, no a ojo.

### Los catálogos — **derivados**

| Tabla | Filas | De dónde |
|---|---|---|
| `tipo_documento` | 6 | Lista cerrada de tipos de documento de identidad |
| `tipo_vinculacion` | 7 | Las vinculaciones institucionales, **incluida la que no exige programa** |
| `tipo_evento` | 8 | Los códigos de clasificación del sistema institucional |
| `estado_proceso` | 6 | Los cinco estados del monitor de procesos, **más uno propio** |
| `tipo_pregunta` | 4 | Las clases de pregunta de una encuesta |
| `rol` | 3 | Los roles del sistema |
| `modalidad` | 4 | Presencial, virtual y mixtas |
| `periodo_academico` | 4 | Periodos con la forma `AAAA-N` |
| `sede` | 3 | Dos físicas y **una virtual**, que es la que importa |
| `parametro` | 9 | La configuración que se cambia sin tocar código |

> **Los catálogos son los únicos derivados**, y la razón es que **no
> identifican a nadie**: «CAAB» es un código de evento, no una persona. Aun
> así, los volúmenes del material original se dan **redondeados** en el resto
> de la documentación, sin la cifra exacta.

### La estructura académica — **inventada**

| Tabla | Filas | Nota |
|---|---|---|
| `facultad` | 9 | Árbol de dos niveles. Los nombres son **inventados** |
| `dependencia` | 8 | Igual, y sirve para probar el rol de coordinador |
| `programa_academico` | 3 | Tres programas, **con las tres formas de código** que el modelo admite |

### Las personas — **inventadas, todas**

| Tabla | Filas | Quiénes |
|---|---|---|
| `asistente` | **8** | Ocho personas que **no existen**. Nombres genéricos a propósito: `Administrador Catedras`, `Participante Rutas de Paz`, `Publico Externo` |

> **Los nombres son deliberadamente antinaturales.** No dicen «Juan Pérez»
> —que parece una persona— sino lo que la fila **está probando**. Quien lea la
> base ve de una que son datos de ejemplo, y nadie los confunde con reales.

### Los hechos — **inventados para probar**

| Tabla | Filas | Qué regla ejercita |
|---|---|---|
| `catedra` | 3 | Cátedras con sus identificadores |
| `enlace_registro` | 4 | `RN-13`: presencial por QR, virtual por enlace |
| `clave_acceso` | 3 | `RN-17`, `RN-18`, `RN-20`: la clave cifrada, que expira y no se reutiliza |
| `registro_asistencia` | 1 | `RN-21` y `RN-22`: uno por sesión, con copia del programa del momento |
| `respuesta_encuesta` | 1 | `RN-23`: una encuesta por registro |
| `lote_migracion` | 1 | `RN-29` y `RN-30`: el envío con su estado |
| `bitacora` | 1 | `RN-35`: la modificación con su antes y su después |
| `encuesta` | 1 | La encuesta versionada |

> **Estas ocho filas son las que más valen y las que menos se notan.** No están
> para que la pantalla «se vea llena»: cada una existe **porque sin ella una
> regla no se puede ejercitar**. Si alguien las borra por parecer ruido, ocho
> reglas dejan de estar probadas y nada falla.

---

## 3. Lo que hay que decidir: los dominios de correo

**Esto está sin resolver y no lo voy a resolver solo.**

Los correos sembrados usan **dominios reales**:

| Dominio | Dónde | Por qué está |
|---|---|---|
| `@ejemplo.com`, `@ejemplo.edu.co` | La mayoría | Dominios reservados para ejemplos. **Correcto** |
| **`@usbmed.edu.co`** | 1 fila | Es el dominio institucional **real** |
| **`@gmail.com`** | 3 filas | Representa un correo **no** institucional |

**El problema:** `RN-19` dice que *si el correo no es institucional, queda
marcado*. Para ejercitar esa regla hacen falta las dos clases de correo — y
«institucional» aquí significa **ese dominio concreto**.

**Las tres salidas, y ninguna es obvia:**

1. **Dejarlos.** No identifican a nadie: `admin.catedras@usbmed.edu.co` es una
   dirección inventada en un dominio real. La regla queda probada de verdad.
2. **Sustituirlos** por `@ejemplo.edu.co` y `@ejemplo.com`, y hacer que la
   regla compare contra un dominio **configurable** —que es lo que debería ser
   de todos modos—. Más limpio, y **mejora el diseño**.
3. **Dejar el institucional y cambiar `@gmail.com`** por `@ejemplo.com`.

> **Mi recomendación es la 2**, y no por la anonimización: porque **el dominio
> institucional quemado en el código es un defecto**. El día que la universidad
> cambie de dominio, o el sistema se use en otra sede, hay que recompilar. Debe
> ser un `parametro`, que es justo la tabla que existe para esto.

---

## 4. Lo que estos datos **no** prueban

Tan importante como lo que sí:

| No se prueba | Por qué | Qué RNF queda sin medir |
|---|---|---|
| **El volumen** | 97 filas no dicen nada del comportamiento con decenas de miles | **`RNF-03`** (desempeño) y **`RNF-04`** (capacidad) |
| **La suciedad de los datos reales** | Aquí todo viene limpio; en la realidad llegan nombres cortados, programas mal escritos, celdas con dos cosas | La utilidad real de `alias_programa` |
| **La concurrencia** | Una fila insertada una vez no ejercita `RN-38` ni el bloqueo | El 409 que la política de errores declara pendiente |

> **La primera es la que hay que tener presente al leer los umbrales.** Los
> seis requisitos marcados «propuesto» en
> [`REQUISITOS_NO_FUNCIONALES.md`](REQUISITOS_NO_FUNCIONALES.md) **no se pueden
> validar con estos datos**. Cuando el usuario experto cargue los reales, hay
> que volver a medir — y esos umbrales son los primeros que se van a mover.

---

## 5. Cómo se regeneran

```bash
# Desde cero: borra el volumen y vuelve a correr init.sql completo.
docker compose down -v
docker compose up -d --build

# Comprobar que quedaron las 97 filas esperadas.
python pruebas_humo/humo_api.py
```

> **`down -v` borra el volumen**, y esa es la diferencia con `down` a secas.
> Sin `-v`, PostgreSQL ve una base ya inicializada y **no vuelve a correr
> `init.sql`** — así que los cambios en el script no se aplican y parece que no
> funcionaron.

---

## 6. Cómo se comprueba la procedencia

```bash
# 1 · Ningún correo fuera de los dominios declarados en §3.
grep -oE "@[a-zA-Z0-9._-]+" db/init.sql | sort -u

# 2 · El recuento por tabla, contado y no supuesto.
grep -cE "^INSERT INTO" db/init.sql
```

> **Pendiente, y es lo que cerraría este documento:** que el recuento de §2 se
> compruebe solo, comparando la tabla de este archivo contra lo que
> `init.sql` siembra de verdad. Hoy se contó con un script **una vez**; si
> mañana alguien agrega una fila, este documento queda desactualizado en
> silencio.

---

## 7. Historial

| Versión | Fecha | Qué cambió |
|---|---|---|
| 1.0 | 14 sep 2026 | Primera versión. Inventario de las 97 filas contado del propio `init.sql`, con su procedencia declarada en tres clases. Se deja **sin resolver a propósito** la decisión de §3 sobre los dominios de correo, con una recomendación |

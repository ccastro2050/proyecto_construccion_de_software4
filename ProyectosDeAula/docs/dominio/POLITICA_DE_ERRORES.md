# Política de errores — Cátedras Abiertas

> **Qué es este archivo.** Qué código de estado devuelve el sistema ante cada
> clase de fallo, qué forma tiene el cuerpo, y **quién decide cada uno**. Es
> una **política**: rige en todas las versiones y no se renegocia en cada
> `6_contracts`.
>
> **Por qué existe:** sin ella, cada versión inventa sus códigos. La v1 y la v2
> del mismo sistema acaban respondiendo distinto al mismo error, y el cliente
> no puede escribir una sola rutina de manejo.
>
> Versión 1.0 · 14 de septiembre de 2026.

---

## 1. Los dos sobres, y ninguno más

```json
{ "estado": 400, "mensaje": "Parámetros inválidos.", "detalle": "..." }
{ "estado": 422, "mensaje": "Datos inválidos.",      "errores": ["...", "..."] }
```

**El primero para un error; el segundo para una lista.** No hay un tercero.

| Campo | Siempre | Para qué |
|---|---|---|
| `estado` | sí | El mismo número que la cabecera HTTP. **Se repite a propósito**: un cliente que solo lee el cuerpo no se queda sin saberlo |
| `mensaje` | sí | Una frase **en español**, entendible por una persona, terminada en punto |
| `detalle` | solo en el sobre de uno | Qué falló exactamente |
| `errores` | solo en el sobre de lista | Un elemento por campo que falló |

> **Por qué en español y no `ProblemDetails`.** El estándar de .NET responde en
> inglés y con un vocabulario del framework —*«The Nombre field is
> required.»*—. Este sistema lo lee gente que no programa, y `RNF-09` prohíbe
> la jerga en pantalla. Si el error llega en inglés, la pantalla tiene que
> traducirlo, y traducir mensajes de otro sistema es una fuente de errores
> permanente.

---

## 2. La tabla de códigos

| Código | Cuándo | **Quién lo decide** |
|---|---|---|
| **200** | La operación se hizo | El controlador |
| **201** | Se creó un recurso, y se devuelve dónde quedó | El controlador |
| **400** | La **forma** está bien, pero **una regla no se cumple**: un `PATCH` sin campos, un `limite` en cero, un código vacío en la ruta | El **servicio** |
| **404** | El recurso no existe, **o está retirado** | El **servicio** |
| **405** | El verbo **no aplica** a ese recurso | **El enrutador, solo** — nadie escribió el método |
| **422** | El cuerpo **no se puede procesar**: falta un obligatorio, el tipo no corresponde, se pasó de la longitud | El **framework**, antes de entrar al método |
| **500** | Una **defensa de la base de datos** se disparó: llave repetida, valor único repetido, un `CHECK` | La **base de datos** |

### La columna que importa es la tercera

No es un adorno: **dice en qué capa está el control**, y eso se puede
comprobar. Si un 404 lo decidiera el controlador, el servicio no sabría que el
recurso no existe — y el mismo caso por otro camino respondería distinto.

---

## 3. Las cuatro distinciones que se equivocan siempre

### 3.1 · **400 frente a 422** — la forma contra la regla

| | |
|---|---|
| **422** | El cuerpo **no se puede leer**: falta `nombre`, o llegó un texto donde va un booleano |
| **400** | El cuerpo **se leyó bien** y una **regla** no se cumple: el `PATCH` no trae ningún campo |

> **La prueba:** ¿el error lo detectó el framework antes de entrar al método?
> Entonces es **422**. ¿Lo detectó el servicio leyendo el objeto ya armado?
> **400**.

### 3.2 · **404 frente a 405** — no existe contra no aplica

| | |
|---|---|
| **404** | `DELETE /api/sede/ZZZ` — la operación existe, **la sede no** |
| **405** | `DELETE /api/rol/ADMIN` — el rol existe, **la operación no** |

> **Y el 405 aquí es especial: nadie lo escribió.** `rol`, `estado_proceso` y
> `tipo_pregunta` **no declaran** el método de baja, así que el enrutador
> responde 405 por su cuenta. Es `RF-07`: la imposibilidad está **en el tipo**,
> no en una validación. Un `if` que respondiera 405 sería *peor*, porque
> alguien podría quitarlo.

### 3.3 · **404 frente a 200 con lista vacía**

| | |
|---|---|
| **404** | Se pidió **un** recurso y no está |
| **200 `[]`** | Se pidió **una lista** y no hay nada que devolver |

> **Una lista vacía no es un error.** Es la respuesta correcta a una pregunta
> bien hecha. Devolver 404 ahí obliga al cliente a tratar un caso normal como
> excepción — y en la pantalla se ve como una falla que no existe (`RNF-11`).

### 3.4 · **500 frente a 400** — cuándo el 500 es culpa nuestra

Un **500 es siempre un defecto**, salvo uno: cuando lo produce una **defensa de
la base de datos** que el servicio no anticipó.

> **Y eso no es una excusa: es una deuda.** Cada 500 por llave repetida es un
> requisito de unicidad que el servicio debería haber comprobado antes y no
> comprobó. Se acepta en la v1 —está declarado en el contrato— y **se paga**:
> la regla es que **un 500 repetido se convierte en 400 con mensaje**, no que
> se documente y ya.

---

## 4. Lo que el mensaje **nunca** dice

| Prohibido | Por qué | Qué se dice en su lugar |
|---|---|---|
| El nombre de una tabla o columna | Es jerga y filtra el esquema | El nombre del campo **como lo ve el usuario** |
| El texto de la excepción del motor | Ilegible, y dice qué motor hay debajo | *«No se pudo guardar: ya existe una sede con ese código.»* |
| Un `stack trace`, nunca y en ningún caso | Regala la estructura interna | Nada. Va **al registro del servidor**, no a la respuesta |
| La ruta de un archivo del servidor | Igual | Nada |
| «Error inesperado» a secas | No dice nada y no se puede actuar | Qué falló, y qué puede hacer quien lo lee |

> **La regla, en una frase:** el mensaje va dirigido a **quien puede hacer algo
> con él**. Si quien lo lee no puede actuar, el mensaje está mal escrito o está
> en el sitio equivocado.

---

## 5. Lo que sí queda registrado del lado del servidor

Lo que no va en la respuesta **no desaparece**: va al registro.

| Va en la respuesta | Va solo al registro |
|---|---|
| `estado`, `mensaje`, `detalle`/`errores` | La excepción completa y su traza |
| | Qué consulta se estaba ejecutando |
| | Qué usuario y a qué hora *(desde la v3)* |

> **Esto es `RNF-20` visto desde otro lado.** Un error que no deja rastro es un
> error que no se puede depurar, y se vuelve «a veces falla».

---

## 6. Cómo se comprueba

```powershell
# 422 · falta un obligatorio
Invoke-RestMethod -Method Post -Uri http://localhost:8039/api/sede `
  -ContentType 'application/json' -Body '{}'

# 400 · la forma está bien, la regla no se cumple: PATCH sin campos
Invoke-RestMethod -Method Patch -Uri http://localhost:8039/api/sede/SBEN `
  -ContentType 'application/json' -Body '{}'

# 404 · el recurso no existe
Invoke-RestMethod -Uri http://localhost:8039/api/sede/NOEXISTE

# 405 · la operación no existe en ese recurso
Invoke-RestMethod -Method Delete -Uri http://localhost:8039/api/rol/ADMIN

# 200 [] · una lista vacía NO es un error
Invoke-RestMethod -Uri http://localhost:8039/api/sede?limite=0
```

> **Los cinco están en `7_quickstart`** y se corren con la prueba de humo. Un
> código de estado que no se prueba es un código que alguien va a cambiar sin
> darse cuenta.

---

## 7. Lo que esta política **no** cubre todavía

Se dice para que la ausencia sea una decisión:

| Caso | Estado |
|---|---|
| **401 / 403** — sin autenticar y sin permiso | Llegan en la **v3**. Hoy no hay autenticación, así que no existen |
| **409** — conflicto por edición concurrente | **Sin decidir.** Hoy la última escritura gana, y nadie lo declaró |
| **429** — demasiadas peticiones | Fuera de alcance del ejercicio |
| Reintentos y tiempo de espera del cliente | Sin definir. Lo pide `RNF-15`, que está **propuesto** |

> **El 409 es el hueco real de esta tabla.** Dos personas editando la misma
> ficha: hoy la segunda pisa a la primera **sin que nadie se entere**. No es un
> defecto del código, es una decisión que nadie tomó — y por eso está escrita
> aquí y no escondida.

---

## 8. Historial

| Versión | Fecha | Qué cambió |
|---|---|---|
| 1.0 | 14 sep 2026 | Primera versión. Recoge los códigos que `6_contracts` de la v1 ya usaba y los eleva a política, para que la v2 no los reinvente. Se agregan las cuatro distinciones de §3, las prohibiciones de §4 y el hueco declarado del **409** |

# Plan de la versión 2 — Cátedras Abiertas

> Este documento cuenta **cómo se construyó** la v2: qué había al empezar, qué
> decisión la define, en qué orden se hizo y con qué se verificó.
>
> No es la especificación —esa está en
> `2_spec.md`—
> sino el relato del camino.
>
> **Incluye lo que se rompió mientras se construía** (§6), que está arreglado y
> se cuenta porque es la parte que se repite: a quien construya lo mismo le van
> a pasar las mismas tres cosas.

---

## 0. Lo que queda al terminar

El mismo sistema de tres procesos de la v1, con **diez tablas** en vez de una:

```
FRONT (Flask, :8040) ──HTTP──> API (C#, :8039) ──SQL──> PostgreSQL (:15463)
```

- El CRUD de **las diez tablas sin clave foránea**, en la API y en pantalla.
- **Una ruta propia por recurso**: `/api/sede`, `/api/modalidad`, `/api/rol`…
  con sus clases de petición tipadas, para que Swagger describa cada cuerpo.
- **Una pantalla propia por recurso**, con su dirección y su entrada de menú.
- **El retiro que no existe** en los tres recursos que la base de datos dejó sin columna
  de estado: no se declara en ninguna capa, y `DELETE` responde 405.
- Tres pruebas verdes: capas, API y pantallas.

## 1. De dónde se partió

De la v1 **corriendo**, no de su código (regla 7 del mapa de versiones):

| Lo que dejó la v1 | Qué se hizo con ello en la v2 |
|---|---|
| `SedeController`, `ServicioSede`, `RepositorioSedePostgreSql` y sus 3 peticiones | **Se conservaron tal cual.** Son el patrón que se repitió nueve veces |
| El sobre de error plano y la fábrica del 422 | Se conservaron, y ahora valen para los diez recursos |
| `marca.css`, la banda del logo, las plantillas | Se conservaron. Las 20 pantallas nuevas no metieron **un solo color nuevo** |
| Las lecciones D-v1-1 a D-v1-9 | Siguen vigentes. La D-v1-9 —no hablarle al usuario en jerga— es un criterio de aceptación de esta versión |

> **`sede` no cambió: se le sumaron nueve hermanas.** Por eso la regresión
> (criterio 17) es barata de correr y sigue siendo obligatoria.

## 2. Lo primero que hubo que contar bien

**¿Cuántas tablas no tienen clave foránea?**

La primera cuenta dio **once**. Está mal, y el motivo enseña a leer un esquema
ajeno: en `db/init.sql` **las claves foráneas no se declaran dentro del `CREATE
TABLE`** — van todas juntas en el bloque 9/18, con `ALTER TABLE ... ADD
CONSTRAINT`.

Leyendo solo los `CREATE TABLE`, `registro_asistencia` parecía no tener
ninguna. Tiene **seis**.

La señal fiable son las columnas con prefijo `fk_` **y** el bloque de `ALTER
TABLE`. Con eso, la lista correcta es de **diez**:

```
sede · modalidad · tipo_evento · tipo_documento · tipo_vinculacion
periodo_academico · rol · tipo_pregunta · estado_proceso · encuesta
```

## 3. Y lo segundo: releer el mapa

El `0_mapa_versiones.md` decía que
la v2 traía «los catálogos, `catedra`, `sesion` y las puente». Lo que había que
construir eran **las diez tablas sin clave foránea**, que es la v1 del curso
completa.

Se corrigió el mapa —ahora son **cinco** versiones, y dice por qué— y quedó la
advertencia escrita en él:

> **Un mapa que nadie relee es una intención vieja.** Ya había pasado una vez
> entre el mapa y las historias de usuario; esta fue la segunda.

## 4. La decisión que define la versión

Con diez tablas parecidas por delante, la pregunta es inevitable:

> **¿Se repite el patrón de la v1 diez veces, o se hace uno genérico que sirva
> para todas?**

**Se repite.** Cada tabla tiene su ruta con nombre, su controlador, su
servicio, su repositorio y sus tres clases de petición. Noventa archivos.

**Y el porqué, que es lo que hay que entender:**

| | |
|---|---|
| **La razón de fondo** | Una API genérica sirve para **un prototipo**. Un prototipo se escribe y se tira; este sistema **se lee y se cambia durante años**, casi siempre por alguien que no lo escribió |
| **Lo primero que se pierde** | **Swagger deja de decir qué recursos existen**: muestra `/api/{tabla}`, un hueco. Y este proyecto se sustenta proyectando Swagger |
| **Lo segundo** | El compilador deja de revisar los campos, los permisos por recurso no caben, las métricas por endpoint se vuelven una sola línea, y un cambio pequeño toca las diez tablas |
| **Qué cuesta hacerlo específico** | Noventa archivos parecidos, y un cambio transversal repetido diez veces |
| **Por qué ese costo no es el que parece** | Repetir un patrón **no es duplicar lógica**: cada archivo dice sus columnas, sus restricciones y sus mensajes |
| **La excepción honesta** | Sí hay APIs genéricas buenas en producción —PostgREST, Hasura—, y lo que las hace válidas es que **lo genérico es el producto entero**. Lo que no funciona es la mitad |

Quedó escrito como **Artículo 10.1** de la constitución y como **sección 6.1**
de la metodología del curso, en los diez módulos.

### La ventaja que solo se ve al comparar

Una ruta genérica obliga a componer el nombre de la tabla dentro del SQL, y eso
exige una comprobación previa contra una lista cerrada. Funciona, y es frágil:
si alguien quita esa línea «para dar flexibilidad», el repositorio pasa de
correcto a peligroso sin cambiar nada más.

**Aquí esa línea no existe.** El nombre de la tabla está escrito en el SQL de
cada repositorio, no llega de ninguna parte, y no hay nada que validar.

> Una arquitectura donde la seguridad **depende de que alguien no borre una
> comprobación** es peor que una donde no hay nada que comprobar.

## 5. El orden en que se construyó

| Fase | Qué | Por qué en ese lugar |
|---|---|---|
| 1 | **`sede` completa**, calcada de la v1 | Es el patrón de referencia: escribirlo bien una vez hace mecánicas las nueve siguientes |
| 2 | **La prueba de capas** | **Antes** de seguir: si el servicio necesitara HTTP para probarse, la separación estaría mal, y con una tabla escrita todavía es barato arreglarla |
| 3 | Las otras ocho, una por una | Cada una con lo suyo |
| 4 | `encuesta`, la más distinta, de última | Su llave la genera la base de datos |
| 5 | El ensamblador: veinte registros | |
| 6 | El front, con su pantalla y su entrada de menú por recurso | |
| 7 | **Correrlo**, incluida una prueba que **mira Swagger** | Donde aparecieron los tres tropiezos del §6: Dapper y las fechas, una columna `date` con un parámetro de texto, y la casilla que el PATCH no debe desmarcar |
| 8 | Documentar, contra lo que el sistema responde | |

## 6. Los tres tropiezos que aparecieron al correrlo

**De qué son, para que no haya dudas:** son tres problemas del **código de la
API de esta versión**, que aparecieron al ponerla a correr y **quedaron
arreglados antes de cerrarla**.

- **No son de la documentación.**
- **No son de la v1.** `sede` no tiene fechas, así que dos de los tres no
  podían pasarle; el tercero es una regla del guardado parcial que la v1
  cumplía ya.
- **No quedan en el sistema entregado.** Están corregidos, y hay una prueba
  que comprueba cada uno.

Se cuentan porque son la parte útil del relato: a quien construya lo mismo le
van a pasar.

Los tres tienen algo en común y por eso valen: **los tres compilaban**. El
proyecto se armaba sin una sola advertencia y fallaba al primer intento de
guardar un dato. Ninguna revisión de código los habría encontrado.

En una frase cada uno:

| # | Qué se rompió | Dónde quedó la lección |
|---|---|---|
| 6.1 | Los dos recursos con **fechas** daban 500 en todo: Dapper no conoce `DateOnly` aunque Npgsql sí | `4_research.md` |
| 6.2 | Una columna `date` **no acepta un parámetro de texto**, y el recurso entero no se podía crear | `4_research.md` |
| 6.3 | En la modificación parcial, una **casilla que no viaja** no puede guardarse como «desmarcada» | `4_research.md` |

Y hay un cuarto, el 6.4, que no era del sistema sino del guion de prueba.

### 6.1 · Dapper no conoce `DateOnly`, aunque Npgsql sí (D-v1-6)

Los dos recursos con fechas daban **500 en todo**. Lo confuso es que el driver
sí sabe convertir `date` ↔ `DateOnly`: **el que no sabe es Dapper**, que se
interpone con su propia tabla de tipos y, cuando no encuentra uno, lanza en vez
de delegar.

> **Una biblioteca de conveniencia no es el driver.** Su comodidad tiene un
> borde, y al llegar a él hay que enseñarle a mano lo que el driver ya sabía.

### 6.2 · Una columna `date` no acepta un parámetro de texto (D-v1-7)

Declarar una fecha como texto —«total, PostgreSQL sabe convertir»— deja el
recurso sin poder crearse:

```
42804: column "fecha_ini" is of type date but expression is of type text
```

Un texto se convierte a fecha cuando uno lo **escribe en el SQL**; un
**parámetro sí tiene tipo**, y el motor no convierte al asignar.

**Y lo que costó verlo:** la prueba de humo ejercitaba solo `sede`, que no
tiene fechas, así que daba todo en verde con un recurso roto. Se reescribió
para recorrer **los diez**.

> **Probar el patrón en la tabla que se tenía a mano es probar la que ya
> funcionaba.**

### 6.3 · El booleano del PATCH (D-v1-8)

Una casilla sin marcar **no se envía**. En el POST y el PUT eso lo resuelve el
contrato —`[Required] bool?`, y falta → 422—, pero en la modificación parcial
un booleano ausente **no puede viajar como falso**: quien cambiara solo el
nombre desmarcaría la casilla sin enterarse.

Se ve con los ojos en `tipo_pregunta`: desmarcar sus tres casillas viola el
CHECK de la base de datos, que exige exactamente una.

### 6.4 · Y uno más, que no era del sistema

Al correr la prueba de humo **dos veces seguidas**, la segunda falló. El
sistema estaba bien: el **retiro es lógico**, así que la fila retirada sigue
ocupando su código y sus valores únicos (`pk_sede`, `uq_sede_nombre`,
`uq_encuesta_ver`).

El guion generaba valores fijos. Ahora genera un sufijo distinto en cada
corrida, y el motivo está escrito en su cabecera — porque es exactamente la
clase de cosa que uno vuelve a hacer.

## 7. Cómo se verificó

| Prueba | Qué comprueba |
|---|---|
| `pruebas/PruebaCapas.csproj` | Las capas, **sin base de datos y sin Npgsql ni Dapper**, en cuatro servicios elegidos por lo que cada uno tiene de distinto |
| `pruebas_humo/humo_api.py` | El contrato, sobre **los diez recursos**, y **el documento OpenAPI**: que haya una ruta por recurso, que ninguna tenga el nombre de la tabla como parámetro, y que los tres sin retiro no publiquen DELETE |
| `pruebas_humo/humo_front.py` | Las **once pantallas**, llenando formularios con cookies, y que no aparezca jerga en el HTML |

Las tres, **dos veces seguidas**, sobre un sistema levantado desde cero con
`docker compose down -v && docker compose up -d --build`.

> **La prueba que mira Swagger es la que hay que copiar.** Comprueba lo que el
> sistema **publica**, no lo que responde. Un sistema puede pasar todas las
> demás pruebas y no dejar ver qué recursos tiene — y ninguna de las otras se
> entera.

## 8. Lo que queda para la v3

- Las tablas **con** clave foránea: `catedra`, `sesion`, `asistente` y las
  puente.
- Las **listas desplegables** que eligen una llave foránea en vez de digitarla.
  Hay una deuda concreta esperándolas: `modalidad.canal_difusion` es una lista
  cerrada (`QR`, `ENLACE`, `AMBOS`) que hoy se digita, y equivocarse da un 500
  del motor.
- **Reactivar** una ficha retirada, que hoy no se puede.

Nada de eso se preparó aquí. El Artículo 1 dice que una versión no anticipa a
la siguiente, y la razón es la de siempre: lo que se construye «por si acaso»
casi nunca es lo que hace falta cuando llega el momento.

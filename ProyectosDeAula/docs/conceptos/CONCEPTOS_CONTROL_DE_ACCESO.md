# El control de acceso

> **Qué es este documento.** Qué significa «ponerle llave» a un sistema, y las
> decisiones que hay que tomar **antes** de escribir una línea.
>
> ### ⚠️ Esto TODAVÍA NO está en el repositorio
>
> Hoy la API atiende a cualquiera que llegue al puerto 8039. No es una
> suposición: en `api_catedras/` **no hay un solo `[Authorize]`**, ni
> `JwtBearer`, ni un atributo de permiso — se puede comprobar buscándolos.
>
> Con el sistema arriba se ve igual de rápido:
>
> ```powershell
> Invoke-RestMethod http://localhost:8039/api/sede   # responde sin token
> ```
>
> Este documento es **lo que viene
> en la versión que agrega la puerta**, y está escrito antes para que las
> decisiones no se tomen a las carreras.
>
> Versión 1.0 · 5 de octubre de 2026.

---

## 1. Tres cosas distintas, que se confunden

| | La pregunta que responde | Si falla |
|---|---|---|
| **Identificación** | ¿Quién dice ser? | — |
| **Autenticación** | ¿Puede demostrarlo? | **401** |
| **Autorización** | ¿Puede hacer esto? | **403** |

> **El 401 y el 403 no son lo mismo, y confundirlos cuesta caro.**
>
> - **401** = «no sé quién es usted». El cliente debe **identificarse**.
> - **403** = «sé quién es, y no puede». Identificarse otra vez **no cambia
>   nada**.
>
> Si todo responde 401, quien consume la API no puede distinguir «vuelva a
> entrar» de «pídale permiso a alguien». Son dos arreglos distintos.

---

## 2. La contraseña no se guarda: se guarda su hash

Y es lo primero, porque sin esto lo demás es decoración.

| | |
|---|---|
| **Qué se guarda** | Un **hash**: el resultado de una función que no se puede devolver |
| **Qué NO se guarda** | La contraseña. Ni cifrada, ni «codificada», ni en un campo escondido |
| **Cómo se verifica** | No se compara el hash: se le **pregunta al algoritmo** si ese texto produce ese hash |

> **Dos hashes de la misma contraseña son distintos**, y eso es correcto: cada
> uno lleva su propia **sal** adentro. Si fueran iguales, la tabla delataría
> qué dos personas usan la misma clave.
>
> **Y por eso no existe «recuperar» una contraseña**, solo «cambiarla». Un
> sistema que se la devuelve por correo le está diciendo que la guardó en
> claro.

**Dónde vive esto en cátedras abiertas:** la tabla `usuario` ya está en el
esquema, con su columna `clave_hash varchar(255)`. Lo que no existe todavía es el código
que la use.

---

## 3. La sesión: el token

Identificarse una vez y que la **segunda** petición sepa quién pregunta. Eso
es un token: un papel firmado que el cliente adjunta en cada llamada.

```
POST /api/sesion   { "email": "...", "contrasena": "..." }   →   un token
GET  /api/sede     Authorization: Bearer <token>             →   200
GET  /api/sede     (sin cabecera)                            →   401
```

| Qué lleva el token | Qué NO debe llevar |
|---|---|
| Quién es (el correo o el id) | La contraseña |
| Cuándo vence | **Los permisos** — y el §4 explica por qué |
| Una firma que el servidor comprueba | Datos que cambien seguido |

> **El token va firmado, no cifrado.** Cualquiera puede leer lo que dice; lo
> que nadie puede es **cambiarlo sin que se note**. Por eso no se guarda ahí
> nada secreto.
>
> **Y la firma se comprueba siempre.** Un token con un carácter cambiado
> responde 401, sin consultar la base de datos: no cuadra la firma y se acabó.

---

## 4. La decisión que define la versión: **el permiso NO va en el token**

Es la que más se discute, y la que más consecuencias tiene. Los permisos
podrían ir **dentro** del token: se calculan al entrar, se firman, y cada
petición trae la respuesta puesta. Sería más rápido.

| | Permiso EN el token | Permiso consultado **cada vez** |
|---|---|---|
| **Costo** | Cero consultas por operación | **Una** consulta por operación |
| **Quitarle un permiso a un rol** | No surte efecto **hasta que el token venza** | Surte efecto **en la siguiente petición** |
| **Quién manda** | Lo que se firmó hace una hora | Lo que dice la base de datos **ahora** |

> **La pregunta que lo decide no es técnica:** si a alguien se le retira un
> permiso porque dejó el cargo, ¿cuánto tiempo puede seguir entrando? Si la
> respuesta es «ninguno», el permiso **no puede** estar en el token.

---

## 5. Y el permiso lo resuelve la base de datos

El esquema de cátedras abiertas ya trae las tres tablas que hacen falta:

```
usuario  ──▶  rol_por_usuario  ──▶  rol
```

| Tabla | Qué guarda |
|---|---|
| `usuario` | Quién puede entrar, y el hash de su contraseña |
| `rol` | Los perfiles del sistema |
| `rol_por_usuario` | Qué rol tiene cada quién — **y desde cuándo hasta cuándo** |

> **Fíjese en `fecha_ini` y `fecha_fin` de `rol_por_usuario`**, que es más de
> lo que suele haber: el permiso no solo existe o no existe, **tiene
> vigencia**. Un rol que terminó ayer no se borra — se cierra, y queda el
> registro de que lo tuvo.
>
> Eso cambia la consulta del permiso: no basta con que la fila exista, tiene
> que estar **vigente hoy**.

**Por qué la regla vive en la base de datos y no en C#:** porque así vale
también para quien entre por `psql` o por otro cliente. Una regla que solo
existe en la API protege **una** puerta.

---

## 6. Dónde se exige el permiso: en un filtro, no en cada método

Cuando llegue el momento de escribirlo, la decisión importa:

| | Qué pasa el día que alguien agrega un endpoint y se le olvida |
|---|---|
| **Una línea al principio de cada método** | Ese endpoint **queda abierto — y nadie lo nota, porque funciona** |
| **Un filtro, declarado sobre el controlador** | Está a la vista, en la misma línea donde se lee el nombre |

En ASP.NET Core eso es un atributo:

```csharp
[Authorize]                       // sin token: 401
[ExigePermiso("interfaz.sedes")]  // con token y sin permiso: 403
public class SedeController : ControllerBase
```

> **El filtro no se olvida.** Esa es toda la razón de preferirlo.

---

## 7. El menú escondido NO es protección

Y es el error que hay que decir antes de que se cometa.

| | |
|---|---|
| **Esconder el enlace en el menú** | Comodidad. Evita que la persona vea opciones que no le sirven |
| **Lo que de verdad protege** | Que la **API** responda 403 |

> **Se comprueba escribiendo la dirección a mano.** Si el menú esconde
> «Sedes» pero escribir `/sedes` en la barra trae los datos, no hay control de
> acceso: hay un menú ordenado.
>
> La pantalla **puede** abrirse —el front no es la autoridad—, pero el dato no
> puede llegar.

---

## 8. Lo que habrá que poder demostrar

Cuando esta versión esté hecha, esto es lo que se prueba — y lo que se
pregunta en la sustentación:

| Intento | Respuesta |
|---|---|
| Una operación **sin token** | **401** |
| Un token con un carácter cambiado | **401** |
| Credenciales incorrectas | El **mismo** mensaje que con un correo que no existe |
| Un rol sin permiso | **403**, no 401 |
| Quitar el permiso y repetir **sin volver a entrar** | **403** de inmediato |
| Escribir la dirección a mano | La pantalla abre; el dato **no llega** |
| `SELECT contrasena FROM usuario` | Ni un texto legible |

> **El mismo mensaje para «ese correo no existe» y «la contraseña está mal»**
> no es pereza: dos mensajes distintos confirman **qué correos están
> registrados**, que es justo lo que no se quiere regalar.

---

| Qué | Dónde |
|---|---|
| Las tablas `usuario`, `rol` y `rol_por_usuario` | [`../dominio/DISENO_BD.md`](../dominio/DISENO_BD.md) |
| Qué código HTTP responde cada cosa | [`../dominio/POLITICA_DE_ERRORES.md`](../dominio/POLITICA_DE_ERRORES.md) |
| Las capas donde encaja el filtro | [`SOLID_CAPAS_PATRONES.md`](SOLID_CAPAS_PATRONES.md) |
| Cómo se levantan estos requisitos | [`CONCEPTOS_REQUISITOS.md`](CONCEPTOS_REQUISITOS.md) |

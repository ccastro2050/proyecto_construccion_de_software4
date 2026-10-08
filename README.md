# Proyecto Construcción de Software — construcción por versiones

Proyecto de curso (USB Medellín). Aquí NO se descarga un sistema terminado:
**se construye un sistema real por versiones en C# / ASP.NET Core**, guiado
por especificaciones. El repositorio siempre contiene la **versión en
curso, funcionando** — usted la ejecuta, la estudia y luego la
**reconstruye desde cero** en su propio proyecto.

---

## 1. Cómo le trabaja el estudiante (léame primero)

### Qué necesita instalado (una sola vez)

| Herramienta | Para qué |
|---|---|
| **Git** | Clonar el repositorio y traer versiones nuevas |
| **Docker Desktop** | La BD y la API corren en contenedores (no se instala PostgreSQL ni .NET) |
| **VS Code** | El editor — y su terminal integrada (*Terminal → New Terminal*) |

> El SDK de .NET local es **opcional** (solo para desarrollar fase a fase
> sin Docker): .NET 10.

### Primera vez: cargar y EJECUTAR la versión (un solo comando)

En la terminal integrada de VS Code (*Terminal → New Terminal*, PowerShell):

> ⚠️ **ANTES de clonar — solo si usted ya corrió OTRO proyecto de estos
> cursos en este PC:** puede quedar un contenedor viejo encendido ocupando
> el puerto 8045 (pasa al reiniciar el PC: la API vieja revive sin su
> base de datos y "secuestra" el puerto — el contenedor huérfano). El
> síntoma: Swagger abre, pero todo responde 500 con *"No address
> associated with hostname"*, y usted cree que el error es de ESTE
> proyecto cuando en realidad está hablando con el viejo. Verifíquelo y
> apáguelo primero:
>
> **En este curso los dos comandos se copian y se pegan tal cual.** Pero no
> por la misma razón, y la diferencia importa:
>
> | | ¿Se cambia? |
> |---|---|
> | `$_` | **Nunca.** Es sintaxis de PowerShell — significa «cada uno de los que vinieron por la tubería». Si lo reemplaza por algo, deja de funcionar |
> | `proyecto_` | **Aquí no**, porque todas las carpetas de estos cursos se llaman `proyecto_algo`. Pero es **texto de búsqueda**: en otro proyecto habría que poner el suyo |
>
> **O sea: este comando no es universal.** Funciona tal cual en estos cursos
> porque Docker le pone al contenedor el nombre de la carpeta de la que
> salió, y todas empiezan igual. Si mañana trabaja en una carpeta llamada
> `taller_php`, el filtro sería `name=taller_`.
>
> **¿Y cómo sabría qué poner?** Corriendo `docker ps` sin filtro, mirando los
> nombres de la columna `NAMES` y escogiendo el pedazo que tengan en común.
>
> **Paso 1 — VERIFICAR.** ¿Quedó algo del curso encendido?
>
> ```powershell
> docker ps --filter "name=proyecto_"
> ```
>
> | La parte | Qué significa |
> |---|---|
> | `docker ps` | Lista los contenedores **encendidos** |
> | `--filter` | «No me muestre todo, filtre» |
> | `name=` | Filtrar **por nombre**. Es palabra de Docker: también existen `status=` y `ancestor=` |
> | `proyecto_` | **El texto a buscar.** Esto no es sintaxis: lo escogió quien escribió el comando |
>
> **Ojo con esa última parte.** El comando se copia tal cual y funciona, pero
> `proyecto_` no es una palabra mágica: es el texto por el que se busca.
> Funciona porque **todas** las carpetas de estos cursos se llaman
> `proyecto_algo`, y Docker le pone al contenedor el nombre de la carpeta de
> la que salió. Si su carpeta se llamara `taller_php`, el filtro sería
> `name=taller_`.
>
> Si hay algo, se ve así:
>
> ```
> NAMES                                               STATUS                    PORTS
> proyecto_construccion_de_software4-api-facturas-1   Up 2 hours                0.0.0.0:8045->8045/tcp
> proyecto_construccion_de_software4-postgres-1       Up 2 hours (healthy)      0.0.0.0:15445->5432/tcp
> ```
>
> **Si no hay nada, sale solo el encabezado** —`NAMES  STATUS  PORTS`— y
> ninguna línea debajo. En ese caso no tiene que limpiar nada: siga.
>
> **Paso 2 — LIMPIAR.** Apaga de una vez todos los del curso:
>
> ```powershell
> docker ps --filter "name=proyecto_" -q | ForEach-Object { docker stop $_ }
> ```
>
> | La parte | Qué significa |
> |---|---|
> | `docker ps --filter …` | Lo mismo de arriba: los del curso que están encendidos |
> | `-q` | *quiet*. En vez de la tabla, imprime **solo el identificador** de cada uno |
> | `\|` | La tubería: entrega esa lista al comando que sigue |
> | `ForEach-Object { … }` | «Para **cada uno** de los que llegaron, haga esto» |
> | `$_` | **Cada uno de ellos.** Es de PowerShell: no se reemplaza por nada |
> | `docker stop $_` | Apaga ese contenedor |
>
> En una frase: **«de los contenedores del curso que estén encendidos, tome
> el identificador de cada uno y apáguelo».**
>
> Va imprimiendo el identificador de cada uno que apaga. Para comprobar que
> quedó limpio, repita el paso 1: debe salir solo el encabezado.
>
> **Qué efecto tiene:** apaga los contenedores. **No borra nada** — los datos
> quedan en sus volúmenes y cada proyecto se vuelve a encender con su
> `docker compose up -d`. Funciona aunque ya no tenga la carpeta vieja.
> También sirve el botón **Stop** de Docker Desktop, uno por uno.
>
> Solo entonces continúe.

```powershell
git clone https://github.com/ccastro2050/proyecto_construccion_de_software4.git
cd proyecto_construccion_de_software4
docker compose up -d --build
```

**Eso es todo.** La primera vez tarda unos minutos (descarga imágenes,
PostgreSQL se siembra solo con el script montado, y la primera
compilación toma ~1 minuto más). Al terminar quedan corriendo **tres
contenedores**: la base de datos, la API y la **interfaz gráfica**.

### Lo primero que hay que abrir

| Qué | Dónde |
|---|---|
| **La interfaz gráfica** — por aquí se empieza | **http://localhost:8051** |
| **Swagger** — la API, para verla y probarla | http://localhost:8045/swagger |
| La API — diagnóstico | http://localhost:8045/ |
| PostgreSQL (para SQLTools/pgAdmin, opcional) | `localhost:15445` · `postgres`/`Construccion123!` |

> **La interfaz gráfica y la API son dos puertos distintos**, y conviene no
> confundirlos: el **8051** es lo que se abre en el navegador; el **8045**
> es lo que esa interfaz consume. Abrir `8051/swagger` da 404 — Swagger vive
> en la API.

### El menú de la interfaz gráfica

**Doce entradas**, agrupadas por versión — y es la forma más rápida de ver que cada versión **incluye la anterior**:

| Dirección | En el menú | De la |
|---|---|---|
| `/productos` | Productos | v1 |
| `/empresas` | Empresas | v1 |
| `/personas` | Personas | v1 |
| `/roles` | Roles | v1 |
| `/rutas` | Rutas | v1 |
| `/usuarios` | Usuarios | v1 |
| `/clientes` | Clientes | v2 |
| `/vendedores` | Vendedores | v2 |
| `/facturas` | Facturas | v2 |
| `/usuario-con-roles` | Usuarios y roles | v2 |
| `/rol-usuario` | Roles por usuario | v2 |
| `/ruta-rol` | Permisos por rol | v2 |

> **El menú nombra RECURSOS del dominio, no tablas ni rutas de la API.**
> Dice «Facturas», no `/api/factura`.

### Y en esta versión hay que identificarse primero

**La v3 le pone la puerta a todo lo anterior.** Sin iniciar sesión, la interfaz
manda a `/sesion` y la API responde **401**.

| Correo | Contraseña | Qué ve en el menú |
|---|---|---|
| `admin@correo.com` | `admin123` | **las 12** interfaces |
| `vendedor1@correo.com` | `vendedor123` | Facturas y Clientes — **no** Usuarios, Personas ni Productos |
| `cliente1@correo.com` | `cliente123` | Productos — **no** Facturas ni Clientes, al revés que el vendedor |

> **En Swagger hay que autorizar antes de probar nada:** `POST /api/sesion` con
> uno de esos correos → copie el `token` de la respuesta → botón **Authorize**
> arriba a la derecha → pegue **solo el token** (la palabra `Bearer` la pone
> Swagger). Los pasos están en
> [`7_quickstart.md` §2bis](docs/spec_kit/versiones/v3_control_acceso/7_quickstart.md).

**La prueba que importa, y es el criterio 9:** entre como `vendedor1` y
escriba **`/usuarios` en la barra de direcciones**. La interfaz se abre y tiene
que decir que no tiene permiso, con **cero filas**. Si mostrara los datos, el
control estaba en el menú — y esconder una entrada del menú **no es** control
de acceso.

> **Recargar con F5 cierra la sesión.** El token vive en el circuito de Blazor
> Server, en memoria del servidor, y no baja al navegador: el F5 tumba el
> circuito. Está en
> [`3_plan.md` §4.1](docs/spec_kit/versiones/v3_control_acceso/3_plan.md) con su
> razón.

> ℹ️ Este proyecto usa los puertos **8051** (interfaz gráfica), **8045**
> (API) y **15445** (PostgreSQL): si alguno ya está ocupado
> en su máquina, cámbielo en `docker-compose.yml` (el lado izquierdo del
> `"puerto:puerto"`).
>
> ⚠️ La v4 suma SQL Server: necesita ~2 GB de RAM libres en Docker
> Desktop (PostgreSQL sigue siendo liviano).

### Los días siguientes (volver a encender)

```powershell
docker compose up -d        # segundos; los datos se conservan
```

### Cuando hay cambios

| Qué cambió | Qué hacer |
|---|---|
| **Usted edita un `.cs`** | **Nada** — el código está montado como volumen y `dotnet watch` recompila y reinicia solo (espere unos segundos) |
| **El profesor publicó una versión nueva** | `git pull` y `docker compose up -d --build` |
| **Cambió el `Dockerfile` o el `.csproj`** | `docker compose up -d --build` (reconstruye la imagen) |
| **Quiere resetear la BD** a sus datos originales | `docker compose down -v` y luego `docker compose up -d` (⚠️ borra los datos) |
| **Apagar todo** | `docker compose down` (los datos se conservan) |

### Y ahora, SU trabajo: reconstruirla desde cero

Ejecutar la versión del repo es solo el punto de partida. Lo que se evalúa
es **reconstruirla usted mismo, en una carpeta propia (fuera del clon)**,
siguiendo las especificaciones — con o sin ayuda de IA:

> 🤖 ¿Va a trabajar con IA? Siga la **[Guía para construir la versión con
> IA](docs/spec_kit/versiones/v4_aplicativo/GUIA_IA4.md)** — cubre los dos caminos con su prompt exacto listo
> para copiar: **chat web** (Gemini, DeepSeek, ChatGPT: qué archivos
> subirle) e **IDE agéntico** (Antigravity, Cursor, Claude Code: cómo
> supervisar al agente).

### Conceptos resumidos (los que acaba de usar)

| Concepto | En una frase |
|---|---|
| **Clonar** | Descargar el repositorio con su historial; `git pull` trae lo nuevo |
| **Contenedor** | BD y API corren en "cajas" de Docker: nada que instalar, se borran y recrean sin miedo |
| **docker compose** | UN archivo declara todo el sistema y UN comando lo levanta (`up -d`) |
| **Volumen** | Donde viven los datos: `down` los conserva, `down -v` los borra (reset) |
| **dotnet watch** | El vigilante del código: guardar un `.cs` recompila y reinicia la API sola |
| **Spec kit** | Los documentos que dicen QUÉ/CÓMO/EN QUÉ ORDEN — la fuente de verdad |
| **Versión / tag** | Un incremento cerrado y verificado (`v1`, `v2`, …): se avanza solo en verde |

> Detalle de los conceptos Docker: [docs/conceptos/CONCEPTOS_DOCKER.md](docs/conceptos/CONCEPTOS_DOCKER.md).

---

## 2. Estructura del repositorio

Qué es cada carpeta y cada archivo, y para qué sirve:

```
proyecto_construccion_de_software4/
├── docker-compose.yml           # TODO el sistema declarado: PostgreSQL + API
│                                #   (el "un solo comando" del proyecto)
├── db/
│   └── bdfacturas_postgres.sql  # Crea bdfacturas COMPLETA (12 tablas, triggers,
│                                #   SPs, datos) — PostgreSQL lo ejecuta SOLO la
│                                #   primera vez (docker-entrypoint-initdb.d)
│
├── postman/                     # La colección de Postman lista para importar:
│                                #   los 13 endpoints en orden didáctico (alternativa a Swagger)
│
├── api_facturas/                # LA API DE LA v1 — C#/ASP.NET Core (puerto 8045)
│   ├── ApiFacturas.csproj       # El proyecto .NET (paquetes: Npgsql, Dapper y Swashbuckle)
│   ├── Program.cs               # Punto de entrada: ENSAMBLADOR (DI) + 422 + rutas
│   ├── appsettings.json         # Cadena de conexión (default localhost:15445)
│   ├── Dockerfile               # Imagen sdk:10.0 + dotnet watch
│   ├── Controllers/             # Capa 1 — HTTP: atributos de verbo y try/catch → códigos
│   ├── Modelos/                 # Los MODELOS = las clases ENTIDAD (v1: Producto)
│   ├── Peticiones/              # Los body por verbo (Crear/Reemplazo/Actualizar):
│   │                            #   sus anotaciones validan la entrada → 422
│   ├── Servicios/               # Capa 2 — negocio: interfaz + reglas
│   ├── Repositorios/            # Capa 3 — datos: interfaz + Dapper (SQL a mano)
│   ├── Excepciones/             # NoEncontradoExcepcion (el servicio la lanza → 404)
│   └── pruebas/                 # Proyecto de consola: el servicio con repositorio
│                                #   FALSO en memoria (criterio 6, corre sin BD)
├── docs/
│   ├── spec_kit/                # LAS ESPECIFICACIONES: constitución permanente +
│   │                            #   una carpeta de specs por versión (v1, v2, …)
│   │                            #   + la GUIA_IA de ESA versión (GUIA_IA1, GUIA_IA2…) (cómo
│   │                            #   construirla con ayuda de una IA)
│   ├── FLUJO_DE_UNA_PETICION.md # Dónde "está" el GET, dónde se captura el POST
│   ├── TUTORIAL_VSCODE_SQLTOOLS.md # Administrar la BD desde VS Code (SQLTools)
│   ├── PARADIGMA_POO.md         # Material conceptual: POO, SOLID+capas, ACID,
│   ├── SOLID_CAPAS_PATRONES.md         #   Docker y SDD (un .md por tema)
│   ├── PRINCIPIOS_ACID.md       #
│   ├── CONCEPTOS_DOCKER.md      #
│   └── SDD_SPECKIT.md           #
│
├── .gitignore / .gitattributes  # Higiene del repo (bin/, obj/, .session.sql; .sh con LF)
└── README.md                    # Este archivo
```

La regla de lectura: **el sistema vive en `docker-compose.yml`**, la API
vive en `api_facturas/` (una carpeta por capa), y **todo lo que explica**
vive en `docs/`. Cuando lleguen las versiones siguientes, aquí aparecerán
más carpetas de componentes (y el compose crecerá con ellas).

## 3. La ruta de versiones

```
v1  CRUD de las SEIS tablas sin clave foranea (producto, empresa,
    persona, rol, ruta, usuario) — API Y INTERFAZ GRÁFICA
v2  CRUD de las SEIS con clave foranea: las FK como listas
    desplegables, las puente, y la factura maestro-detalle — API Y INTERFAZ GRÁFICA
v3  control de acceso: JWT, sesiones y permisos por rol
v4  el resto: 10 consultas multitabla, dashboard, manual de marca,
    responsive/PWA y publicacion   <- USTED ESTA AQUI
```

> **Son CUATRO versiones.** No hay v5 ni v6: lo que antes eran «un
> motor por version» y «el front al final» cambio de lugar — ver
> [el mapa](docs/spec_kit/versiones/0_mapa_versiones.md).
>
> **Y cada version entrega su API y su INTERFAZ GRÁFICA**, en Blazor Server,
> en su propio contenedor. Media version no es una version.

La regla del juego: la **constitución** es permanente, cada versión tiene
su propia spec, y una versión está TERMINADA solo cuando pasa sus criterios
de aceptación (commit + tag). Mapa completo:
[docs/spec_kit/versiones/0_mapa_versiones.md](docs/spec_kit/versiones/0_mapa_versiones.md).

## 4. Las especificaciones de la versión actual (v4)

| Documento | Contenido |
|---|---|
| [1_constitution.md](docs/spec_kit/1_constitution.md) | Las reglas permanentes del proyecto — **vale para las cuatro versiones** |
| [2_spec.md](docs/spec_kit/versiones/v4_aplicativo/2_spec.md) | QUÉ construir y los criterios de aceptación de la v4 |
| [3_plan.md](docs/spec_kit/versiones/v4_aplicativo/3_plan.md) | CÓMO: stack, estructura y diseño de las capas |
| [4_research.md](docs/spec_kit/versiones/v4_aplicativo/4_research.md) | Decisiones y alternativas (el porqué) |
| [5_data_model.md](docs/spec_kit/versiones/v4_aplicativo/5_data_model.md) | El modelo de datos. **La v4 no agrega tablas**: consulta las doce |
| [6_contracts.md](docs/spec_kit/versiones/v4_aplicativo/6_contracts.md) | Los contratos. Los **70 endpoints** de v1–v3 no se tocan; la v4 **suma 10** |
| [7_quickstart.md](docs/spec_kit/versiones/v4_aplicativo/7_quickstart.md) | Arranque y verificación |
| [8_tasks.md](docs/spec_kit/versiones/v4_aplicativo/8_tasks.md) | Orden de construcción por fases verificables |
| [9_checklist.md](docs/spec_kit/versiones/v4_aplicativo/9_checklist.md) | La lista que **se marca a mano** antes de poner el tag |
| [GUIA_IA4.md](docs/spec_kit/versiones/v4_aplicativo/GUIA_IA4.md) | Los dos caminos —chat web e IDE agéntico— con su prompt exacto |

> **Y las cuatro versiones tienen su spec kit completo**, cada una en su
> carpeta: [`v1_sin_fk/`](docs/spec_kit/versiones/v1_sin_fk/),
> [`v2_con_fk/`](docs/spec_kit/versiones/v2_con_fk/),
> [`v3_control_acceso/`](docs/spec_kit/versiones/v3_control_acceso/) y
> `v4_aplicativo/`. La v4 es **acumulativa**: contiene lo de las tres
> anteriores, y por eso la regresión es obligatoria.

## 5. Material conceptual del curso

| Documento | Qué cubre |
|---|---|
| [El flujo de una petición](docs/conceptos/FLUJO_DE_UNA_PETICION.md) | **Léalo primero:** dónde está el GET, dónde se captura el POST, y el viaje completo por las capas |
| [Colección de Postman](postman/README.md) | Los 13 endpoints de la v1 listos para importar y probar con clics — incluida la pareja PUT=422 vs PATCH=200 |
| [SDD y Spec Kit](docs/conceptos/SDD_SPECKIT.md) | La metodología con la que se trabaja este curso: la spec manda sobre el código |
| [Pruebas y calidad de las pruebas](docs/conceptos/PRUEBAS_Y_CALIDAD_DE_PRUEBAS.md) | Cobertura, la métrica CRAP y mutation testing: cómo saber si sus pruebas de verdad protegen — y por qué hoy es reto opcional, no alcance del proyecto |
| [Programación asincrónica](docs/conceptos/PROGRAMACION_ASINCRONICA.md) | Qué resuelve el async/await en la web, qué se daña sin él (con diagramas), y cómo se ve en el código de este proyecto |
| [El paradigma P.O.O. en C#](docs/conceptos/PARADIGMA_POO.md) | Qué es un paradigma, los 4 pilares, y las propiedades e interfaces de C# |
| [SOLID, capas y patrones de diseño](docs/conceptos/SOLID_CAPAS_PATRONES.md) | Los 5 principios y las capas — y en qué versión se demuestra cada uno |
| [Principios ACID](docs/conceptos/PRINCIPIOS_ACID.md) | Las 4 garantías transaccionales, por qué una facturación las exige |
| [Conceptos de Docker](docs/conceptos/CONCEPTOS_DOCKER.md) | Imagen, contenedor, volumen, compose (con el del proyecto explicado línea por línea) y por qué NO se necesita Kubernetes |


## 6. El dominio: qué ES este sistema

Los de arriba explican **los conceptos**. Estos 18, en
[`docs/dominio/`](docs/dominio/), describen **este sistema concreto** — y están
escritos desde el código, el esquema y la API corriendo, no de memoria.

| Documento | Qué cubre |
|---|---|
| [**PLAN_DE_TRABAJO**](docs/dominio/PLAN_DE_TRABAJO.md) | **El acta donde los tres acuerdan quién hace qué**: quién integra, qué herramienta usa cada uno, cómo se reparten el código **y los documentos del spec kit** |
| [GLOSARIO](docs/dominio/GLOSARIO.md) | Cada palabra del dominio, y las que se confunden entre sí |
| [REGLAS_DE_NEGOCIO](docs/dominio/REGLAS_DE_NEGOCIO.md) | Las reglas **con quién las defiende** — y lo que nada defiende |
| [DISENO_BD](docs/dominio/DISENO_BD.md) | El modelo en sus cuatro etapas, y **por qué** quedó así |
| [ARQUITECTURA](docs/dominio/ARQUITECTURA.md) | Las capas y, sobre todo, **qué tiene prohibido hacer cada una** |
| [POLITICA_DE_ERRORES](docs/dominio/POLITICA_DE_ERRORES.md) | Qué código HTTP devuelve cada cosa, y quién lo decide |
| [REQUISITOS_FUNCIONALES](docs/dominio/REQUISITOS_FUNCIONALES.md) | Los RF de las cinco versiones, **con los cinco verbos recurso por recurso** |
| [REQUISITOS_NO_FUNCIONALES](docs/dominio/REQUISITOS_NO_FUNCIONALES.md) | Los RNF **con su forma de comprobarlos**, y lo que el sistema NO promete |
| [DATOS_DE_PRUEBA](docs/dominio/DATOS_DE_PRUEBA.md) | Qué trae la base de datos sembrada y **para qué sirve cada fila** |
| [MANUAL_DE_MARCA](docs/dominio/MANUAL_DE_MARCA.md) | La paleta **con sus contrastes WCAG calculados**, no estimados |
| [PLAN_V1](docs/dominio/PLAN_V1.md) · [PLAN_V2](docs/dominio/PLAN_V2.md) · [PLAN_V3](docs/dominio/PLAN_V3.md) · [PLAN_V4](docs/dominio/PLAN_V4.md) | Las decisiones **antes** de programar, y los tropiezos de cada versión |
| [**PENDIENTES**](docs/dominio/PENDIENTES.md) | **Lo que este repositorio todavía NO tiene**, con cómo se comprueba cada cosa |
| [CRONOGRAMA](docs/dominio/CRONOGRAMA.md) | Contado de `git log`, con sus huecos dichos |
| [FUENTES](docs/dominio/FUENTES.md) | **De dónde salió todo, y de dónde NO** |
| [SUSTENTACION_DEL_CODIGO](docs/dominio/SUSTENTACION_DEL_CODIGO.md) | **Diez preguntas sobre este código, respondidas** |
| [elicitacion/](docs/dominio/elicitacion/1_PREGUNTAS.md) | ⚠ **SIMULADA**, y lo advierte en su primera línea |

> **Por dónde empezar, según para qué:**
>
> | Si usted… | Lea |
> |---|---|
> | va a **sustentar** su código | [SUSTENTACION_DEL_CODIGO](docs/dominio/SUSTENTACION_DEL_CODIGO.md) |
> | no entiende **por qué** algo quedó así | [DISENO_BD](docs/dominio/DISENO_BD.md) y [FUENTES](docs/dominio/FUENTES.md) |
> | va a **probar** el sistema | [DATOS_DE_PRUEBA](docs/dominio/DATOS_DE_PRUEBA.md) |
> | va a **tocar** el código | [ARQUITECTURA](docs/dominio/ARQUITECTURA.md), por las prohibiciones |
> | quiere saber **qué falta** | [PENDIENTES](docs/dominio/PENDIENTES.md) |

> ### Y una advertencia sobre copiar de aquí, que en ESTE curso es la principal
>
> **El proyecto de aula de Construcción de Software es el de cátedras**, no
> `bdfacturas`. Lo que usted ve aquí es el sistema que el profesor construye a
> la vista para enseñar el método; lo que su equipo entrega es **su** proyecto,
> con la metodología de
> [`ProyectosDeAula/docs/0_METODOLOGIA.md`](ProyectosDeAula/docs/0_METODOLOGIA.md).
>
> Y este ejemplo toma decisiones que **en el proyecto de aula no se valen**, y
> están señaladas donde aparecen: el motor **viene dado** (aquí) contra **lo
> escoge el equipo** (allá); el modelo de datos **llegó hecho** (aquí) contra
> **se elicita primero** (allá). Ver [FUENTES](docs/dominio/FUENTES.md).

---

*Proyecto Construcción de Software · USB Medellín · Base de datos bdfacturas
(facturación + RBAC).*

# El proyecto de aula del curso

**El proyecto de aula de Construcción de Software es CÁTEDRAS ABIERTAS**:

> *Sistema de registro y control de asistencia a las cátedras abiertas de la
> Universidad de San Buenaventura, Medellín.*

**El dominio es uno solo para todo el curso**: no hay un proyecto por equipo,
todos los equipos construyen el mismo sistema.

Y **todo lo que hace falta para construirlo está en este repositorio**, en
[`docs/conceptos/`](docs/conceptos/) y [`docs/dominio/`](docs/dominio/): los
requisitos, las reglas de negocio, el diseño de la base de datos, la
elicitación completa y los documentos conceptuales del curso. No hay que
pedir acceso a ningún otro sitio.

---

## Entonces, ¿qué es `bdfacturas`?

**El ejemplo del profesor, no el proyecto de aula.** Este repositorio
construye `bdfacturas` de principio a fin —cuatro versiones, con su spec kit,
su API y su interfaz gráfica— para que usted vea **el método aplicado
completo** antes de aplicarlo a cátedras abiertas.

| | |
|---|---|
| **`bdfacturas`** (este repositorio) | El dominio del EJEMPLO: facturas, clientes, productos |
| **Cátedras abiertas** (el otro) | El dominio que USTED entrega |
| **Lo que se copia entre los dos** | **El método, no el dominio** |

> **Y el dominio no se deduce del ejemplo.** Cátedras abiertas no tiene
> facturas ni stock: tiene cátedras, asistentes y asistencias. Las reglas de
> negocio hay que **elicitarlas** — por eso la elicitación es parte de la nota.

---

## Lo que SÍ está aquí: el método

Es lo único que los dos proyectos comparten, y por eso se guarda al lado del
ejemplo.

| Archivo | Para qué sirve |
|---|---|
| [`docs/0_METODOLOGIA.md`](docs/0_METODOLOGIA.md) | **Las cuatro versiones**, el calendario y la rúbrica — incluido el **20 % de interpretabilidad** desde la v2. Es lo que ordena [`0_mapa_versiones.md`](../docs/spec_kit/versiones/0_mapa_versiones.md) |
| [`docs/VERSION_2.md`](docs/VERSION_2.md) | Qué se espera de la versión 2, criterio por criterio |

---

## Y aquí está el proyecto de aula entero, para leerlo sin cambiar de repositorio

Los documentos de **cátedras abiertas** están copiados aquí, en dos carpetas,
tal como viven en su repositorio:

| Carpeta | Qué contiene |
|---|---|
| [`docs/conceptos/`](docs/conceptos/) | Los **21 documentos conceptuales** del curso: elicitación, historias de usuario, plan de desarrollo, requisitos, reglas de negocio, diseño de base de datos, control de acceso, arquitectura, SOLID, ACID, Docker, ramas y colaboración, identidad visual… |
| [`docs/dominio/`](docs/dominio/) | Los **16 documentos del dominio de cátedras**: requisitos funcionales y no funcionales, reglas de negocio, diseño de la base de datos, glosario, política de errores, datos de prueba, cronograma, manual de marca, los planes de la v1 y la v2, la sustentación del código, las fuentes — y la [`elicitacion/`](docs/dominio/elicitacion/) completa: preguntas, respuestas e historias |

**Los tres `CONCEPTOS_*.md` que estaban sueltos en `docs/` ya no están**, y no
es un descuido: eran los mismos tres, en una versión anterior y más corta. Los
de `docs/conceptos/` traen además la sección «En este proyecto» —que apunta al
`PLAN_V1.md` de cátedras, aquí al lado en `docs/dominio/`— y las referencias
bibliográficas. Tener dos versiones del mismo documento garantiza que alguien
lea la vieja.

> **Y esto no cambia de qué es el ejemplo y de qué es la entrega.**
> `bdfacturas` sigue siendo el ejemplo del profesor y cátedras abiertas sigue
> siendo el proyecto de aula. Lo que se copia entre los dos es el **método**.
> Estas dos carpetas están aquí para que el método y el dominio al que se
> aplica se puedan leer uno junto al otro, en un mismo sitio, sin cambiar de
> repositorio a mitad de una clase.
>
> La copia **se actualiza desde `proyecto_catedras2`**, que es donde esos
> documentos se escriben. Si los dos difieren, el que manda es el del
> repositorio de cátedras.

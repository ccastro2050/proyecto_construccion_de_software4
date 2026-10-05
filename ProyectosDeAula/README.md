# ProyectosDeAula — el proyecto de aula del curso

**En Construcción de Software el proyecto de aula es UNO, y es el de
cátedras.** No hay un proyecto por equipo: todos trabajan sobre el mismo, en
sus cuatro módulos.

| Módulo | Qué cubre | Su base de datos |
|---|---|---|
| [Gestión profesoral](docs/modulo_gestion_profesoral.md) | La hoja de vida académica y la carga docente | [`gestion_profesoral.pg.sql`](db_scripts/postgresql/gestion_profesoral.pg.sql) |
| [Innovación curricular](docs/modulo_innovacion_curricular.md) | Los cambios de plan de estudios y su trazabilidad | [`innovacion_curricular.pg.sql`](db_scripts/postgresql/innovacion_curricular.pg.sql) |
| [Investigación](docs/modulo_investigacion.md) | Grupos, semilleros y productos de investigación | [`investigacion.pg.sql`](db_scripts/postgresql/investigacion.pg.sql) |
| [Mapa de conocimiento](docs/modulo_mapa_conocimiento.md) | Quién sabe qué, y dónde está ese saber | [`mapa_conocimiento.pg.sql`](db_scripts/postgresql/mapa_conocimiento.pg.sql) |

> **Y cómo encajan los cuatro** está en
> [`docs/proyecto_completo.md`](docs/proyecto_completo.md). La base completa,
> con los cuatro módulos juntos, es
> [`knowledge_map_db_completa.pg.sql`](db_scripts/postgresql/knowledge_map_db_completa.pg.sql).

---

## Entonces, ¿qué es `bdfacturas`?

**El ejemplo del profesor, no el proyecto de aula.** Este repositorio
construye `bdfacturas` de principio a fin —cuatro versiones, con su spec kit,
su API y su interfaz gráfica— para que el estudiante vea **el método
aplicado completo** antes de aplicarlo a su módulo.

| | |
|---|---|
| **`bdfacturas`** | El dominio del EJEMPLO. Facturas, clientes, productos |
| **Su módulo de cátedras** | El dominio que USTED entrega |
| **Lo que se copia entre los dos** | **El método, no el dominio** |

---

## El método, que sí es el mismo para los dos

| Archivo | Para qué lo usa este proyecto |
|---|---|
| [`docs/0_METODOLOGIA.md`](docs/0_METODOLOGIA.md) | **Las cuatro versiones**, el calendario y la rúbrica — incluido el **20 % de interpretabilidad** desde la v2. Es lo que ordena [`0_mapa_versiones.md`](../docs/spec_kit/versiones/0_mapa_versiones.md) |
| [`docs/VERSION_2.md`](docs/VERSION_2.md) | Qué se espera de la versión 2, criterio por criterio |
| [`docs/CONCEPTOS_ELICITACION.md`](docs/CONCEPTOS_ELICITACION.md) | Cómo se levantan los requisitos |
| [`docs/CONCEPTOS_HISTORIAS_DE_USUARIO.md`](docs/CONCEPTOS_HISTORIAS_DE_USUARIO.md) | Cómo se escriben las historias |
| [`docs/CONCEPTOS_PLAN_DE_DESARROLLO.md`](docs/CONCEPTOS_PLAN_DE_DESARROLLO.md) | Cómo se reparte el trabajo en versiones |

> **Lo que NO se copia es el dominio.** Un módulo de cátedras no tiene
> facturas ni stock: tiene profesores, cursos, grupos o saberes. Las reglas de
> negocio hay que **elicitarlas**, no deducirlas del ejemplo — y por eso la
> elicitación es parte de la nota.

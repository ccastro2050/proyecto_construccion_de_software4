# Requisitos no funcionales — Cátedras Abiertas

> **Qué es este archivo.** El catálogo de **qué tan bien** tiene que hacer el
> sistema lo que hace. Cada uno con su **número, su unidad y su forma de
> medirlo** — sin las tres cosas no entra.
>
> **Cómo se escribe uno y por qué el nombre es malo:**
> [`../conceptos/CONCEPTOS_REQUISITOS_NO_FUNCIONALES.md`](../conceptos/CONCEPTOS_REQUISITOS_NO_FUNCIONALES.md).
>
> Versión 1.0 · 14 de septiembre de 2026.

---

## 0. Cómo leer este catálogo

Está organizado por las **nueve características de ISO/IEC 25010:2023**, y se
recorren **todas**, incluidas las que no aplican — porque declarar que no
aplica es una respuesta y el silencio no.

| Columna | Qué dice |
|---|---|
| **ID** | `RNF-nn`. Estable, y **no se reutiliza** |
| **Umbral** | El número y su unidad |
| **Cómo se mide** | El comando o la acción. Sin esto, el requisito no existe |
| **Rige** | `constitución` si es transversal · la versión si es de una sola |
| **Estado** | `medido` · `verificado` · `acordado, sin medir` · `propuesto` |

### Dos avisos sobre los umbrales

> **1 · Los que dicen «propuesto» son hipotéticos y así están marcados.** Este
> proyecto no ha estado en producción: no hay una línea base real de la que
> sacar los números. Los umbrales de desempeño y capacidad son **puntos de
> partida razonables para medir contra ellos**, no promesas contractuales. El
> primero que se mida los corrige.
>
> **2 · Todos los datos del sistema son hipotéticos.** Las mediciones se hacen
> sobre datos de ejemplo. Cuando el usuario experto cargue los reales, **hay
> que volver a medir** — y los umbrales de capacidad son los primeros que van a
> moverse.

---

## 1. Adecuación funcional

| ID | Requisito | Umbral | Cómo se mide | Rige | Estado |
|---|---|---|---|---|---|
| **RNF-01** | Todo requisito funcional acordado para una versión está construido antes de cerrarla | **100 %** de los `RF-nn` de esa versión | La matriz de `REQUISITOS_FUNCIONALES.md` §6 contra `9_checklist` | constitución | verificado en v1 |
| **RNF-02** | Todo criterio de aceptación se puede **ejecutar** | 100 % de los criterios tienen comando | `7_quickstart` tiene una entrada por criterio de `2_spec` | constitución | verificado en v1 |

---

## 2. Eficiencia de desempeño

| ID | Requisito | Umbral | Cómo se mide | Rige | Estado |
|---|---|---|---|---|---|
| **RNF-03** | Un listado responde rápido en condiciones normales | **mediana < 200 ms**, con 10 peticiones concurrentes | 100 repeticiones de `GET /api/sede`, se toma la mediana | constitución | **propuesto** |
| **RNF-04** | El sistema **acota** cuántas filas devuelve un listado | por defecto **100**, máximo **1.000** | `GET /api/sede?limite=999999` devuelve 1.000, no más | constitución | **propuesto** |
| **RNF-05** | La respuesta **dice qué límite se aplicó**, para que el cliente sepa si lo truncaron | el cuerpo o la cabecera lo indica | Pedir más del máximo y leer la respuesta | constitución | **propuesto** |

> **RNF-04 y RNF-05 existen por una lección ajena, y vale la pena contarla.**
> En otra API —genérica, de este mismo autor— el límite por defecto era
> `limite ?? 50000` **y distinto según el motor**: cincuenta mil filas contra
> PostgreSQL, mil contra Oracle, **con el mismo endpoint y sin que el contrato
> lo dijera**. No lo decidió nadie: **nadie escribió el requisito**, y el valor
> lo puso quien escribió la línea.
>
> **Y RNF-05 es el que casi nadie pone:** hoy, en la mayoría de las APIs, no hay
> forma de distinguir *«hay mil filas»* de *«había más y te di mil»*.

---

## 3. Compatibilidad

| ID | Requisito | Umbral | Cómo se mide | Rige | Estado |
|---|---|---|---|---|---|
| **RNF-06** | El archivo que se envía al ASIS cumple **exactamente** su formato | **3 columnas**, ni una más | Comparar la salida contra el formato del manual de migración | v4 | acordado, sin medir |
| **RNF-07** | El sistema arranca con **un solo comando**, sin instalar nada en la máquina | **1** comando | `docker compose up -d --build` desde cero | constitución | verificado en v1 |

---

## 4. Capacidad de interacción *(antes «usabilidad»)*

| ID | Requisito | Umbral | Cómo se mide | Rige | Estado |
|---|---|---|---|---|---|
| **RNF-08** | El contraste del texto cumple **WCAG 2.2 nivel AA** | **4,5:1** texto normal · **3:1** texto grande y elementos no textuales | Se **calcula** con `(L_claro + 0,05)/(L_oscuro + 0,05)` sobre las combinaciones que el front usa de verdad | constitución | **medido** |
| **RNF-09** | La pantalla **no le habla al usuario en jerga** | **0** apariciones | Ninguna pantalla contiene «PUT», «PATCH», «422», «500», «Flask», «PostgreSQL» ni un nombre de tabla | constitución | verificado en v1 |
| **RNF-10** | El front **no muestra un botón** para una operación que el recurso no admite | 0 botones de más | Las pantallas de `rol`, `estado_proceso` y `tipo_pregunta` no pintan «Retirar» | constitución | verificado en v1 |
| **RNF-11** | Una lista vacía **se explica**, no parece un error | mensaje presente | Vaciar el catálogo y mirar la pantalla | constitución | verificado en v1 |
| **RNF-12** | Todo formulario se puede recorrer **solo con el teclado** | 100 % de los campos alcanzables con `Tab` | Recorrer los once formularios sin tocar el ratón | constitución | **propuesto** |

> **RNF-08 estaba en el manual de marca pero no era un requisito**, y esa es la
> diferencia que importa: **se cumplía por accidente**. Si mañana alguien
> cambia un color y el contraste baja a 3,8:1, sin este requisito **nadie se
> entera**.
>
> **Y no basta con pasarlo.** Hay evidencia de que cumplir el 4,5:1 **no
> garantiza** que lo vea alguien con deficiencia de visión del color. El umbral
> es el mínimo legal, no el objetivo.

---

## 5. Fiabilidad

| ID | Requisito | Umbral | Cómo se mide | Rige | Estado |
|---|---|---|---|---|---|
| **RNF-13** | El front **sigue en pie con la API apagada**, y lo explica | 0 pantallas en blanco | Se apaga `api-catedras` y se navegan las once pantallas | constitución | verificado en v1 |
| **RNF-14** | La base de datos **se restaura desde un respaldo**, y está **probado** | restauración completa **< 15 min** | Restaurar en una base vacía y correr la prueba de humo contra ella | constitución | **propuesto** |
| **RNF-15** | Un fallo de la base de datos **no tumba la API**: responde con un error legible | 0 caídas del proceso | Apagar PostgreSQL con la API encendida y pedir un listado | constitución | **propuesto** |

> **RNF-14 es el que este proyecto no tiene y debería.** Hay una base con
> datos, un `init.sql` que la reconstruye, y **nadie ha probado nunca una
> restauración**. Un respaldo que no se ha restaurado no es un respaldo: es un
> archivo.

---

## 6. Seguridad

| ID | Requisito | Umbral | Cómo se mide | Rige | Estado |
|---|---|---|---|---|---|
| **RNF-16** | **Todo** SQL va parametrizado | **0** concatenaciones de valores en SQL | Revisión de los diez repositorios; ninguna interpolación de valor | constitución | verificado en v1 |
| **RNF-17** | **Ningún secreto** en el código ni en el repositorio | 0 credenciales versionadas | Las cadenas de conexión vienen de variables de entorno | constitución | verificado en v1 |
| **RNF-18** | **Ningún dato de persona real**, en la base de datos ni en el repositorio | **0** | Los datos sembrados son inventados; los correos usan un dominio de ejemplo | constitución | verificado en v1 |
| **RNF-19** | **Ninguna contraseña en claro**, nunca | 0 | Se guarda solo el resumen cifrado | v3 | acordado, sin medir |
| **RNF-20** | Toda modificación de un registro de asistencia queda **en bitácora**, con el antes y el después | 100 % de las modificaciones | Modificar un registro y leer `bitacora` | v4 | acordado, sin medir |
| **RNF-21** | El control de acceso está **en el servicio**, no en la pantalla | 0 rutas accesibles sin permiso | Llamar la ruta directamente, saltándose el front | v3 | acordado, sin medir |

> **RNF-18 es más fuerte de lo que parece, y no depende de que el repositorio
> sea privado.** La visibilidad se cambia con un clic y el clic no revisa el
> historial. La regla útil es la que no depende de la configuración: **el dato
> personal no entra**.

---

## 7. Mantenibilidad

| ID | Requisito | Umbral | Cómo se mide | Rige | Estado |
|---|---|---|---|---|---|
| **RNF-22** | **Tres capas con interfaces**, en todos los recursos | 100 % de los recursos | La prueba de capas **compila y corre sin el paquete del motor** | constitución | verificado en v1 |
| **RNF-23** | El controlador **no sabe SQL** y el servicio **no sabe HTTP** | 0 infracciones | Búsqueda de `SELECT`/`SqlConnection` en `Controllers/` y de `IActionResult`/`[Http…]` en `Servicios/` | constitución | verificado en v1 |
| **RNF-24** | **Todo cuerpo de petición tiene una clase tipada**; ninguno llega como diccionario | 0 diccionarios | El contrato publica un esquema por cada cuerpo | constitución | verificado en v1 |
| **RNF-25** | La documentación **es consistente consigo misma**: sin enlaces rotos ni referencias colgantes | **0** de cada | `python pruebas_humo/auditar_documentacion.py` | constitución | **medido** |

> **RNF-22 se verifica de una forma que vale la pena señalar:** la prueba de
> capas **no tiene el paquete del motor de base de datos**. Si el servicio
> hubiera dejado entrar una dependencia del motor, **no compila**. La
> separación no se revisa leyendo: la revisa el compilador.

---

## 8. Flexibilidad *(antes «portabilidad»)*

| ID | Requisito | Umbral | Cómo se mide | Rige | Estado |
|---|---|---|---|---|---|
| **RNF-26** | Cambiar de motor de base de datos **no toca los servicios**: solo se escribe otro repositorio | 0 archivos de servicio modificados | Escribir un repositorio alterno y contar qué más hubo que tocar | constitución | acordado, sin medir |
| **RNF-27** | El sistema corre **igual en Windows, macOS y Linux** | 3 de 3 | `docker compose up` en los tres | constitución | acordado, sin medir |
| **RNF-28** | El front **puede reemplazarse entero** sin tocar la API | 0 archivos de la API modificados | El contrato es lo único que los une | constitución | acordado, sin medir |

---

## 9. Seguridad física *(safety)* — **no aplica, y se declara**

> Este sistema **registra asistencia a eventos académicos**. No controla
> maquinaria, no administra medicamentos, no toma decisiones sobre la
> integridad de nadie. **Un fallo suyo no puede causar daño físico.**
>
> Se declara expresamente en vez de dejarlo en blanco: la característica se
> recorrió, y la respuesta es que no aplica. Si alguna versión futura llegara a
> controlar un acceso físico —un torniquete, una puerta—, **esta sección deja
> de estar vacía** y hay que volver sobre ella.

---

## 10. Las tensiones, y qué se sacrificó

No se resuelven: se ponderan, y se escribe el costo.

| Tensión | Cómo se resolvió aquí | Qué se sacrificó |
|---|---|---|
| **RNF-23** (capas) ↔ simplicidad | Se eligen las capas | **90 archivos** para diez recursos. Un cambio transversal hay que hacerlo diez veces |
| **RNF-24** (tipado) ↔ flexibilidad | Se elige el tipado | Agregar un campo obliga a tocar tres clases, no una |
| **RNF-20** (bitácora) ↔ desempeño | Se elige la bitácora | Cada modificación cuesta una escritura más |
| **RNF-18** (sin datos reales) ↔ realismo de la prueba | Se eligen los datos inventados | Los umbrales de capacidad **no están validados con volumen real** |
| **RNF-04** (tope de filas) ↔ comodidad del cliente | Se elige el tope | Un cliente que necesite más tiene que paginar |

> **La cuarta fila es la más honesta de la tabla.** Al no tener datos reales,
> `RNF-03` y `RNF-04` se miden sobre un volumen que no es el de producción. Se
> sabe, se dice, y se vuelve a medir cuando haya datos.

---

## 11. Recuento

| | |
|---|---|
| Requisitos no funcionales | **28** |
| Características de 25010 recorridas | **9 de 9** — una declarada «no aplica» |
| Verificados en la v1 | **13** |
| Medidos | **2** |
| Acordados, sin medir | **7** |
| **Propuestos (umbral hipotético)** | **6** |

*(13 + 2 + 7 + 6 = 28. Contados con el script, no a ojo.)*

**Los seis propuestos son los que hay que medir primero**, y son justo los que
este proyecto nunca miró: desempeño (RNF-03), capacidad (RNF-04, RNF-05),
teclado (RNF-12), restauración (RNF-14) y caída de la base de datos (RNF-15).

---

## 12. Historial

| Versión | Fecha | Qué cambió |
|---|---|---|
| 1.0 | 14 sep 2026 | Primera versión. Absorbe los ocho `RNF1`–`RNF8` que estaban sueltos en `2_spec` de la v1 y les agrega los de **calidad** que no existían: desempeño, capacidad, accesibilidad como requisito, fiabilidad y recuperación. Se recorren las nueve características de ISO/IEC 25010:2023, incluida la que no aplica |

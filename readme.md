# 🎮 2D Platformer: Movement, Animation & Extended Camera System

Este repositorio contiene los módulos y scripts de control físico, ciclo de animaciones 2D y el sistema de seguimiento de cámara sobre un nivel extendido con Tilemaps en Unity.

---

## 📸 Vista General y Arquitectura del Nivel

### Recorrido y Diseño del Nivel (Gameplay)
![Vista del Nivel](Entregable/Vista2.PNG)

### Configuración Técnica de Tilemaps y Colisiones (Inspector)
![Configuración Técnica y Colisiones](Entregable/Vista4.PNG)

---

## 📹 Video Demostrativo

Puedes consultar el recorrido completo y el comportamiento dinámico de la cámara en el siguiente enlace:
🔗 **[Ver Video Demostrativo (Google Drive)](https://drive.google.com/file/d/1RtJQNFs8sYJJmL7QVIqkWBqUuMEpf9uO/view)**

---

## 🚀 Taller 4: Diseño de Niveles Extendidos y Cámara Suave

En esta etapa se diseñó un escenario extendido con desniveles y vacíos para saltos, estructurado en capas visuales y de colisión:

- **Estructura de Tilemaps (`Grid`):**
  - **`Suelo_Tilemap`:** Contiene las superficies transitables configuradas con `Tilemap Collider 2D` y `Composite Collider 2D` en modo **`Merge`** (`Static`), eliminando costuras internas (*ghost collisions*) y con la etiqueta `Ground`.
  - **`Decoracion_Tilemap`:** Diseñado sin colliders para objetos ornamentales y paredes profundas de fondo.
- **Fondo Global Multicapa (`Background`):** Imágenes de fondo organizadas con valores negativos en `Order in Layer` (ej. `-100`) para garantizar su visualización por detrás del jugador y el terreno.
- **Seguimiento de Cámara (`CameraFollow.cs`):**
  - Ejecución en **`LateUpdate()`** para evitar el temblor de pantalla (*jittering*), esperando a que el personaje termine de procesar su movimiento físico.
  - Interpolación suave mediante **`Vector3.Lerp()`** multiplicada por `Time.deltaTime` para un movimiento fluido e independiente del framerate.
  - Configuración de un **Offset** adecuado (`Vector3(0, 2, -10)`) que previene recortes en el plano 2D y mejora la visibilidad del horizonte.

---

## 🏃 Taller 3: Sistema de Control y Animaciones del Personaje

- **Movimiento Horizontal:** Desplazamiento reactivo con físicas mediante `linearVelocity` y lectura del eje horizontal (`Input.GetAxis`).
- **Sistema de Volteo Dinámico (`Flip`):** Inversión del eje $X$ en `transform.localScale` al cambiar de dirección para reutilizar sprites sin duplicar assets.
- **Control de Salto y Detección de Suelo:** Detección de colisión con la etiqueta `Ground` vía `OnCollisionEnter2D` y `OnCollisionExit2D`, previniendo saltos infinitos en el aire.
- **Máquina de Estados en el Animator:** Transiciones sincronizadas de baja latencia (`Has Exit Time` desactivado) entre los estados `Idle`, `Walk` y `Jump`.

### ⚙️ Parámetros del Animator

| Parámetro | Tipo | Uso |
| :--- | :--- | :--- |
| **`Speed`** | `Float` | Controla la transición entre reposo (`Idle`) y movimiento (`Walk`) evaluando `Mathf.Abs(move)`. |
| **`isJumping`** | `Bool` | Activa la animación de salto (`Jump`) y devuelve el control a `Idle` o `Walk` al colisionar con el suelo. |

### 🔄 Lógica de Transiciones

```text
       [ Idle ]  <==== Speed >= 0.1 ====>  [ Walk ]
          ||                                  ||
      isJumping                           isJumping
          ||                                  ||
          \/                                  \/
       ================ [ Jump ] ================
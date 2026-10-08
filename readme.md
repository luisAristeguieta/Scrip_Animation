# 🎮 2D Platformer Character Controller & Animation System (Taller 3)

Este módulo implementa el control físico y el ciclo de animaciones 2D para el personaje principal utilizando el motor de físicas de Unity (`Rigidbody2D`) y el componente `Animator`.

---

## 🚀 Características Implementadas

* **Movimiento Horizontal:** Desplazamiento reactivo con físicas mediante `linearVelocity` y lectura del eje horizontal (`Input.GetAxis`).
* **Sistema de Volteo Dinámico (`Flip`):** Inversión del eje $X$ en `transform.localScale` al cambiar de dirección para reutilizar sprites sin duplicar assets.
* **Control de Salto y Detección de Suelo:** Detección de colisión con la etiqueta `Ground` vía `OnCollisionEnter2D` y `OnCollisionExit2D`, previniendo saltos infinitos en el aire.
* **Máquina de Estados en el Animator:** Transiciones sincronizadas de baja latencia (`Has Exit Time` desactivado) entre los estados `Idle`, `Walk` y `Jump`.

---

## ⚙️ Parámetros del Animator

| Parámetro | Tipo | Uso |
| :--- | :--- | :--- |
| **`Speed`** | `Float` | Controla la transición entre reposo (`Idle`) y movimiento (`Walk`) evaluando `Mathf.Abs(move)`. |
| **`isJumping`** | `Bool` | Activa la animación de salto (`Jump`) y devuelve el control a `Idle` o `Walk` al colisionar con el suelo. |

---

## 🔄 Lógica de Transiciones

```text
       [ Idle ]  <==== Speed >= 0.1 ====>  [ Walk ]
          ||                                  ||
      isJumping                           isJumping
          ||                                  ||
          \/                                  \/
       ================ [ Jump ] ================
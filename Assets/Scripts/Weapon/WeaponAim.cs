using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponAim : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera mainCamera;

    private void Start()
    {
        if (mainCamera == null) mainCamera = Camera.main;
    }

    private void Update()
    {
        RotateTowardsMouse();
    }

    private void RotateTowardsMouse()
    {
        if (Mouse.current == null) return;

        Vector2 mouseScreenPosition = Mouse.current.position.ReadValue();
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(mouseScreenPosition);
        mouseWorldPosition.z = 0f;

        Vector3 aimDirection = (mouseWorldPosition - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        // 1. Aplicamos la rotación normal
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        // 2. ¡EL ARREGLO! Si el arma apunta a la izquierda, la damos vuelta en el eje Y.
        Vector3 aimLocalScale = Vector3.one;
        if (angle > 90 || angle < -90)
        {
            aimLocalScale.y = -1f; // La endereza cuando mira a la izquierda
        }
        else
        {
            aimLocalScale.y = 1f;  // La deja normal cuando mira a la derecha
        }
        transform.localScale = aimLocalScale;
    }
}
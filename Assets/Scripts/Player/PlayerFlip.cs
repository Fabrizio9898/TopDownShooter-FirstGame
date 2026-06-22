using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFlip : MonoBehaviour
{
    [Header("Referencias")]
    [Tooltip("El SpriteRenderer del cuerpo del Player")]
    [SerializeField] private SpriteRenderer playerSprite;

    [Tooltip("La cámara principal para calcular el mouse")]
    [SerializeField] private Camera mainCamera;

    private void Start()
    {
        if (mainCamera == null) mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Mouse.current == null || playerSprite == null) return;

        // 1. Buscamos dónde está el mouse en el mundo
        Vector2 mouseScreenPosition = Mouse.current.position.ReadValue();
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(mouseScreenPosition);

        if (mouseWorldPosition.x < transform.position.x)
        {
            // El mouse está a la izquierda: Espejamos el dibujo
            playerSprite.flipX = true;
        }
        else
        {
            // El mouse está a la derecha: Lo dejamos normal
            playerSprite.flipX = false;
        }
    }
}
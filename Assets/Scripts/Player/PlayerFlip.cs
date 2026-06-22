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

        Vector2 mouseScreenPosition = Mouse.current.position.ReadValue();
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(mouseScreenPosition);

        if (mouseWorldPosition.x < transform.position.x)
        {
            playerSprite.flipX = true;
        }
        else
        {
            playerSprite.flipX = false;
        }
    }
}
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraFollowPlayer : MonoBehaviour
{
    [Header("Referencias")]
    [Tooltip("El Transform del jugador")]
    [SerializeField] private Transform player;
    [Tooltip("La cámara que vamos a mover (si está vacío, busca la principal)")]
    [SerializeField] private Camera cam;

    [Header("Configuración del Game Feel")]
    [Tooltip("Qué tan suave sigue la cámara. Menor número = más rígida.")]
    [SerializeField] private float smoothTime = 0.15f;
    [Tooltip("Radio máximo (en unidades) que la cámara puede alejarse del jugador")]
    [SerializeField] private float maxPeekDistance = 4f;
    [Tooltip("Qué tanta bola le da al mouse (0 = pegada al jugador, 1 = en la posición exacta del mouse)")]
    [Range(0f, 1f)]
    [SerializeField] private float mouseInfluence = 0.35f;

    private Vector3 velocity = Vector3.zero;

    private void Start()
    {
        if (cam == null) cam = Camera.main;
    }

    // Usamos LateUpdate para la cámara, asegura que el jugador ya se movió este frame
    private void LateUpdate()
    {
        if (player == null || Mouse.current == null) return;

        // 1. Obtener la posición del mouse en coordenadas del mundo
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
        Vector3 mouseWorldPos = cam.ScreenToWorldPoint(new Vector3(mouseScreenPos.x, mouseScreenPos.y, Mathf.Abs(cam.transform.position.z)));
        mouseWorldPos.z = 0f;

        // 2. Calcular la dirección hacia el mouse y limitar su máxima distancia (evita que vuele si tenés monitor ultra-wide)
        Vector3 mouseOffset = mouseWorldPos - player.position;
        Vector3 clampedOffset = Vector3.ClampMagnitude(mouseOffset, maxPeekDistance);

        // 3. La cámara va a buscar el punto medio entre el jugador y esa influencia del mouse
        Vector3 targetPosition = player.position + (clampedOffset * mouseInfluence);

        // Mantenemos la Z original de la cámara (ej: -10) para no perder de vista los sprites
        targetPosition.z = transform.position.z;

        // 4. Mover la cámara con una interpolación matemática suave (SmoothDamp)
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
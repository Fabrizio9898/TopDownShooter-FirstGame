using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Configuración")]
    [Tooltip("Velocidad de movimiento del jugador")]
    [SerializeField] private float moveSpeed = 4f;

    [Header("Referencias")]
    [SerializeField] private Animator animator;

    private Rigidbody2D rb;
    private PlayerInput playerInput;
    private InputAction moveAction;
    private Vector2 moveInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();

        moveAction = playerInput.actions["Move"];
    }

    private void Update()
    {
        moveInput = moveAction.ReadValue<Vector2>();

        bool isMoving = moveInput.magnitude > 0f;
        if (animator != null)
        {
            animator.SetBool("isWalking", isMoving);
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = moveInput * moveSpeed;
    }
}
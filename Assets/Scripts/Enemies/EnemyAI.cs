using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    [Tooltip("Velocidad a la que el enemigo se mueve hacia el jugador")]
    [SerializeField] private float moveSpeed = 3f;

    [Header("Configuración de Distancia")]
    [Tooltip("Distancia a la que se detiene el agente para atacar")]
    [SerializeField] private float stopDistance = 1.2f;

    [Header("Referencias")]
    [SerializeField] private Animator animator;

    private Transform player;
    private NavMeshAgent navMeshAgent;
    private Vector3 originalScale;
    private readonly int isWalking = Animator.StringToHash("isWalking");
    private void Start()
    {
        
        navMeshAgent = GetComponent<NavMeshAgent>();
        originalScale = transform.localScale;
        if (navMeshAgent != null)
        {
            navMeshAgent.speed = moveSpeed;
            navMeshAgent.updateRotation = false;
            navMeshAgent.updateUpAxis = false;
            navMeshAgent.stoppingDistance = stopDistance;
        }
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogWarning("EnemyAI: No se encontró ningún objeto con el tag 'Player'.");
        }
    }

    private void Update()
    {
        if (player != null && navMeshAgent != null)
        {
            navMeshAgent.SetDestination(player.position);
            if (navMeshAgent.velocity.x > 0.01f)
            {
                transform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);

            }
            else if (navMeshAgent.velocity.x < -0.01f)
            {
                transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
            }
            if(animator!=null)
            {
                animator.SetBool(isWalking, navMeshAgent.velocity.magnitude > 0.1f);
            }
        }
    }
}
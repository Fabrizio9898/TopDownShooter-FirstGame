using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float stopDistance = 1.2f;

    private Transform player;
    private NavMeshAgent navMeshAgent;
    private Vector3 originalScale;

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
    }

    private void Update()
    {
        if (player != null && navMeshAgent != null)
        {
            navMeshAgent.SetDestination(player.position);

            // Voltear el sprite según la dirección
            if (navMeshAgent.velocity.x > 0.01f)
            {
                transform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
            }
            else if (navMeshAgent.velocity.x < -0.01f)
            {
                transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
            }
        }
    }
}
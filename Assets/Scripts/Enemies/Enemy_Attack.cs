using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [Header("Configuración de Ataque")]
    [SerializeField] private int damage = 10;
    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private float attackCooldown = 2f;

    [Header("Referencias")]
    [SerializeField] private Animator animator;

    private Transform player;
    private Health playerHealth;
    private float nextAttackTime = 0f;

    private readonly int attackTrigger = Animator.StringToHash("Attack");

    private void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
            playerHealth = playerObj.GetComponent<Health>();
        }
    }

    private void Update()
    {
        if (player == null || playerHealth == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange && Time.time >= nextAttackTime)
        {
            nextAttackTime = Time.time + attackCooldown;
            animator.SetTrigger(attackTrigger);
        }
    }

    public void ApplyDamage()
    {
        if (player == null || playerHealth == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer <= attackRange)
        {
            playerHealth.TakeDamage(damage);
        }
    }
}
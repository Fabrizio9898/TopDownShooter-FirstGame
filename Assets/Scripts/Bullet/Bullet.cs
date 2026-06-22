using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Settings")]
    [SerializeField] private float speed = 15f;
    [SerializeField] private float lifeTime = 3f;
    private int damage;

    private int collisionLayer;

    private void Start()
    {
        collisionLayer = LayerMask.NameToLayer("Collision");
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    public void SetDamage(int newDamage)
    {
        damage = newDamage;
    }

    // Este es el método que importa para detectar el choque
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("La bala chocó con: " + collision.gameObject.name);

        if (collision.TryGetComponent(out Health enemyHealth))
        {
            if (!collision.CompareTag("Player"))
            {
                Debug.Log("¡Haciendo " + damage + " de daño al enemigo!");
                enemyHealth.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
        else if (collision.gameObject.layer == collisionLayer)
        {
            Destroy(gameObject);
        }
    }
}
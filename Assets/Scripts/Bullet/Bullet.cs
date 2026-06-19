using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Settings")]
    [SerializeField] private float speed = 15f;
    [SerializeField] private float lifeTime = 3f;
    private int damage;

    private int enemyLayer;
    private int collisionLayer;

    private void Start()
    {
     
        enemyLayer = LayerMask.NameToLayer("Enemy");
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int layerTocada = collision.gameObject.layer;

        if (layerTocada == enemyLayer)
        {
         
            if (collision.TryGetComponent(out Health enemyHealth))
            {
                enemyHealth.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
        else if (layerTocada == collisionLayer)
        {
            Destroy(gameObject);
        }
    }
}
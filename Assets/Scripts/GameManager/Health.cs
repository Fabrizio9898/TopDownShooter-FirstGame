using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Configuración de Vida")]
    [Tooltip("La vida máxima del personaje")]
    public int maxHealth = 100;

    [SerializeField] private int currentHealth;

    public event Action<float> OnHealthChanged;
    public event Action OnDie;

    private void Start()
    {
        currentHealth = maxHealth;
        OnHealthChanged?.Invoke(1f);
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        float healthPercentage = (float)currentHealth / maxHealth;
        OnHealthChanged?.Invoke(healthPercentage);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        OnDie?.Invoke();
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        OnHealthChanged = null;
        OnDie = null;
    }
}
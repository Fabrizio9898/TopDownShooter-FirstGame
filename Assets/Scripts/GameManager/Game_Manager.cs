using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Pantallas de UI")]
    [Tooltip("Arrastrá acá tu panel de Victoria")]
    [SerializeField] private GameObject victoryScreen;
    [Tooltip("Arrastrá acá tu panel de Derrota")]
    [SerializeField] private GameObject defeatScreen;

    [Header("Referencias")]
    [Tooltip("Arrastrá acá a tu Player (el que tiene el script Health)")]
    [SerializeField] private Health playerHealth;

    private int totalEnemies;

    private void Start()
    {
        // 1. Nos suscribimos a la muerte del jugador
        if (playerHealth != null)
        {
            playerHealth.OnDie += TriggerDefeat;
        }

        // 2. Buscamos a TODOS los enemigos en el mapa y los contamos
        // (Asegurate de que tus enemigos tengan la etiqueta "Enemy")
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        totalEnemies = enemies.Length;

        // 3. Nos suscribimos a la muerte de CADA UNO de los enemigos
        foreach (GameObject enemyObj in enemies)
        {
            Health enemyHealth = enemyObj.GetComponent<Health>();
            if (enemyHealth != null)
            {
                enemyHealth.OnDie += EnemyDied;
            }
        }
    }

    // Esta función se ejecuta CADA VEZ que un enemigo pega el grito de "OnDie"
    private void EnemyDied()
    {
        totalEnemies--; // Restamos 1 al contador

        // Si ya no quedan enemigos... ¡Ganamos!
        if (totalEnemies <= 0)
        {
            TriggerVictory();
        }
    }

    private void TriggerDefeat()
    {
        Debug.Log("Perdiste...");
        if (defeatScreen != null) defeatScreen.SetActive(true);

        // Frena el tiempo para que el juego se detenga
        Time.timeScale = 0f;
    }

    private void TriggerVictory()
    {
        Debug.Log("¡Ganaste!");
        if (victoryScreen != null) victoryScreen.SetActive(true);

        // Frena el tiempo
        Time.timeScale = 0f;
    }
}
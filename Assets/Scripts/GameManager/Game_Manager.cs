using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Pantallas de UI")]
    [Tooltip("Arrastrá acá tu panel de Victoria")]
    [SerializeField] private GameObject victoryPanel;

    [Tooltip("Arrastrá acá tu panel de Derrota")]
    [SerializeField] private GameObject defeatPanel;


    [Header("Pausa")]
    [SerializeField] private GameObject pausePanel;
    private bool isPaused = false;


    [Header("Referencias")]
    [Tooltip("Arrastrá acá a tu Player (el que tiene el script Health)")]
    [SerializeField] private Health playerHealth;

    private int totalEnemies;

    private void Start()
    {
        if (playerHealth != null)
        {
            playerHealth.OnDie += TriggerDefeat;
        }

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        totalEnemies = enemies.Length;

        foreach (GameObject enemyObj in enemies)
        {
            Health enemyHealth = enemyObj.GetComponent<Health>();
            if (enemyHealth != null)
            {
                enemyHealth.OnDie += EnemyDied;
            }
        }
    }


    private void Update()
    {
        if (Keyboard.current != null && Keyboard.current.pKey.wasPressedThisFrame)
        {

            bool canPause = (defeatPanel == null || !defeatPanel.activeSelf) &&
                            (victoryPanel == null || !victoryPanel.activeSelf);

            if (canPause)
            {
                TogglePause();
            }
        }
    }

    private void OnDestroy()
    {
        if (playerHealth != null)
        {
            playerHealth.OnDie -= TriggerDefeat;
        }

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemyObj in enemies)
        {
            if (enemyObj != null && enemyObj.TryGetComponent(out Health enemyHealth))
            {
                enemyHealth.OnDie -= EnemyDied;
            }
        }
    }

    private void EnemyDied()
    {
        totalEnemies--; 
        if (totalEnemies <= 0)
        {
            TriggerVictory();
        }
    }

    private void TriggerDefeat()
    {
        Debug.Log("Perdiste...");
        if (defeatPanel != null) defeatPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    private void TriggerVictory()
    {
        Debug.Log("¡Ganaste!");
        if (victoryPanel != null) victoryPanel.SetActive(true);

        Time.timeScale = 0f;
    }


    public void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;

        if (pausePanel != null)
        {
            pausePanel.SetActive(isPaused);
        }
    }

    public void Retry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

}
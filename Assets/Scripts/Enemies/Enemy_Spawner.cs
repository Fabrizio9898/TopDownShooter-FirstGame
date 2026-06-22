using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Prefabs y Puntos de Spawn")]
    [SerializeField] private GameObject enemyPrefab;
    [Tooltip("Arrastrá acá los objetos vacíos que actúan como posiciones de Spawn")]
    [SerializeField] private Transform[] spawnPoints;

    [Header("Configuración")]
    [SerializeField] private int enemiesToSpawn = 6;

    private void Awake()
    {
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        if (spawnPoints.Length == 0 || enemyPrefab == null)
        {
            Debug.LogWarning("Faltan configurar los Spawn Points o el Prefab del enemigo.");
            return;
        }

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Transform randomSpawn = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(enemyPrefab, randomSpawn.position, Quaternion.identity);
        }
    }
}
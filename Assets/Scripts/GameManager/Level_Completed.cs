// using UnityEngine;

// public class LevelExit : MonoBehaviour
// {

//     private void OnTriggerEnter2D(Collider2D other)
//     {

//         if (other.CompareTag("Player"))
//         {
//             Debug.Log($"[LevelExit {levelNumber}] ¡Tag 'Player' detectado! Avisando al GameManager para el nivel {levelNumber}...");
//             GameManager.Instance.LevelCompleted(levelNumber);
//         }
//         else
//         {
//             Debug.LogWarning($"[LevelExit {levelNumber}] Ojo: Lo que me tocó NO tiene el tag 'Player'. Su tag es: '{other.tag}'");
//         }
//     }
// }
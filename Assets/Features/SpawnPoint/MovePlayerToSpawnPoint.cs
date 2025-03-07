using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpawnPoint
{
    public class MovePlayerToSpawnPoint : MonoBehaviour
    {
        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            var spawnPoint = GameObject.FindWithTag("Respawn");
            if (spawnPoint == null) return;
            transform.position = spawnPoint.transform.position;
        }
    }
}
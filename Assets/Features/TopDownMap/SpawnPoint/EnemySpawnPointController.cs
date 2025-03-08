using System.Collections;
using System.Collections.Generic;
using EnemyTopDown;
using SpawnPoint;
using UnityEngine;
using Utilities.Pooling;

namespace TopDownMap.SpawnPoint
{
    public class EnemySpawnPointController : ObjectPoolingBase<AIEnemy>
    {
        [SerializeField] private AreaSpawnPoint<AIEnemy>[] enemySpawnPoints;
        [SerializeField] private int maxEnemyCount = 5;
        [SerializeField] private float spawnDelay = 5f;

        [SerializeField] private List<AIEnemy> enemyPrefabs = new();

        private void Start()
        {
            if (enemyPrefabs.Count == 0)
            {
                Debug.LogError("Enemy Prefabs is empty");
                return;
            }

            Init(enemyPrefabs[0], maxEnemyCount);
            GetAllEnemySpawnPoint();
            StartCoroutine(StartSpawnEnemy());
        }

        IEnumerator StartSpawnEnemy()
        {
            while (true)
            {
                if (GetActiveObjectCount() < maxEnemyCount)
                {
                    var enemy = GetObjectFromPool();
                    var enemySo = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)].enemySo;
                    var enemySpawnPoint = GetRandomEnemySpawnPoint();
                    enemy.Init(enemySo, enemySpawnPoint);
                    enemy.onDeath = () => ReturnObjectToPool(enemy);
                }

                yield return new WaitForSeconds(spawnDelay);
            }
            // ReSharper disable once IteratorNeverReturns
        }

        private void GetAllEnemySpawnPoint()
        {
            enemySpawnPoints =
                FindObjectsByType<AreaSpawnPoint<AIEnemy>>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
        }

        private Vector2 GetRandomEnemySpawnPoint()
        {
            var randomIndex = Random.Range(0, enemySpawnPoints.Length);
            while (enemySpawnPoints[randomIndex].IsSpawned() || enemySpawnPoints[randomIndex].IsPlayerInRange())
            {
                randomIndex = Random.Range(0, enemySpawnPoints.Length);
            }

            return enemySpawnPoints[randomIndex].GetSpawnPosition();
        }
    }
}
using SpawnPoint.Interfaces;
using UnityEngine;

namespace SpawnPoint
{
    public class BaseSpawnPoint<T> : MonoBehaviour, ISpawnPoint<T> where T : Object
    {
        [SerializeField] protected T spawnedGameObject;

        public virtual T Spawn(Transform targetTransform)
        {
            return Instantiate(spawnedGameObject, targetTransform.position, targetTransform.rotation, transform);
        }
    }
}
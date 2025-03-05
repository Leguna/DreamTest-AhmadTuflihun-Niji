using SpawnPoint.Interfaces;
using UnityEngine;

namespace SpawnPoint
{
    public sealed class BaseSpawnPoint<T> : MonoBehaviour, ISpawnPoint<T> where T : Object
    {
        [SerializeField] private T spawnedGameObject;

        public T Spawn(Transform targetTransform)
        {
            return Instantiate(spawnedGameObject, targetTransform.position, targetTransform.rotation, transform);
        }
    }
}
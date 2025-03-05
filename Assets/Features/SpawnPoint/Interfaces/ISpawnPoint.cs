using UnityEngine;

namespace SpawnPoint.Interfaces
{
    public interface ISpawnPoint<out T>
    {
        T Spawn(Transform targetTransform);
    }
}
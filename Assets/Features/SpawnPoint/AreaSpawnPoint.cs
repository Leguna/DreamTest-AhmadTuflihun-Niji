using UnityEngine;

namespace SpawnPoint
{
    public class AreaSpawnPoint<T> : BaseSpawnPoint<T> where T : MonoBehaviour
    {
        [SerializeField] private float spawnRadius = 1f;

        public override T Spawn(Transform targetTransform)
        {
            var spawnPosition = Random.insideUnitCircle * spawnRadius;
            return Instantiate(spawnedGameObject, targetTransform.position + (Vector3)spawnPosition,
                targetTransform.rotation, transform);
        }

        public Vector3 GetSpawnPosition()
        {
            var spawnPosition = Random.insideUnitCircle * spawnRadius;
            return transform.position + (Vector3)spawnPosition;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(transform.position, spawnRadius);
        }
    }
}
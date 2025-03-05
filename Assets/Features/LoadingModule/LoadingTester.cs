using UnityEngine;

namespace LoadingModule
{
    internal class LoadingTester : MonoBehaviour
    {
        private LoadingManager _loadingManager;
        private LoadingManager _loadingPrefab;

        private void Start()
        {
            _loadingManager = TryGetComponent(out LoadingManager loadingManager)
                ? loadingManager
                : Instantiate(_loadingPrefab);
        }
    }
}
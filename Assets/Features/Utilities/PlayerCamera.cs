using Constant;
using Unity.Cinemachine;
using UnityEngine;

namespace Utilities
{
    public class PlayerCamera : MonoBehaviour
    {
        private CinemachineCamera _camera;
        private CinemachineConfiner2D _cinemachineConfiner2D;
        private Vector3 _lastPosition;

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            _camera = FindFirstObjectByType<CinemachineCamera>();
            _camera.Follow = transform;
            SearchBounding();
        }

        private void SearchBounding()
        {
            _camera.TryGetComponent(out _cinemachineConfiner2D);
            if (_cinemachineConfiner2D == null) return;
            _cinemachineConfiner2D.BoundingShape2D =
                GameObject.Find(GameConst.CameraBound).GetComponent<Collider2D>();
        }

        public void StopFollow()
        {
            _camera.Follow = null;
            _lastPosition = transform.position;
            _camera.transform.position = new Vector3(0, 0, -10);
        }

        public void SetFollow(Transform target)
        {
            _camera.transform.position = target.position;
            _camera.Follow = target;
        }

        public void StartFollow()
        {
            _camera.transform.position = _lastPosition;
            _camera.Follow = transform;
        }
    }
}
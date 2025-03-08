using System;
using Unity.Cinemachine;
using UnityEngine;

namespace Utilities
{
    public class PlayerCamera : MonoBehaviour
    {
        private CinemachineCamera _camera;
        private CinemachineConfiner2D _cinemachineConfiner2D;

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
            _cinemachineConfiner2D.BoundingShape2D = GameObject.Find(Constant.GameConst.CameraBound).GetComponent<Collider2D>();
        }
    }
}
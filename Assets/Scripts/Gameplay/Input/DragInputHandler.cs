using System;
using UnityEngine;

namespace Scripts.Input
{
    public class DragInputHandler : MonoBehaviour, IInputHandler
    {
        public event Action Done;

        [SerializeField] private float _minDistance;
        
        private Camera _camera;
        private Vector3 _mouseDownPos;
        private bool _isMouseDown;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            if(!_isMouseDown) return;
        
            if (UnityEngine.Input.GetMouseButtonUp(0))
            {
                Vector3 mouseUpPos = _camera.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
                Vector3 distance = mouseUpPos - _mouseDownPos;
                Debug.Log("dist" + distance);
                _isMouseDown = false;

                if (distance.y > _minDistance)
                    Done?.Invoke();
            }
        }

        private void OnMouseDrag()
        {
            if (_isMouseDown) return;
        
            _isMouseDown = true;
            _mouseDownPos = _camera.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
        }
    }
}
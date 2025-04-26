using System;
using Scripts.Animations;
using UnityEngine;
using UnityEngine.Serialization;

namespace Scripts.Input
{
    public class MoveInsideInputHandler : MonoBehaviour, IInputHandler
    {
        public event Action Done;
        
        private float _treshhold = 0.1f;
        private float _targetMovesAmount = 10f;
        private float _movesAmount;
        private bool _mouseInside;
        private Vector3 prevPos;
        private Camera _camera;
        private FadeDuringInput _fadeAnimation;

        private void Awake()
        {
            _camera = Camera.main;
            _fadeAnimation = GetComponent<FadeDuringInput>();
        }

        private void OnMouseDown()
        {
            if(_mouseInside) return;
            MouseInside();
        }

        private void OnMouseEnter()
        {
            if(_mouseInside) return;
            if (UnityEngine.Input.GetMouseButton(0))
                MouseInside();
        }

        private void OnMouseUp()
        {
            _mouseInside = false;
        }

        private void OnMouseExit()
        {
            _mouseInside = false;
        }

        private void MouseInside()
        {
            _mouseInside = true;
            prevPos = _camera.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
        }

        private void Update()
        {
            if(!_mouseInside) return;
            Vector3 mousePos = _camera.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
            Vector3 distance = mousePos - prevPos;
            float absX = Mathf.Abs(distance.x);
            float absY = Mathf.Abs(distance.y);

            if (absX > _treshhold || absY > _treshhold)
            {
                prevPos = mousePos;
                _movesAmount += absX;
                _movesAmount += absY;

                if (_movesAmount >= _targetMovesAmount)
                {
                    Done?.Invoke();
                    return;
                }

                float progress = _movesAmount / _targetMovesAmount;
                _fadeAnimation.Fade(progress);
            }
        }
    }
}
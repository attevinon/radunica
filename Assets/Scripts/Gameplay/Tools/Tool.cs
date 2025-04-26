using System;
using DG.Tweening;
using UnityEngine;

namespace Scripts
{
    [RequireComponent(typeof(Collider2D))]
    public class Tool : MonoBehaviour
    {
        [SerializeField] private Vector3 _offset;

        private Camera _camera;
        private bool _followMouse;
        [SerializeField] private float _tapScale;
        [SerializeField] private float _animationDuration;
        private Vector3 _startPosition;
        private Vector3 _startScale;

        protected Action _onActivated;
        private Collider2D _collider;
        private Tween _animation;
        protected bool _isInHand;
        protected virtual void Awake()
        {
            _collider = GetComponent<Collider2D>();
            _startPosition = transform.position;
            _startScale = transform.localScale;
            gameObject.SetActive(false);
        }

        public void Show(Action onActivated)
        {
            gameObject.SetActive(true);
            _collider.enabled = true;
            _onActivated = onActivated;
            //show animated
            //start pulsating
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            _isInHand = false;
            transform.position = _startPosition;
        }

        private void Update()
        {
            if(!_isInHand) return;
            Vector3 mousePos = _camera.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
            mousePos.z = 0f;
            transform.position = mousePos + _offset;
            
            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                transform
                    .DOScale(_startScale * _tapScale, _animationDuration)
                    .Play();
            }
            else if (UnityEngine.Input.GetMouseButtonUp(0))
            {
                transform
                    .DOScale(_startScale, _animationDuration)
                    .Play();
            }
        }

        protected virtual void OnMouseDown()
        {
            if(_isInHand) return;
            PlaceInHand();
            _onActivated.Invoke();
        }

        protected void PlaceInHand()
        {
            _isInHand = true;
            _collider.enabled = false;
            _camera = Camera.main;
        }
    }
}
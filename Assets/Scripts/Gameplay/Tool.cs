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

        private Collider2D _collider;
        private Action _onTakenInHand;
        private Tween _animation;
        private bool _isInHand;
        private void Awake()
        {
            _collider = GetComponent<Collider2D>();
            gameObject.SetActive(false);
        }

        public void Initialize(Action onTakenInHand)
        {
            gameObject.SetActive(true);
            _onTakenInHand = onTakenInHand;
            //show animated
            //start pulsating
        }

        private void Update()
        {
            if(!_isInHand) return;
            Vector3 mousePos = _camera.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
            mousePos.z = 0f;
            transform.position = mousePos;
            
            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                TryCompleteAnimation();
                _animation = transform.DOScale(_tapScale, _animationDuration);
            }
            else if (UnityEngine.Input.GetMouseButtonUp(0))
            {
                TryCompleteAnimation();
                _animation = transform.DOScale(1f, _animationDuration);
            }
        }
        
        private bool TryCompleteAnimation()
        {
            if (_animation != null && _animation.IsPlaying())
            {
                _animation.Complete();
                return true;
            }
            return false;
        }

        private void OnMouseDown()
        {
            _collider.enabled = false;
            _camera = Camera.main;
            _isInHand = true;
            _onTakenInHand.Invoke();
        }
    }
}
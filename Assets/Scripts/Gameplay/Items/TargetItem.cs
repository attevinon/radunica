using System;
using Scripts.Animations;
using Scripts.Input;
using UnityEngine;
using UnityEngine.Serialization;

namespace Scripts
{
    [RequireComponent(typeof(Collider2D))]
    public class TargetItem : MonoBehaviour
    {
        public event Action<TargetItem> OnDone; 
        private Collider2D _collider;
        private IInputHandler _inputHandler;
        private IAnimatable _animatable;

        private void Awake()
        {
            _inputHandler = GetComponent<IInputHandler>();
            _animatable = GetComponent<IAnimatable>();
            _collider = GetComponent<Collider2D>();
            _collider.enabled = false;
        }

        private void OnEnable()
        {
            _inputHandler.Done += OnInputDone;
        }

        private void OnDisable()
        {
            _inputHandler.Done -= OnInputDone;
        }
        
        public void EnableCollider(bool enable)
        {
            _collider.enabled = enable;
            if(_animatable is ShowFadeAnimation show)
            {
                show.ShowCircle();
            }
        }

        private void OnInputDone()
        {
            _collider.enabled = false;
            _animatable.Animate(() => OnDone?.Invoke(this));
        }
    }
}
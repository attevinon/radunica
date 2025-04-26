using System;
using UnityEngine;

namespace Scripts
{
    [RequireComponent(typeof(Collider2D))]
    public class Basin : MonoBehaviour
    {
        public event Action OnWaterClicked;

        private Collider2D _collider;
        private bool _isClicked;

        private void Awake()
        {
            _collider = GetComponent<Collider2D>();
            gameObject.SetActive(false);
        }

        private void OnMouseUpAsButton()
        {
            if(_isClicked) return;
            _collider.enabled = false;
            _isClicked = true;
            OnWaterClicked?.Invoke();
        }

        public void Activate()
        {
            _isClicked = false;
            _collider.enabled = true;
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
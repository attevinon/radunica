using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace Scripts
{
    [RequireComponent(typeof(Collider2D))]
    public class Surface : MonoBehaviour
    {
        [SerializeField] private GameObject[] _itemsPresentations; 
        [SerializeField] private GameObject[] _itemsShowAfterCleaning;
        [SerializeField] private SurfaceType _surfaceType;

        [SerializeField] private float _scaleDuration = 0.5f;
        [SerializeField] private Vector3 _animatedScale = new Vector3(1.1f, 1.1f, 1);
        
        public SurfaceType SurfaceType => _surfaceType;
        
        private Tween _animation;
        private bool _waitForSelection;
        private bool _clicked;
        private Collider2D _collider;
        private Action _onSelected;

        private void Awake()
        {
            _collider = GetComponent<Collider2D>();
            _collider.enabled = false;
        }

        public void OnCleaned()
        {
            foreach (var presentation in _itemsPresentations)
            {
                Destroy(presentation);
            }

            if (_itemsShowAfterCleaning != null && _itemsShowAfterCleaning.Length > 0)
            {
                foreach (var item in _itemsShowAfterCleaning)
                {
                    item.gameObject.SetActive(true);
                }
            }
        }

        public void WaitForSelection(Action callback)
        {
            _onSelected = callback;
            _collider.enabled = true;
            _animation = transform
                .DOScale(transform.localScale + _animatedScale, _scaleDuration)
                .SetLoops(-1, LoopType.Yoyo)
                .SetLink(gameObject)
                .Play();
        }

        private void OnMouseUpAsButton()
        {
            if(_clicked) return;
            _clicked = true;
            _collider.enabled = false;
            _onSelected.Invoke();
            _animation.Pause();
            _animation.Kill();
        }
    }
}
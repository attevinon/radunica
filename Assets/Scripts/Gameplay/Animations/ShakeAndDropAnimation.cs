using System;
using DG.Tweening;
using UnityEngine;

namespace Scripts.Animations
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class ShakeAndDropAnimation : MonoBehaviour, IAnimatable
    {
        [SerializeField] private float _moveDownDistance = 1.4f;
        [SerializeField] private float _shakeDuration = 1f;
        [SerializeField] private float _moveDelay = 0.5f;
        [SerializeField] private float _moveDuration = 0.8f;
        [SerializeField] private float _fadeDelay = 0.8f;
        [SerializeField] private float _fadeDuration = 0.3f;
        
        private SpriteRenderer _spriteRender;
        private bool _isInAnimation;

        private void Awake()
        {
            _spriteRender = GetComponent<SpriteRenderer>();
        }
        public void Animate(Action callback)
        {
            if (_isInAnimation) return;
            _isInAnimation = true;
            
            Vector3 targetPos = transform.position + Vector3.down * _moveDownDistance;
            transform
                .DOShakeRotation(_shakeDuration, new Vector3(2,2,10), 6)
                .SetLink(gameObject)
                .Play();
            transform
                .DOMove(targetPos, _moveDuration)
                .SetDelay(_moveDelay)
                .SetLink(gameObject)
                .Play();
            _spriteRender
                .DOFade(0f, _fadeDuration)
                .SetDelay(_fadeDelay)
                .SetLink(gameObject)
                .OnComplete(() =>
                {
                    callback?.Invoke();
                })
                .Play();
        }
    }
}
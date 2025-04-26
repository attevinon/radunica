using System;
using DG.Tweening;
using UnityEngine;

namespace Scripts.Animations
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class MoveUpAndDisappear : MonoBehaviour, IAnimatable
    {
        [SerializeField] private float _moveUpDistance = 1f;
        [SerializeField] private float _moveDuration = 0.8f;
        [SerializeField] private float _fadeDuration = 0.3f;
        [SerializeField] private float _fadeDelay = 0.5f;
        
        private SpriteRenderer _spriteRender;
        private bool _isInAnimation;

        private void Awake()
        {
            _spriteRender = GetComponent<SpriteRenderer>();
        }

        public void Animate(Action callback)
        {
            if (_isInAnimation) return;
            enabled = false;
            _isInAnimation = true;
            Vector3 targetPos = transform.position + Vector3.up * _moveUpDistance;
            transform
                .DOMove(targetPos, _moveDuration)
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
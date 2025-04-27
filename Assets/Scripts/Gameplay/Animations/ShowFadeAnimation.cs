using System;
using DG.Tweening;
using UnityEngine;

namespace Scripts.Animations
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class ShowFadeAnimation : MonoBehaviour, IAnimatable
    {
        [SerializeField] private SpriteRenderer _selectionCircle;
        [SerializeField] private float _fadeDuration = 0.5f;
        private SpriteRenderer _spriteRender;
        private bool _isInAnimation;

        private void Awake()
        {
            _spriteRender = GetComponent<SpriteRenderer>();
            _selectionCircle.gameObject.SetActive(false);
        }

        public void ShowCircle()
        {
            _selectionCircle.gameObject.SetActive(true);
        }

        public void Animate(Action callback)
        {
            if (_isInAnimation) return;
            _isInAnimation = true;
            
            _selectionCircle
                .DOFade(0f, _fadeDuration / 2)
                .SetLink(gameObject)
                .OnComplete(() => Destroy(_selectionCircle.gameObject))
                .Play();
            _spriteRender
                .DOFade(1f, _fadeDuration)
                .SetLink(gameObject)
                .OnComplete(() =>
                {
                    callback?.Invoke();
                })
                .Play();
        }
    }
}
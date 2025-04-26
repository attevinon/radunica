using System;
using DG.Tweening;
using UnityEngine;

namespace Scripts.Animations
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class FadeDuringInput : MonoBehaviour, IAnimatable
    {
        private SpriteRenderer _spriteRender;
        private float _startAlpha;
        private float _fadeDuration = 0.3f;

        private void Awake()
        {
            _spriteRender = GetComponent<SpriteRenderer>();
            _startAlpha = _spriteRender.color.a;
        }

        public void Fade(float progress)
        {
            Color color = _spriteRender.color;
            float alpha = Mathf.Lerp(_startAlpha, 0f, progress);
            color.a = alpha;
            _spriteRender.color = color;
        }

        public void Animate(Action callback)
        {
            _spriteRender
                .DOFade(0f, _fadeDuration)
                .SetLink(gameObject)
                .OnComplete(() =>
                {
                    callback?.Invoke();
                })
                .Play();
        }
    }
}
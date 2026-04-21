using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Scripts.Animations
{
    public class ChangeSpriteAnimation : ScaleAnimation, IAnimatableDuringInput
    {
        [FormerlySerializedAs("_spriteRenderer")] [SerializeField] private SpriteRenderer _drySpriteRenderer; 
        [SerializeField] private SpriteRenderer _wetSpriteRenderer;
        private bool _isInAnimation;
        private bool _spriteChanged;

        private void Awake()
        {
            Color color = _wetSpriteRenderer.color;
            color.a = 0f;
            _wetSpriteRenderer.color = color;
        }

        public void AnimateDuringInput(float progress)
        {
            ChangeAlpha(_wetSpriteRenderer, progress, 1f);
        }

        public override void Animate(Action callback)
        {
            Color color = _drySpriteRenderer.color;
            color.a = 1f;
            _drySpriteRenderer.color = color;
            base.Animate(callback);
        }
        
        private void ChangeAlpha(SpriteRenderer spriteRenderer, float progress, float targetAlpha)
        {
            float startAlpha = spriteRenderer.color.a;
            Color color = spriteRenderer.color;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, progress);
            color.a = alpha;
            spriteRenderer.color = color;
        }
    }
}
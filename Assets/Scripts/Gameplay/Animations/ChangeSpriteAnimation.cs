using System;
using UnityEngine;

namespace Scripts.Animations
{
    public class ChangeSpriteAnimation : ScaleAnimation, IAnimatableDuringInput
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Sprite _newSprite;
        private bool _isInAnimation;
        private bool _spriteChanged;

        public void AnimateDuringInput(float progress)
        {
            if (progress < 0.5f)
            {
                ChangeAlpha(progress / 0.5f, 0.5f);
            }
            else
            {
                if (!_spriteChanged)
                {
                    _spriteChanged = true;
                    _spriteRenderer.sprite = _newSprite;
                }
                
                ChangeAlpha((progress - 0.5f) / 0.5f, 1f);
            }
        }

        public override void Animate(Action callback)
        {
            Color color = _spriteRenderer.color;
            color.a = 1f;
            _spriteRenderer.color = color;
            base.Animate(callback);
        }
        
        public void ChangeAlpha(float progress, float targetAlpha)
        {
            float startAlpha = targetAlpha > 0.98f ? 0.5f : 1f; 
            Color color = _spriteRenderer.color;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, progress);
            color.a = alpha;
            _spriteRenderer.color = color;
        }
    }
}
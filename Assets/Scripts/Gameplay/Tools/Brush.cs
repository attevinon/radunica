using System;
using UnityEngine;
using DG.Tweening;

namespace Scripts
{
    public class Brush : Rag, IDryable
    {
        private const float FADE_DURATION = 0.4f;
        public event Action Dried;
        
        public void OnDriedOut()
        {
            _isWet = false;
            _spriteRender.color = _dryColor;
            _spriteRender
                .DOFade(0f, FADE_DURATION)
                .SetLink(gameObject)
                .Play();
            _basin.Activate();
            Dried?.Invoke();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            Dried = null;
        }
    }

    public interface IDryable
    {
        public event Action Dried;
        public void OnDriedOut();
    }
}
using System;
using UnityEngine;

namespace Scripts
{
    public class Brush : Rag, IDryable
    {
        public event Action Dried;
        
        public void OnDriedOut()
        {
            _isWet = false;
            _spriteRender.color = _dryColor;
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
﻿using System;
using DG.Tweening;
using UnityEngine;

namespace Scripts.Animations
{
    public class ScaleAnimation : MonoBehaviour, IAnimatable
    {
        [SerializeField] private float _scaled = 1.1f;
        [SerializeField] private float _scaleDuration = 1f;
        private bool _isInAnimation;

        public virtual void Animate(Action callback)
        {
            if(_isInAnimation) return;
            _isInAnimation = true;
            transform
                .DOScale(transform.localScale * _scaled, _scaleDuration)
                .SetLink(gameObject)
                .SetLoops(2, LoopType.Yoyo)
                .OnComplete(() => callback?.Invoke())
                .Play();
        }
    }
}
using System;
using DG.Tweening;
using Scripts.Input;
using UnityEngine;

namespace Scripts
{
    public class Rag : Tool, IWettable
    {
        public event Action GotWet;
        
        [SerializeField] protected Basin _basin;
        [SerializeField] protected SpriteRenderer _spriteRender;
        [SerializeField] protected Color _dryColor;
        [SerializeField] protected Color _wetColor;
        [SerializeField] private float _recolorDuration = 0.3f;

        protected bool _isWet;

        protected override void OnMouseDown()
        {
            if(_isInHand) return;
            PlaceInHand();
            _basin.Activate();
        }

        private void OnEnable()
        {
            _isWet = false;
            _spriteRender.color = _dryColor;
            _basin.OnWaterClicked += SetWet;
            _basin.Show();
        }

        protected virtual void OnDisable()
        {
            _basin.OnWaterClicked -= SetWet;
            _basin.Hide();
        }

        private void SetWet()
        {
            if(_isWet) return;
            _isWet = true;
            _spriteRender
                .DOColor(_wetColor, _recolorDuration)
                .SetLink(gameObject)
                .Play();
            _onActivated?.Invoke();
        }
    }

    public interface IWettable
    {
        public event Action GotWet;
    }
}
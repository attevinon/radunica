using System;
using DG.Tweening;
using Scripts.Input;
using UnityEngine;

namespace Scripts
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Rag : Tool
    {
        [SerializeField] private Basin _basin;
        [SerializeField] private Color _dryColor;
        [SerializeField] private Color _wetColor;
        [SerializeField] private float _recolorDuration = 0.3f;

        private SpriteRenderer _spriteRender;
        private bool _isWet;

        protected override void Awake()
        {
            base.Awake();
            _spriteRender = GetComponent<SpriteRenderer>();
        }

        protected override void OnMouseDown()
        {
            if(_isInHand) return;
            PlaceInHand();
        }

        private void OnEnable()
        {
            _spriteRender.color = _dryColor;
            _basin.OnWaterClicked += SetWet;
            _basin.Show();
        }

        private void OnDisable()
        {
            _basin.OnWaterClicked += SetWet;
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
}
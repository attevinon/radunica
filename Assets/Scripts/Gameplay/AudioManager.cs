using System;
using DG.Tweening;
using UnityEngine;

namespace Scripts
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private float _fadeDuration;
        [SerializeField] private float _gameplayFadeDuration;
        [SerializeField] private AudioClip _endTheme;
        private AudioSource _audioSource;
        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _audioSource.volume = 0f;
        }

        public void PlayMainTheme()
        {
            _audioSource.PlayDelayed(2f);
            _audioSource
                .DOFade(1f, _gameplayFadeDuration)
                .SetEase(Ease.OutSine)
                .SetDelay(2f)
                .SetLink(gameObject)
                .Play();
        }

        public void PlayEndTheme()
        {
            _audioSource
                .DOFade(0f, _fadeDuration)
                .SetLink(gameObject)
                .OnComplete(() =>
                {
                    _audioSource.Stop();
                    _audioSource.loop = false;
                    _audioSource.clip = _endTheme;
                    _audioSource.volume = 1f;
                    _audioSource.PlayDelayed(1f);
                })
                .Play();

        }
    }
}
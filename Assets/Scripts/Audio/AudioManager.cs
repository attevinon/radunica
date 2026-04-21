using System.Linq;
using DG.Tweening;
using Scripts.Data;
using UnityEngine;

namespace Scripts.Audio
{
    public class AudioManager : MonoBehaviour, ISoundsPlayer
    {
        [SerializeField] private float _fadeDuration;
        [SerializeField] private float _gameplayFadeDuration;
        [SerializeField] private AudioClip _endTheme;
        [SerializeField] private AudioSource _audioSourceMusic;
        [SerializeField] private AudioSource _audioSourceOneShot;
        [SerializeField] private SoundsConfig _soundConfig;
        public static ISoundsPlayer Instance { get; private set; }
        private void Awake()
        {
            Instance = this;
            _audioSourceMusic.volume = 0f;
        }

        public void PlayMainTheme()
        {
            _audioSourceMusic.PlayDelayed(2f);
            _audioSourceMusic
                .DOFade(1f, _gameplayFadeDuration)
                .SetEase(Ease.OutSine)
                .SetDelay(2f)
                .SetLink(gameObject)
                .Play();
        }

        public void PlayEndTheme()
        {
            _audioSourceMusic
                .DOFade(0f, _fadeDuration)
                .SetLink(gameObject)
                .OnComplete(() =>
                {
                    _audioSourceMusic.Stop();
                    _audioSourceMusic.loop = false;
                    _audioSourceMusic.clip = _endTheme;
                    _audioSourceMusic.volume = 1f;
                    _audioSourceMusic.PlayDelayed(0.4f);
                })
                .Play();

        }

        public void PlaySoundEffect(SoundEffectType soundEffectType)
        {
            var soundEffect = _soundConfig.SoundEffects.FirstOrDefault(x => x.Type == soundEffectType);
            if(soundEffect == null)
                return;
            
            _audioSourceOneShot.PlayOneShot(soundEffect.Clip, soundEffect.Volume);
        }
    }
}
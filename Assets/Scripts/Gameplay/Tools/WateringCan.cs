using UnityEngine;

namespace Scripts
{
    public class WateringCan : Tool
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private ParticleSystem _particleSystem;

        protected override void HandleInteractionStart()
        {
            _audioSource.Play();
            _particleSystem.Play();
        }
        
        protected override void HandleInteractionEnd()
        {
            _audioSource.Stop();
            _audioSource.time = 0;
            _particleSystem.Stop();
        }
    }
}
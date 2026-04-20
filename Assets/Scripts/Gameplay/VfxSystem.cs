using System;
using UnityEngine;

namespace Scripts
{
    public class VfxSystem : MonoBehaviour
    {
        [SerializeField] private GameObject _shineParticlesPrefab;
        
        public static VfxSystem Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        public void PlayShineParticles(Vector3 position)
        {
            Instantiate(_shineParticlesPrefab, position, Quaternion.identity);
        }
    }
}
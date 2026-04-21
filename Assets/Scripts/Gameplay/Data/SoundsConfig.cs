using System;
using UnityEngine;

namespace Scripts.Data
{  
    [CreateAssetMenu(fileName = "SoundsConfig", menuName = "Radunica/SoundsConfig")]
    public class SoundsConfig : ScriptableObject
    {
        public SoundEffect[] SoundEffects;
    }
    
    [Serializable]
    public class SoundEffect
    {
        public SoundEffectType Type;
        public AudioClip Clip;
        public float Volume;
    }

    public enum SoundEffectType
    {
        STEP_COMPLETED,
    }
}


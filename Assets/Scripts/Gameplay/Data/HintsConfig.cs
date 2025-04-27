using System;
using UnityEngine;

namespace Scripts.Data
{
    [CreateAssetMenu(fileName = "HintsConfig", menuName = "Radunica/HintsConfig")]
    public class HintsConfig : ScriptableObject
    {
        public Hint[] Hints;
    }

    [Serializable]
    public class Hint
    {
        public ItemTag Tag;
        public string HintText;
    }
}
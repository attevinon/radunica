using UnityEngine;

namespace Scripts.Data
{
    [CreateAssetMenu(fileName = "StepsConfig", menuName = "Radunica/StepsConfig")]
    public class StepsConfig : ScriptableObject
    {
        [SerializeField] private Step[] _steps;
        public Step[] Steps => _steps;
    }
}


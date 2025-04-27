using UnityEngine;

namespace Scripts.Data
{
    [CreateAssetMenu(fileName = "CutsceneConfig", menuName = "Radunica/CutsceneConfig")]
    public class CutsceneConfig : ScriptableObject
    {
        [SerializeField] private string[] _lines;
        public string[] Lines => _lines;
    }
}
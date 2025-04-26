using System;
using UnityEngine;

namespace Scripts.Data
{
    [Serializable]
    public class SurfaceToClean
    {
        [SerializeField] private string _surfaceTag;
        [SerializeField] private SceneName _sceneName;
        [SerializeField] private Step[] _steps;

        public string SurfaceTag => _surfaceTag;
        public SceneName SceneName => _sceneName;
        public Step[] Steps => _steps;
    }
}
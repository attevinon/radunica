using System;
using UnityEngine;

namespace Scripts.Data
{
    [Serializable]
    public class Step
    {
        [SerializeField] private ToolType _tool;
        [SerializeField] private ItemTag _targetItemTag;
        [SerializeField] private ItemLayer _layer;
        [SerializeField] private SceneName _sceneName;
        
        public ToolType Tool => _tool;
        public ItemTag TargetItemTag => _targetItemTag;
        public ItemLayer Layer => _layer;
        public SceneName Scene => _sceneName;
    }
}
using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Scripts.Data
{
    [Serializable]
    public class Step
    {
        [SerializeField] private ToolType _tool;
        [SerializeField] private ItemTag _targetItemTag; 
        [SerializeField] private SurfaceType _surfaceType;
        
        public ToolType Tool => _tool;
        public ItemTag TargetItemTag => _targetItemTag;
        public  SurfaceType SurfaceType => _surfaceType;
    }
}
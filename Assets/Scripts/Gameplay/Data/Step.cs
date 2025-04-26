using System;
using UnityEngine;

namespace Scripts.Data
{
    [Serializable]
    public class Step
    {
        //[SerializeField] private Tool _tool;
        [SerializeField] private TargetItem _targetItem;
        [SerializeField] private ItemTag _targetItemTag;
        [SerializeField] private ItemLayer _layer;
        
        //public Tool Tool => _tool;
        public TargetItem TargetItem => _targetItem;
        public ItemTag TargetItemTag => _targetItemTag;
        public ItemLayer Layer => _layer;
    }
}
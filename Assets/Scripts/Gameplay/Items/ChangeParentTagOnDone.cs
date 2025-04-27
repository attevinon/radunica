using System;
using UnityEngine;

namespace Scripts
{
    [RequireComponent(typeof(TargetItem))]
    public class ChangeParentTagOnDone : MonoBehaviour
    {
        [SerializeField] private ItemTag _newTag;
        private TargetItem _targetItem;

        private void Awake()
        {
            _targetItem = GetComponent<TargetItem>();
        }

        private void OnEnable()
        {
            _targetItem.OnDone += ChangeTag;
        }

        private void OnDisable()
        {
            _targetItem.OnDone -= ChangeTag;
        }

        private void ChangeTag(TargetItem item)
        {
            if(transform.parent.parent.TryGetComponent(out ItemsParent parent))
                parent.ChangeTag(_newTag);

            transform.parent.position = transform.position;
            transform.localPosition = Vector3.zero;
        }
    }
}
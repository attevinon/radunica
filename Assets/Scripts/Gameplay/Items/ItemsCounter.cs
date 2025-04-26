using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class ItemsCounter : MonoBehaviour
    {
        public static event Action GoalAchieved;
        public Action OnGoalAchieved;
        public event Action ItemDone;
        private TargetItem[] _targetItems;
        private int _itemsCounter;

        public void SetTarget(ItemTag itemTag, SurfaceType surfaceType)
        {
            _targetItems = GetItems(itemTag, surfaceType);
            _itemsCounter = _targetItems.Length;

            foreach (var item in _targetItems)
            {
                item.OnDone += OnItemDone;
                item.EnableCollider(true);
            }
            
            Debug.Log("Target Set, Count = " + _itemsCounter);
        }
        
        private TargetItem[] GetItems(ItemTag itemTag, SurfaceType surfaceType)
        {
            List<TargetItem> items = new List<TargetItem>();
            ItemsParent[] parents = FindObjectsByType<ItemsParent>(FindObjectsSortMode.None);

            foreach (var parent in parents)
            {
                if (parent.SurfaceType != surfaceType || parent.Tag != itemTag)
                    continue;
                
                foreach (Transform child in parent.transform)
                {
                    if (child.gameObject.TryGetComponent(out TargetItem item))
                        items.Add(item);
                }
            }

            return items.ToArray();
        }

        private void OnItemDone(TargetItem item)
        {
            item.OnDone -= OnItemDone;
            item.Destroy();
            _itemsCounter--;
            ItemDone?.Invoke();
            if (_itemsCounter == 0)
            {
                ItemDone = null;
                GoalAchieved?.Invoke();
                OnGoalAchieved?.Invoke();
            }
        }

        public void ActivateItems()
        {
            foreach (var item in _targetItems)
            {
                if(item == null || !item.isActiveAndEnabled) continue;
                item.EnableCollider(true);
            }
        }

        public void DeactivateItems()
        {
            foreach (var item in _targetItems)
            {
                if(item == null || !item.isActiveAndEnabled) continue;
                item.EnableCollider(false);
            }
        }
    }
}
using System;
using System.Collections;
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
            StartCoroutine(SetTargetCoroutine(itemTag, surfaceType));
        }

        private IEnumerator SetTargetCoroutine(ItemTag itemTag, SurfaceType surfaceType)
        {
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();

            _targetItems = GetItems(itemTag, surfaceType);
            _itemsCounter = _targetItems.Length;
            foreach (var item in _targetItems)
            {
                item.OnDone += OnItemDone;
                item.EnableCollider(true);
            }
            
            Debug.Log("Target = " + itemTag + " On " + surfaceType + ", Count = " + _itemsCounter);
        }
        
        private TargetItem[] GetItems(ItemTag itemTag, SurfaceType surfaceType)
        {
            List<TargetItem> items = new List<TargetItem>();
            ItemsParent[] parents = FindObjectsByType<ItemsParent>(FindObjectsInactive.Include, FindObjectsSortMode.None);

            foreach (var parent in parents)
            {
                if (parent.SurfaceType != surfaceType || parent.Tag != itemTag)
                    continue;
                
                foreach (Transform child in parent.transform)
                {
                    if (child.gameObject.TryGetComponent(out TargetItem item)
                        && item.ItemTag == itemTag)
                        items.Add(item);

                    if (itemTag != ItemTag.FlowerToPlace
                        && itemTag != ItemTag.FlowerToWater) continue;
                    
                    foreach (Transform childChild in child)
                    {
                        if (childChild.gameObject.TryGetComponent(out TargetItem childItem)
                            && childItem.ItemTag == itemTag)
                            items.Add(childItem);
                    }
                }
            }

            return items.ToArray();
        }

        private void OnItemDone(TargetItem item)
        {
            item.OnDone -= OnItemDone;
            if(item.gameObject.TryGetComponent(out DestroyableItem destroyable))
                destroyable.Destroy();
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
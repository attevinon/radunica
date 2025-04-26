using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class ItemsCounter : MonoBehaviour
    {
        public static event Action GoalAchieved;
        private TargetItem[] _targetItems;
        private int _itemsCounter;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void SetTarget(ItemTag itemTag, ItemLayer itemLayer)
        {
            _targetItems = GetItems(itemTag, itemLayer);
            _itemsCounter = _targetItems.Length;

            foreach (var item in _targetItems)
            {
                item.OnDone += OnItemDone;
                item.EnableCollider(true);
            }
            
            Debug.Log("Target Set, Count = " + _itemsCounter);
        }
        
        private TargetItem[] GetItems(ItemTag itemTag, ItemLayer itemLayer)
        {
            List<TargetItem> items = new List<TargetItem>();
            ItemsParent[] parents = FindObjectsByType<ItemsParent>(FindObjectsSortMode.None);

            foreach (var parent in parents)
            {
                if (parent.Layer != itemLayer || parent.Tag != itemTag)
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
            if(_itemsCounter == 0)
                GoalAchieved?.Invoke();
        }
    }
}
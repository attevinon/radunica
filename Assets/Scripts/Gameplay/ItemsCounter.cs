using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class ItemsCounter : MonoBehaviour
    {
        public static event Action GoalAchieved;
    
        private TargetItem[] _allItems;
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

        //todo make class ItemsRegistry for each Scene and add all items in Lists by Tag
        private TargetItem[] GetItems(ItemTag itemTag, ItemLayer itemLayer)
        {
            List<TargetItem> items = new List<TargetItem>();
            _allItems = FindObjectsOfType<TargetItem>();
            foreach (var item in _allItems)
            {
                if(item.Layer != itemLayer)
                    continue;
                
                if(item.CompareTag(itemTag.ToString()))
                    items.Add(item);
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
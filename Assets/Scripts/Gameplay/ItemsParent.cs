using UnityEngine;

namespace Scripts
{
    public class ItemsParent : MonoBehaviour
    {
        [SerializeField] private ItemTag _tag;
        [SerializeField] private ItemLayer _layer;

        public ItemTag Tag => _tag;
        public ItemLayer Layer => _layer;
    }
}
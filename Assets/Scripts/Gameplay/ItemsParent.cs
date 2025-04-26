using UnityEngine;
using UnityEngine.Serialization;

namespace Scripts
{
    public class ItemsParent : MonoBehaviour
    {
        [SerializeField] private ItemTag _tag; 
        [SerializeField] private SurfaceType _surfaceType;

        public ItemTag Tag => _tag;
        public SurfaceType SurfaceType => _surfaceType;
    }
}
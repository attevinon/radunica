using UnityEngine;

namespace Scripts
{
    public class DestroyableItem : MonoBehaviour
    {
        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}
using UnityEngine;

namespace Scripts.Data
{
    [CreateAssetMenu(fileName = "SurfacesConfig", menuName = "Radunica/SurfacesConfig")]
    public class SurfacesConfig : ScriptableObject
    {
        [SerializeField] private SurfaceToClean[] _surfaces;
        public SurfaceToClean[] Surfaces => _surfaces;
    }
}
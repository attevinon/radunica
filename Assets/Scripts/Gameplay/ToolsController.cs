using System;
using UnityEngine;

namespace Scripts
{
    public class ToolsController : MonoBehaviour
    {
        [SerializeField] private Tool _scrissors;
        [SerializeField] private Tool _rag;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        public Tool GetTool(ToolType toolType)
        {
            switch (toolType)
            {
                case ToolType.Scrissors:
                    return _scrissors;
                default:
                    return null;
            }
        }
    }
}
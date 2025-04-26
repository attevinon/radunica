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
                case ToolType.Rag:
                    return _rag;
                default:
                    return null;
            }
        }

        public void TryHideTool(ToolType toolType)
        {
            Tool tool = GetTool(toolType);
            if (tool != null)
                tool.Hide();
        }
    }
}
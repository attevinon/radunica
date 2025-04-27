using System;
using UnityEngine;

namespace Scripts
{
    public class ToolsController : MonoBehaviour
    {
        [SerializeField] private Tool _scrissors;
        [SerializeField] private Rag _rag;
        [SerializeField] private Brush _brush;
        [SerializeField] private Tool _shovel;

        public Tool GetTool(ToolType toolType)
        {
            switch (toolType)
            {
                case ToolType.Scrissors:
                    return _scrissors;
                case ToolType.Rag:
                    return _rag;
                case ToolType.Brush:
                    return _brush;
                case ToolType.Shovel:
                    return _shovel;
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
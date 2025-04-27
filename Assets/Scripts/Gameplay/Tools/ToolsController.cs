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
        [SerializeField] private Tool _wateringCan;
 
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
                case ToolType.WateringCan:
                    return _wateringCan;
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
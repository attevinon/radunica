﻿using System;
using Scripts.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts
{
    public class StepsController
    {
        public event Action<ItemTag> ItemSetted;
        private ItemsCounter _itemsCounter;
        private ToolsController _toolsController;
        private Step[] _steps;
        private Step _currentStep;
        private int _stepIndex;

        public void Initialize(
            ItemsCounter itemsCounter,
            ToolsController toolsController)
        {
            _itemsCounter = itemsCounter;
            _toolsController = toolsController;
        }

        public void SetSteps(Step[] steps)
        {
            _steps = steps;
            _stepIndex = 0;
            SetStep();
        }
        
        public bool TryNextStep()
        {
            _stepIndex++;
            
            if (_stepIndex == _steps.Length)
            {
                //AllStepsDone.Invoke();
                return false;
            }
            
            SetStep();
            return true;
        }

        private void SetStep()
        {
            _currentStep = _steps[_stepIndex];

            if (_currentStep.Tool != ToolType.None)
            {
                var tool = _toolsController.GetTool(_currentStep.Tool);
                tool.Show(SetTargetItems);
                ItemSetted?.Invoke(_currentStep.TargetItemTag);
                if (tool is IDryable dryable)
                {
                    dryable.Dried += _itemsCounter.DeactivateItems;
                    _itemsCounter.ItemDone += dryable.OnDriedOut;
                }
                _itemsCounter.OnGoalAchieved += HideTool;
            }
            else
            {
                SetTargetItems();
                ItemSetted?.Invoke(_currentStep.TargetItemTag);
            }
        }

        private void SetTargetItems()
        {
            _itemsCounter.SetTarget(_currentStep.TargetItemTag, _currentStep.SurfaceType);
        }

        private void HideTool()
        {
            _itemsCounter.OnGoalAchieved -= HideTool;
            if (_currentStep != null)
                _toolsController.TryHideTool(_currentStep.Tool);
        }
    }
}
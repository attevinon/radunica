using Scripts.Data;
using UnityEngine;

namespace Scripts
{
    public class StepsController
    {
        private ItemsCounter _itemsCounter;
        private StepsConfig _stepsConfig;
        private int _stepIndex;
        private Step _currentStep;

        private static StepsController _instance;
        public static StepsController I
        {
            get
            {
                if (_instance == null)
                    _instance = new StepsController();
                return _instance;
            }
        }

        public void Initialize(ItemsCounter itemsCounter, StepsConfig stepsConfig)
        {
            _itemsCounter = itemsCounter;
            _stepsConfig = stepsConfig;
        }

        public void Start()
        {
            _stepIndex = 0;
            SetStep();
        }
        
        public void NextStep()
        {
            _stepIndex++;
            
            if (_stepIndex == _stepsConfig.Steps.Length)
            {
                EndGame();
                return;
            }
            
            SetStep();
        }

        private void EndGame()
        {
            Debug.Log("Good Job!");
        }

        private void SetStep()
        {
            _currentStep = _stepsConfig.Steps[_stepIndex];
            _itemsCounter.SetTarget(_currentStep.TargetItemTag, _currentStep.Layer);
        }
    }
}
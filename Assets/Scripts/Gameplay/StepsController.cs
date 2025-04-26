using Scripts.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts
{
    public class StepsController
    {
        private ItemsCounter _itemsCounter;
        private StepsConfig _stepsConfig;
        private ToolsController _toolsController;
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

        public void Initialize(
            StepsConfig stepsConfig,
            ItemsCounter itemsCounter,
            ToolsController toolsController)
        {
            _itemsCounter = itemsCounter;
            _stepsConfig = stepsConfig;
            _toolsController = toolsController;
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
            var currentScene = SceneManager.GetActiveScene();
            if (_currentStep.Scene.ToString() != currentScene.name)
            {
                //todo UI to hide transition
                SceneManager.UnloadSceneAsync(currentScene);
                SceneManager.LoadScene(_currentStep.Scene.ToString(), LoadSceneMode.Additive);
            }

            if (_currentStep.Tool != ToolType.None)
            {
                var tool = _toolsController.GetTool(_currentStep.Tool);
                tool.Initialize(() =>
                    _itemsCounter.SetTarget(_currentStep.TargetItemTag, _currentStep.Layer));
            }
            else
            {
                _itemsCounter.SetTarget(_currentStep.TargetItemTag, _currentStep.Layer);
            }
        }
    }
}
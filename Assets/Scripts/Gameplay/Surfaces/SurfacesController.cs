using Scripts.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts
{
    public class SurfacesController
    {
        private SurfacesConfig _surfacesConfig;
        private StepsController _stepsController;
        private SurfaceToClean _currentSurface;
        private int _surfaceIndex;
        
        private static SurfacesController _instance;
        public static SurfacesController I
        {
            get
            {
                if (_instance == null)
                    _instance = new SurfacesController();
                return _instance;
            }
        }

        private SurfacesController()
        {
            _stepsController = new StepsController();
        }

        public void Initialize(
            SurfacesConfig surfacesConfig,
            ItemsCounter itemsCounter,
            ToolsController toolsController)
        {
            _surfacesConfig = surfacesConfig;
            _stepsController.Initialize(itemsCounter, toolsController);
        }

        public void Start()
        {
            _surfaceIndex = 0;
            SetSurface();
        }

        public void NextStep()
        {
            bool result = _stepsController.TryNextStep();
            if (!result)
            {
                NextSurface();
            }
        }

        private void NextSurface()
        {
            _surfaceIndex++;
            if (_surfaceIndex == _surfacesConfig.Surfaces.Length)
            {
                EndGame();
                return;
            }
            
            SetSurface();
        }

        private void SetSurface()
        {
            _currentSurface = _surfacesConfig.Surfaces[_surfaceIndex];
            ChangeScene();
            _stepsController.SetSteps(_currentSurface.Steps);
        }

        private void ChangeScene()
        {
            var currentScene = SceneManager.GetActiveScene();
            if (_currentSurface.SceneName.ToString() != currentScene.name)
            {
                //todo UI to hide transition
                SceneManager.UnloadSceneAsync(currentScene);
                SceneManager.LoadScene(_currentSurface.SceneName.ToString(), LoadSceneMode.Additive);
            }
        }
        
        private void EndGame()
        {
            Debug.Log("Good Job!");
        }
    }
}
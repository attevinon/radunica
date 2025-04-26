using System;
using System.Linq;
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

        private Surface[] _surfaces;
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
            Surface[] surfaces,
            ItemsCounter itemsCounter,
            ToolsController toolsController)
        {
            _surfacesConfig = surfacesConfig;
            _surfaces = surfaces;
            _stepsController.Initialize(itemsCounter, toolsController);
        }

        public void Start()
        {
            _surfaceIndex = 0;
            SetSurface();
            Debug.Log("SetSurface();");
        }

        public void NextStep()
        {
            bool result = _stepsController.TryNextStep();
            if (!result)
            {
                NextSurface();
                Debug.Log("NextSurface();");
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
            Debug.Log("SetSurface();");
        }

        private void SetSurface()
        {
            _currentSurface = _surfacesConfig.Surfaces[_surfaceIndex];
            
            if (_currentSurface.SceneName != SceneName.Game
                && _surfacesConfig.Surfaces[_surfaceIndex - 1].SceneName != SceneName.Game)
            {
                UnloadPrevScene(() =>
                {
                    CleanPrevSurface();
                    //remove screensaver
                    TrySetSurfaceSelection();
                });
                return;
            }
            
            bool result =  TrySetSurfaceSelection();
            if(result) return;
            
            _stepsController.SetSteps(_currentSurface.Steps);
        }

        private bool TrySetSurfaceSelection()
        {
            foreach (var surface in _surfaces)
            {
                if (surface.SurfaceType != _currentSurface.SurfaceType) continue;
                
                surface.WaitForSelection(() =>
                {
                    ChangeScene();
                    _stepsController.SetSteps(_currentSurface.Steps);
                });
                return true;
            }
            return false;
        }

        private void ChangeScene()
        {
            var currentScene = SceneManager.GetActiveScene();
            if (_currentSurface.SceneName.ToString() != currentScene.name)
            {
                //todo UI to hide transition
                if (currentScene.name != SceneName.Game.ToString())
                {
                    SceneManager.UnloadSceneAsync(currentScene);
                }
                SceneManager.LoadScene(_currentSurface.SceneName.ToString(), LoadSceneMode.Additive);
                if (_currentSurface.SceneName == SceneName.Game)
                    CleanPrevSurface();

                //hide black screen
            }
        }
        
        private void UnloadPrevScene(Action onSceneUnloaded)
        {
            var scene = SceneManager.GetSceneByName(_surfacesConfig.Surfaces[_surfaceIndex - 1].SceneName.ToString());
            SceneManager.UnloadSceneAsync(scene);
            onSceneUnloaded?.Invoke();
        }

        private void CleanPrevSurface()
        {
            Surface prevSurface = _surfaces
                .FirstOrDefault(x =>
                    x.SurfaceType == _surfacesConfig.Surfaces[_surfaceIndex - 1].SurfaceType);
            if(prevSurface != null)
                prevSurface.OnCleaned();
        }
        
        private void EndGame()
        {
            Debug.Log("Good Job!");
        }
    }
}
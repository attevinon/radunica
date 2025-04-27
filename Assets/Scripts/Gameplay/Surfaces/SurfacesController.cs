using System;
using System.Linq;
using Scripts.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts
{
    public class SurfacesController
    {
        public event Action AllDone;
        public event Action<ItemTag> TargetItemSet;
        public Action HideHint;
        
        private readonly StepsController _stepsController;
        private SurfacesConfig _surfacesConfig;
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
            _stepsController.ItemSetted += ItemSetted;
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
            Debug.Log("NextSurface();");
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
            Debug.Log("SetSurface();");
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
                
                HideHint?.Invoke();
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
            HideHint?.Invoke();
            //todo UI to hide transition
            SceneManager.LoadScene(_currentSurface.SceneName.ToString(), LoadSceneMode.Additive);
            Debug.Log("SceneLoaded");
            if (_currentSurface.SceneName == SceneName.Game)
                CleanPrevSurface();
            //hide black screen
        }
        
        private void UnloadPrevScene(Action onSceneUnloaded)
        {
            HideHint?.Invoke();
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

        private void ItemSetted(ItemTag tag)
        {
            TargetItemSet?.Invoke(tag);
        }
        
        private void EndGame()
        {
            _stepsController.ItemSetted -= ItemSetted;
            UnloadPrevScene( () =>
            {
                CleanPrevSurface();
                Debug.Log("Good Job!");
                AllDone?.Invoke();
            });
        }
    }
}
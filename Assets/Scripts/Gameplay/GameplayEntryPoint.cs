using System;
using System.Collections.Generic;
using Scripts.Data;
using Scripts.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts
{
    public class GameplayEntryPoint : MonoBehaviour
    {
        [SerializeField] private Surface[] _surfaces;
        [SerializeField] private SurfacesConfig _surfacesConfig;
        [SerializeField] private ItemsCounter _itemsCounter;
        [SerializeField] private ToolsController _toolsController;
        [SerializeField] private AudioManager _audioManager;

        private UIController _uiController;

        private void Awake()
        {
            if(IsExists())
            {
                DestroyImmediate(gameObject);
                return;
            }
            
            DontDestroyOnLoad(this);
            SurfacesController.I.Initialize(_surfacesConfig, _surfaces, _itemsCounter, _toolsController);
            SceneManager.LoadScene("HUD", LoadSceneMode.Additive);
        }
        
        private bool IsExists()
        {
            var objects = FindObjectsOfType<GameplayEntryPoint>();
            foreach (var obj in objects)
            {
                if (obj != this)
                    return true;
            }
            return false;
        }

        private void Start()
        {
            _uiController = FindObjectOfType<UIController>();
            _uiController.EndGameButtonClicked += EndThis;
            _uiController.Initialize();
            _uiController.CutsceneController.EndStarted += _audioManager.PlayEndTheme;
            _uiController.CutsceneController.ShowStartCutscene(_audioManager.PlayMainTheme);
            
        }

        private void OnDisable()
        {
            _uiController.EndGameButtonClicked -= EndThis;
            _uiController.CutsceneController.EndStarted -= _audioManager.PlayEndTheme;
        }

        private void EndThis()
        {
            SceneManager.LoadScene("MainMenu");
            Destroy(_toolsController.gameObject);
            Destroy(gameObject);
        }
    }
}
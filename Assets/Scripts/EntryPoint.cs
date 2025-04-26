using System;
using Scripts;
using Scripts.Data;
using Scripts.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using SurfacesController = Scripts.SurfacesController;

namespace DefaultNamespace
{
    public class EntryPoint : MonoBehaviour
    { 
        [SerializeField] private SurfacesConfig _surfacesConfig;
        [SerializeField] private ItemsCounter _itemsCounter;
        [SerializeField] private ToolsController _toolsController;
        
        private UIController _uiController;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            SurfacesController.I.Initialize(_surfacesConfig, _itemsCounter, _toolsController);
            SceneManager.LoadScene("HUD", LoadSceneMode.Additive);
        }
    }
}
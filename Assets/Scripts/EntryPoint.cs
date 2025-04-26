using System;
using Scripts;
using Scripts.Data;
using Scripts.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class EntryPoint : MonoBehaviour
    { 
        [SerializeField] private StepsConfig _stepsConfig;
        [SerializeField] private ItemsCounter _itemsCounter;
        [SerializeField] private ToolsController _toolsController;
        
        private UIController _uiController;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            StepsController.I.Initialize(_stepsConfig, _itemsCounter, _toolsController);
            SceneManager.LoadScene("HUD", LoadSceneMode.Additive);
        }
    }
}
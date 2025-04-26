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
        
        private UIController _uiController;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            StepsController.I.Initialize(_itemsCounter, _stepsConfig);
            SceneManager.LoadScene("HUD", LoadSceneMode.Additive);
        }
    }
}
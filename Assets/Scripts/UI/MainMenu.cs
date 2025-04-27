using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scripts.UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button _startButton;

        private void OnEnable()
        {
            _startButton.onClick.AddListener(StartGame);
        }

        private void OnDisable()
        {
            _startButton.onClick.RemoveListener(StartGame);
        }

        private void StartGame()
        {
            SceneManager.LoadScene(SceneName.Game.ToString());
        }
    }
}
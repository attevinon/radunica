using System;
using Scripts.Data;
using UnityEngine;

namespace Scripts.UI
{
    public class UIController : MonoBehaviour
    {
        public event Action EndGameButtonClicked;

        [SerializeField] private CutsceneController _cutsceneController;
        [SerializeField] private MyButton _startButton;
        [SerializeField] private MyButton _nextStepButton;
        [SerializeField] private MyButton _doneButton;
        [SerializeField] private MyButton _endGameButton;
        [SerializeField] private CanvasGroup _screensaver;

        public CutsceneController CutsceneController => _cutsceneController;

        public void Initialize()
        {
            _startButton.OnClicked += SurfacesController.I.Start;
            _nextStepButton.OnClicked += SurfacesController.I.NextStep;
            ItemsCounter.GoalAchieved += _nextStepButton.Show;
            SurfacesController.I.AllDone += _doneButton.Show;
            _doneButton.OnClicked += _cutsceneController.ShowEndCutscene;
            _cutsceneController.StartShown += ShowStartButton;
            _cutsceneController.EndShown += ShowEndGameButton;
            _endGameButton.OnClicked += EndGameButtonClicked;
        }

        private void OnDestroy()
        {
            _startButton.OnClicked -= SurfacesController.I.Start;
            _nextStepButton.OnClicked -= SurfacesController.I.NextStep;
            ItemsCounter.GoalAchieved -= _nextStepButton.Show;
            SurfacesController.I.AllDone -= _doneButton.Show;
            _doneButton.OnClicked -= _cutsceneController.ShowEndCutscene;
            _cutsceneController.StartShown -= ShowStartButton;
            _cutsceneController.EndShown -= ShowEndGameButton;
            _endGameButton.OnClicked -= EndGameButtonClicked;
        }
        public void EnableScreenSaver(bool enable)
        {
            
        }

        public void ShowStartButton() => _startButton.Show();
        public void ShowEndGameButton() => _endGameButton.Show();
    }
}
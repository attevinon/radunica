using System;
using Scripts.Data;
using UnityEngine;

namespace Scripts.UI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private NextStepButton _nextStepButton;
        [SerializeField] private StartButton _startButton;
        [SerializeField] private CanvasGroup _screensaver;

        private void OnEnable()
        {
            _startButton.OnClicked += SurfacesController.I.Start;
            _nextStepButton.OnClicked += SurfacesController.I.NextStep;
            ItemsCounter.GoalAchieved += _nextStepButton.Show;
        }

        private void OnDisable()
        {
            _startButton.OnClicked -= SurfacesController.I.Start;
            _nextStepButton.OnClicked -= SurfacesController.I.NextStep;
            ItemsCounter.GoalAchieved -= _nextStepButton.Show;
        }

        public static void EnableScreenSaver(bool enable)
        {
            
        }
    }
}
using System;
using UnityEngine;

namespace Scripts.UI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private NextStepButton _nextStepButton;
        [SerializeField] private StartButton _startButton;

        private void OnEnable()
        {
            _startButton.OnClicked += StepsController.I.Start;
            _nextStepButton.OnClicked += StepsController.I.NextStep;
            ItemsCounter.GoalAchieved += _nextStepButton.Show;
        }

        private void OnDisable()
        {
            _startButton.OnClicked -= StepsController.I.Start;
            _nextStepButton.OnClicked -= StepsController.I.NextStep;
            ItemsCounter.GoalAchieved -= _nextStepButton.Show;
        }
    }
}
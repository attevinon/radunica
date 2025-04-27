using System;
using Scripts.Data;
using TMPro;
using UnityEngine;

namespace Scripts.UI
{
    public class UIController : MonoBehaviour
    {
        public event Action EndGameButtonClicked;

        [SerializeField] private HintsConfig _hintsConfig;
        [SerializeField] private CutsceneController _cutsceneController;
        [SerializeField] private MyButton _startButton;
        [SerializeField] private MyButton _nextStepButton;
        [SerializeField] private MyButton _doneButton;
        [SerializeField] private MyButton _endGameButton;
        [SerializeField] private TMP_Text _hint;
        [SerializeField] private CanvasGroup _screensaver;

        public CutsceneController CutsceneController => _cutsceneController;

        private void Awake()
        {
            SetHintToEmpty();
        }

        public void Initialize()
        {
            _startButton.OnClicked += SurfacesController.I.Start;
            _nextStepButton.OnClicked += SurfacesController.I.NextStep;
            ItemsCounter.GoalAchieved += _nextStepButton.Show;
            
            SurfacesController.I.HideHint += SetHintToEmpty;
            SurfacesController.I.TargetItemSet += SetHint;
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
            
            SurfacesController.I.HideHint -= SetHintToEmpty;
            SurfacesController.I.TargetItemSet -= SetHint;
            SurfacesController.I.AllDone -= _doneButton.Show;
            
            _doneButton.OnClicked -= _cutsceneController.ShowEndCutscene;
            _cutsceneController.StartShown -= ShowStartButton;
            _cutsceneController.EndShown -= ShowEndGameButton;
            _endGameButton.OnClicked -= EndGameButtonClicked;
        }
        public void EnableScreenSaver(bool enable)
        {
            
        }

        private void SetHintToEmpty()
        {
            _hint.text = string.Empty;
        }

        private void SetHint(ItemTag item)
        {
            string text = GetHintByItem(item);
            _hint.text = text;
        }

        private string GetHintByItem(ItemTag itemTag)
        {
            foreach (var hint in _hintsConfig.Hints)
            {
                if(hint.Tag != itemTag) continue;
                return hint.HintText;
            }

            return string.Empty;
        }

        private void ShowStartButton() => _startButton.Show();
        private void ShowEndGameButton() => _endGameButton.Show();
    }
}
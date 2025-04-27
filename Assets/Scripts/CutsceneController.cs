using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts
{
    public class CutsceneController : MonoBehaviour
    {
        public event Action StartShown;
        public event Action EndShown;
        //start config
        //end config

        private bool _isStart;
        private bool _isEnd;

        [SerializeField] private float _fadeDuration;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _line;

        private void Awake()
        {
            _canvasGroup.enabled = true;
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(NextLine);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(NextLine);
        }

        public void ShowStartCutscene()
        {
            _isStart = true;
            _canvasGroup
                .DOFade(0f, _fadeDuration)
                .SetDelay(5f)
                .SetLink(gameObject)
                .OnComplete(ShowComplete)
                .Play();
        }
        
        public void ShowEndCutscene()
        {
            _isEnd = true;
            EnableThis(true);
            _canvasGroup
                .DOFade(1f, _fadeDuration)
                .SetLink(gameObject)
                .OnComplete(() =>
                {
                    _isEnd = false;
                    EndShown?.Invoke();

                })
                .Play();
        }

        private void EnableThis(bool enable)
        {
            _button.enabled = enable;
            _canvasGroup.interactable = enable;
            _canvasGroup.blocksRaycasts = enable;
        }

        private void ShowComplete()
        {
            _isStart = false;
            EnableThis(false);
            StartShown?.Invoke();
        }

        private void NextLine()
        {
            
        }

        private void SetText(string text)
        {
            _line.text = text;
        }
    }
}
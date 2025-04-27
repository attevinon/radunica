using System;
using DG.Tweening;
using Scripts.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts
{
    public class CutsceneController : MonoBehaviour
    {
        public event Action StartShown;
        public event Action EndShown;

        [SerializeField] private CutsceneConfig _startConfig;
        [SerializeField] private CutsceneConfig _endConfig;

        [SerializeField] private float _fadeDuration;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _line;
        [SerializeField] private float _textFadeDuration = 0.2f;
        
        private bool _isStart;
        private bool _isEnd;
        private int _index;

        private void Awake()
        {
            _canvasGroup.enabled = true;
            _button.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(ProcessClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(ProcessClick);
        }

        public void ShowStartCutscene()
        {
            _isStart = true;
            _index = 0;
            SetText(_startConfig.Lines[_index]);
        }
        
        public void ShowEndCutscene()
        {
            _isEnd = true;
            _index = 0;
            EnableThis(true);
            _canvasGroup
                .DOFade(1f, _fadeDuration)
                .SetLink(gameObject)
                .OnComplete(() => SetText(_endConfig.Lines[_index]))
                .Play();
        }
        
        private void ShowComplete()
        {
            _isStart = false;
            EnableThis(false);
            StartShown?.Invoke();
        }

        private void EnableThis(bool enable)
        {
            _canvasGroup.interactable = enable;
            _canvasGroup.blocksRaycasts = enable;
        }

        private void ProcessClick()
        {
            if(_line.color.a > 0.98f)
                NextLine();
        }

        private void NextLine()
        {
            _index++;
            if (_isStart)
            {
                if (_index >= _startConfig.Lines.Length)
                {
                    SetText(string.Empty, false);
                    _canvasGroup
                        .DOFade(0f, _fadeDuration)
                        .SetDelay(1f)
                        .SetLink(gameObject)
                        .OnComplete(ShowComplete)
                        .Play();
                    return;
                }
                
                SetText(_startConfig.Lines[_index]);
            }
            else if (_isEnd)
            {
                if (_index == _endConfig.Lines.Length)
                {
                    _isEnd = false;
                    _button.gameObject.SetActive(false);
                    EndShown?.Invoke();
                    return;
                }
                
                SetText(_endConfig.Lines[_index]);
            }
        }

        private void SetText(string text, bool enableButton = true)
        {
            _button.gameObject.SetActive(false);
            _line
                .DOFade(0.5f, _textFadeDuration)
                .SetDelay(0.02f)
                .SetLoops(2, LoopType.Yoyo)
                .OnStepComplete(() => _line.text = text)
                .OnComplete(() => _button.gameObject.SetActive(enableButton))
                .SetLink(gameObject)
                .Play();
        }
    }
}
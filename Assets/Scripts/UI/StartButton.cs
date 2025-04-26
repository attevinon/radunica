using System;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI
{
    [RequireComponent(typeof(Button))]
    public class StartButton : MonoBehaviour
    {
        public event Action OnClicked;
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveAllListeners();
        }

        private void OnClick()
        {
            Hide();
            OnClicked?.Invoke();
        }

        private void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class NextStepButton : MonoBehaviour
{
    public event Action OnClicked;
    private Button _button;
    private void Awake()
    {
        gameObject.SetActive(false);
        _button = GetComponent<Button>();
    }
    
    private void OnEnable()
    {
        _button.onClick.AddListener(OnClick);
    }

    private void OnDisable()
    {
        _button?.onClick.RemoveAllListeners();
    }

    private void OnClick()
    {
        Hide();
        OnClicked?.Invoke();
    }

    public void Show()
    {
        gameObject.SetActive(true);
        //todo fade animation
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}

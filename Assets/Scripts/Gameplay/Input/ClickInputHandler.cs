using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Scripts.Input
{
    public class ClickInputHandler : MonoBehaviour, IInputHandler
    {
        public event Action Done;
        private bool _isMousePressed;

        private void OnMouseDown()
        {
            _isMousePressed = true;
        }

        private void OnMouseUp()
        {
            if(!_isMousePressed) return;
            Done?.Invoke();
        }
    }
}
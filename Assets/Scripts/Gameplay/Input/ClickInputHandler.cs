using System;
using UnityEngine;

namespace Scripts.Input
{
    public class ClickInputHandler : MonoBehaviour, IInputHandler
    { 
        [SerializeField] private AudioSource _releasedAudioSource;

        public event Action Done;
        private bool _isMousePressed;

        private void OnMouseDown()
        {
            _isMousePressed = true;
        }

        private void OnMouseUp()
        {
            if(!_isMousePressed)
                return;
            
            if(_releasedAudioSource != null) 
                _releasedAudioSource.Play();
            
            Done?.Invoke();
        }
    }
}
using System;
using DG.Tweening;
using UnityEngine;

namespace Scripts.Input
{
    public class DragAndDropInputHandler : MonoBehaviour, IInputHandler
    {
        public event Action Done;
        private static bool _isInHand;
        
        [SerializeField] private Transform[] placesToPut;
        [SerializeField] private float radius = 0.5f;
        [SerializeField] private float _moveDuration = 0.3f;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _takeSound;
        [SerializeField] private AudioClip _placeSound;

        private bool _isThisInHand;
        private Camera _camera;
        private void Update()
        {
            if(!_isThisInHand) return;
            Vector3 mousePos = _camera.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
            mousePos.z = 0f;
            transform.position = mousePos;
        }
        
        private void OnMouseDown()
        {
            if(_isInHand)
                return;
            
            _camera = Camera.main;
            _isInHand = true;
            _isThisInHand = true;
            _audioSource.PlayOneShot(_takeSound);
        }

        private void OnMouseUp()
        {
            if(!_isThisInHand)
                return;
            
            Vector3 mousePos = _camera.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
            foreach (var place in placesToPut)
            {
                if(place == null)
                    continue;
                
                Vector3 distance = mousePos - place.position;
                float absX = Mathf.Abs(distance.x);
                float absY = Mathf.Abs(distance.y);

                if (absX < radius && absY < radius)
                {
                    Done?.Invoke();
                    transform
                        .DOMove(place.position, _moveDuration)
                        .SetLink(gameObject)
                        .OnComplete(() => Destroy(place.gameObject))
                        .Play();
                    
                    _audioSource.PlayOneShot(_placeSound);
                    _isInHand = false;
                    _isThisInHand = false;
                    return;
                }
            }
        }
    }
}
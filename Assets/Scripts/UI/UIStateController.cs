using System;
using PixelCrushers;
using PixelCrushers.DialogueSystem;
using UnityEngine;

namespace UI
{
    public class UIStateController : MonoBehaviour
    {
        [SerializeField] private CameraController _camera;
        [SerializeField] private InputController _inputController;

        private void Update()
        {
            var DSC = GetComponent<DialogueSystemController>();
            var gg = DSC.currentConversant;
            if (gg != null)
            {
                _camera.enabled = false;
                _inputController.enabled = false;
            }
            else
            {
                _camera.enabled = true;
                _inputController.enabled = true;
            }
        }
    }
    
    
}
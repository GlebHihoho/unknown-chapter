using System;
using PixelCrushers.DialogueSystem;
using UI.InventoryScripts;
using UnityEngine;

namespace UI
{
    public class UIStateController : MonoBehaviour
    {
        [SerializeField] private CameraController _camera;
        private MouseInput _mouseInput;
        private GameObject _playerHUD;


        private void Start()
        {
            _mouseInput = FindObjectOfType<MouseInput>(true);
            _playerHUD = FindObjectOfType<ItemsDB>(true).gameObject;
        }

        private void Update()
        {
            var DSC = GetComponent<DialogueSystemController>();
            var gg = DSC.currentConversant;
            if (gg != null)
            {
                _camera.enabled = false;
                _mouseInput.enabled = false;
                _playerHUD.SetActive(false);
            }
            else
            {
                _camera.enabled = true;
                _mouseInput.enabled = true;
                _playerHUD.SetActive(true);
            }
        }
    }
    
    
}
﻿using System;
using UnityEngine;

namespace DefaultNamespace.TestTools
{
    public class TestToolManager : MonoBehaviour
    {
        [SerializeField] private GameObject _testToolKitItem;
        [SerializeField] private GameObject _testToolMenuItem;
        [SerializeField] private GameObject _testMainCharacteristic;
        [SerializeField] private GameObject _sceneReloaded;
        [SerializeField] private GameObject _saveLoadButton;
        private bool isActive = false;

        private void Update()
        {
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.K))
            {
                isActive = !isActive;
                _testToolKitItem.SetActive(isActive);
                _testToolMenuItem.SetActive(isActive);
                _testMainCharacteristic.SetActive(isActive);
                _sceneReloaded.SetActive(isActive);
            }

            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.L))
            {
                if (_saveLoadButton)
                {
                    _saveLoadButton.SetActive(!_saveLoadButton.activeSelf);
                }
            }
        }
    }
}
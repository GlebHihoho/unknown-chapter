using System;
using UnityEngine;

namespace DefaultNamespace.TestTools
{
    public class TestToolManager : MonoBehaviour
    {
        [SerializeField] private GameObject _estToolKitItem;
        [SerializeField] private GameObject _estToolMenuItem;
        [SerializeField] private GameObject _estMainCharacteristic;
        [SerializeField] private GameObject _sceneReloaded;
        private bool isActive = false;

        private void Update()
        {
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.K))
            {
                isActive = !isActive;
                _estToolKitItem.SetActive(isActive);
                _estToolMenuItem.SetActive(isActive);
                _estMainCharacteristic.SetActive(isActive);
                _sceneReloaded.SetActive(isActive);
            }
        }
    }
}
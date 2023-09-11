using System;
using UnityEngine;

namespace DefaultNamespace.TestTools
{
    public class TestToolManager : MonoBehaviour
    {
        [SerializeField] private GameObject TestToolKitItem;
        [SerializeField] private GameObject TestToolMenuItem;
        [SerializeField] private GameObject TestMainCharacteristic;
        private bool isActive = false;

        private void Update()
        {
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.K))
            {
                isActive = !isActive;
                TestToolKitItem.SetActive(isActive);
                TestToolMenuItem.SetActive(isActive);
                TestMainCharacteristic.SetActive(isActive);
            }
        }
    }
}
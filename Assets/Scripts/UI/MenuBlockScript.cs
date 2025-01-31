using System;
using UnityEngine;

namespace UI
{
    public class MenuBlockScript : MonoBehaviour
    {
        [SerializeField] private GameObject _workingPanel;

        private Transform _child;

        bool isEnabled = true;

        private void Start()
        {
            _child = transform.Find("Image");

            UIController.OnEnableUI += EnableUI;
        }

        private void OnDestroy() => UIController.OnEnableUI -= EnableUI;

        private void EnableUI(bool isEnabled) => this.isEnabled = isEnabled;


        void Update()
        {
            _child.gameObject.SetActive(_workingPanel.activeSelf);
        }


        public void OnClick()
        {
            if (isEnabled)
            {
                _workingPanel.SetActive(!_workingPanel.activeSelf);
            }
        }
    }
}

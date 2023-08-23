using System;
using UnityEngine;

namespace UI
{
    public class MenuBlockScript : MonoBehaviour
    {
        [SerializeField] private GameObject _workingPanel;

        private Transform _child;

        private void Start()
        {
            _child = transform.Find("Image");
        }

        void Update()
        {
            _child.gameObject.SetActive(_workingPanel.activeSelf);
        }

        public void OnClick()
        {
            _workingPanel.SetActive(!_workingPanel.activeSelf);
        }
    }
}

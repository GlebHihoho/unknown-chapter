using System;
using Unity.VisualScripting;
using UnityEngine;

namespace DefaultNamespace
{
    
    public class Hightlight : MonoBehaviour
    {
        
        [SerializeField] private Outline _outline;

        private void Start()
        {
            _outline = GetComponent<Outline>();
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.LeftAlt))
            {
                _outline.enabled = true;
            }
            else if (Input.GetKeyUp(KeyCode.LeftAlt))
            {
                _outline.enabled = false;
            }
            
        }
        
        private void OnMouseEnter()
        {
            _outline.enabled = true;
        }

        private void OnMouseExit()
        {
            _outline.enabled = false;
        }
        
    }
}
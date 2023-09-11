using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class DestroyObject : MonoBehaviour
    {
        private Transform _player;
        [SerializeField]private QuickItemView _quickItemView;

        private void Start()
        {
            _player = GameObject.FindWithTag("Player").transform;
            _quickItemView = FindObjectOfType<QuickItemView>();
        }

        public void ThisDestroyObject()
        {
            _player.GetComponent<InputController>().enabled = true;
            _quickItemView.SetIsInventoryOpen(false);
            Destroy(transform.parent.gameObject);
        }
    }
}

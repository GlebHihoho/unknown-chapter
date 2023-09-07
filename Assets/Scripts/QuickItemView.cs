using System;
using UI;
using UI.InventoryScripts;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    public class QuickItemView : MonoBehaviour
    {
        //todo изменить названия этого компонента на более читаемое
        [SerializeField] private GameObject _foundItemsUIPrefab;
        [SerializeField] private ItemsList _itemsList;
        [SerializeField] private Transform _containerTransform;
        [SerializeField] private float maxDistanceToOpen = 5f;
        
        private Transform _player;
        private float distance;
        private Vector3 mousePosition;

        private static bool isInventoryOpen = false;

        private void Start()
        {
            _player = GameObject.FindWithTag("Player").transform;
        }

        private void OnMouseDown()
        {
            distance = Vector3.Distance(_player.position, transform.position);

            if (distance <= maxDistanceToOpen)
            {
                if (!isInventoryOpen)
                {
                    mousePosition = Input.mousePosition;
                    OpenView();
                    isInventoryOpen = true;
                }
            }
        }

        private void OpenView()
        {
            _foundItemsUIPrefab.GetComponentInChildren<FoundItem>()._listOfItems = _itemsList;
            Instantiate(_foundItemsUIPrefab, mousePosition, Quaternion.identity, _containerTransform);
            _player.GetComponent<InputController>().enabled = false;
        }
        
        public void QuickItemViewDestroy()
        {
            isInventoryOpen = false;
            // Destroy(transform.parent.gameObject);
            Destroy(_foundItemsUIPrefab);
            _player.GetComponent<InputController>().enabled = true;
        }
    }
}
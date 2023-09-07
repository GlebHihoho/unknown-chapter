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
        private Vector3 mousePosition;

        public static bool isInventoryOpen = false;


        private void OnMouseDown()
        {
            if (!isInventoryOpen)
            {
                mousePosition = Input.mousePosition;
                OpenView();
                isInventoryOpen = true;
            }
            
        }

        private void OpenView()
        {
            _foundItemsUIPrefab.GetComponentInChildren<FoundItem>()._listOfItems = _itemsList;
            Instantiate(_foundItemsUIPrefab, mousePosition, Quaternion.identity, _containerTransform);
            // _foundItemsUIPrefab.GetComponentInChildren<FoundItem>()._inventory = FindObjectOfType<Inventory>();
        }
        
    }
}
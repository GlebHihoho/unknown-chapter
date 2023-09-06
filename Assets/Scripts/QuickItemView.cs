using System;
using UI;
using UnityEngine;

namespace DefaultNamespace
{
    public class QuickItemView : MonoBehaviour
    {
        //todo изменить названия этого компонента на более читаемое
        [SerializeField] private GameObject _foundItemsUI;
        [SerializeField] private ItemsList _itemsList;
        // [SerializeField] private Transform _containerTransform;
        // [SerializeField] private ItemsList _itemsListFound;

        private void Start()
        {
            // _itemsListFound = _foundItemsUI.GetComponentInChildren<FoundItem>()._listOfItems;
        }

        private void OnMouseDown()
        {
            print(_itemsList);
            OpenView();
        }

        private void OpenView()
        {
            _foundItemsUI.GetComponentInChildren<FoundItem>()._listOfItems = _itemsList;
            // Instantiate(_foundItemsUI, _containerTransform);
            _foundItemsUI.SetActive(true);
        }
        
    }
}
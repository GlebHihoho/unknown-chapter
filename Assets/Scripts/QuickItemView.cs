using System;
using System.Collections.Generic;
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
        [SerializeField] public ItemsList _itemsList;
        [SerializeField] public List<ItemSO> _itemsListFilling;
        [SerializeField] private Transform _containerTransform;
        [SerializeField] private float maxDistanceToOpen = 5f;
        [SerializeField] private bool _isDeleteObject = false;
        
        private Transform _player;
        private float distance;
        private Vector3 mousePosition;
        private static bool isInventoryOpen = false;
        
        private void Start()
        {
            foreach (var itemsWithCount in _itemsList.itemsWithCount)
            {
                    ItemSO newItem = Instantiate(itemsWithCount.item); // Создаем копию объекта из _itemList
                    newItem._count = itemsWithCount.count;
                    _itemsListFilling.Add(newItem); // Добавляем копию в _itemListFilling
            }
        }

        private void OnMouseDown()
        {
            _player = GameObject.FindWithTag("Player").transform;
            distance = Vector3.Distance(_player.position, transform.position);
            if (distance <= maxDistanceToOpen)
            {
                if (!isInventoryOpen)
                {
                    mousePosition = Input.mousePosition;
                    isInventoryOpen = true;
                    OpenView();
                }
            }
        }

        private void OpenView()
        {
            GameObject go = Instantiate(_foundItemsUIPrefab, mousePosition, Quaternion.identity, _containerTransform);
            go.GetComponentInChildren<FoundItem>()._quickItemView = this;
            _player.GetComponent<InputController>().enabled = false;
            if(_isDeleteObject)
            {
                Destroy(gameObject);
            }
        }
        
        public void SetIsInventoryOpen(bool value)
        {
            isInventoryOpen = value;
        }
    }
}
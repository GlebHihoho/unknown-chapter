﻿using System;
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
        [SerializeField] private ItemsList _itemsList;
        public List<ItemSO> _itemsListFilling;
        [SerializeField] private Transform _containerTransform;
        [SerializeField] private float maxDistanceToOpen = 5f;
        
        private Transform _player;
        private float distance;
        private Vector3 mousePosition;

        private static bool isInventoryOpen = false;

        private void Start()
        {
            _itemsListFilling = _itemsList.items;
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
                    isInventoryOpen = true;
                    OpenView();
                }
            }
        }

        private void OpenView()
        {
            _foundItemsUIPrefab.GetComponentInChildren<FoundItem>()._listOfItems.items = _itemsListFilling;
            GameObject go = Instantiate(_foundItemsUIPrefab, mousePosition, Quaternion.identity, _containerTransform);
            _foundItemsUIPrefab.GetComponentInChildren<FoundItem>()._quickItemView = this;
            _player.GetComponent<InputController>().enabled = false;
            isInventoryOpen = false;
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
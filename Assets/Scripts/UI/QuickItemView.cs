using System.Collections.Generic;
using ScriptableObjects;
using UI;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    public class QuickItemView : MonoBehaviour
    {
        [SerializeField] private GameObject _foundItemsUIPrefab;
        [SerializeField] private Transform _containerTransform;
        [SerializeField] private float _maxDistanceToOpen = 5f;
        [SerializeField] private bool _isDeleteObject = false;
        
        [FormerlySerializedAs("_itemsList")] [SerializeField] public ItemsList ItemsList;
        [FormerlySerializedAs("_itemsListFilling")] [SerializeField] public List<ItemSO> ItemsListFilling;

        private Transform _player;
        private float _distance;
        private Vector3 _mousePosition;
        private static bool _isInventoryOpen = false;
        
        private void Start()
        {
            foreach (var itemsWithCount in ItemsList.itemsWithCount)
            {
                    ItemSO newItem = Instantiate(itemsWithCount.item);
                    newItem._count = itemsWithCount.count;
                    ItemsListFilling.Add(newItem);
            }
        }

        private void OnMouseDown()
        {
            _player = GameObject.FindWithTag("Player").transform;
            _distance = Vector3.Distance(_player.position, transform.position);
            if (_distance <= _maxDistanceToOpen)
            {
                if (!_isInventoryOpen)
                {
                    _mousePosition = Input.mousePosition;
                    _isInventoryOpen = true;
                    OpenView();
                }
            }
        }

        private void OpenView()
        {
            GameObject obj = Instantiate(_foundItemsUIPrefab, _mousePosition, Quaternion.identity, _containerTransform);
            obj.GetComponentInChildren<FoundItem>()._quickItemView = this;
            if(_isDeleteObject)
            {
                Destroy(gameObject);
            }
        }
        
        public void SetIsInventoryOpen(bool value)
        {
            _isInventoryOpen = value;
        }
    }
}

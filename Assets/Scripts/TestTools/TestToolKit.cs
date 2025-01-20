using System;
using UI.InventoryScripts;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace.TestTools
{
    public class TestToolKit : MonoBehaviour
    {
        [SerializeField] private Inventory _inventory;
        [FormerlySerializedAs("_mainCharacteristics")] [SerializeField] private Characteristics characteristics;
        [SerializeField] private AllItemCollectionsSO _allItemCollectionsSo;
        [SerializeField] private GameObject _prefab;
        [SerializeField] private Transform _contentParent;
        // [SerializeField] private TestToolButtonUI testToolButtonUI;

        private void Awake()
        {
            foreach (var item in _allItemCollectionsSo._allItemCollections)
            {
                var obj = Instantiate(_prefab, _contentParent);
                var a = obj.GetComponentInChildren<TestToolButtonUI>()._inventory = _inventory;
                // testToolButtonUI.ButtonFilling(item);
                obj.GetComponent<TestToolButtonUI>().ButtonFilling(item);
            }
        }

        private void Update()
        {
        }
    }
}
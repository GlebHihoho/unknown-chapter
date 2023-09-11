using System;
using UI.InventoryScripts;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace.TestTools
{
    public class TestToolKit : MonoBehaviour
    {
        [SerializeField] private Inventory _inventory;
        [SerializeField] private MainCharacteristics _mainCharacteristics;
        [SerializeField] private AllItemCollectionsSO _allItemCollectionsSo;
        [SerializeField] private GameObject _prefab;
        [SerializeField] private Transform _contentParent;
        [FormerlySerializedAs("_testButtonItem")] [SerializeField] private TestToolButtonUI testToolButtonUI;

        private void Awake()
        {
            foreach (var item in _allItemCollectionsSo._allItemCollections)
            {
                Instantiate(_prefab, _contentParent);
                testToolButtonUI.ButtonFilling(item);
            }
        }

        private void Update()
        {
            throw new NotImplementedException();
        }
    }
}
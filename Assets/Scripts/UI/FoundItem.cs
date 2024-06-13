using System.Collections.Generic;
using System.IO;
using DefaultNamespace;
using ScriptableObjects;
using TMPro;
using UI.InventoryScripts;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class FoundItem : MonoBehaviour
    {
        //todo проверить надежность публичных полей
        [SerializeField] public ItemsList _listOfItems;

        [SerializeField] private Inventory _inventory;

        [SerializeField] private GameObject _gameObjShow;

        [SerializeField] public List<ItemInventory> _items = new();
        
        [SerializeField] private Button _takeAllButton;

        [SerializeField] public QuickItemView _quickItemView;
        [SerializeField] public List<ItemSO> _itemsListFilling;

        private void Start()
        {
            print(_quickItemView);
            _inventory = FindAnyObjectByType<Inventory>(FindObjectsInactive.Include);

            foreach (var itemSo in _quickItemView.ItemsListFilling)
            {
                AddGraphics(itemSo);
            }

            for (int i = 0; i < _quickItemView.ItemsListFilling.Count; i++)
            {
                var item = _quickItemView.ItemsListFilling[i];
                AddItem(i, item);
            }

            _takeAllButton.onClick.AddListener(TakeAll);
        }

        private void AddGraphics(ItemSO t) //добавляет кнопку предмета в инвентарь
        {
            var newItem = Instantiate(_gameObjShow, transform);

            newItem.name = t._name;

            var ii = new ItemInventory
            {
                _itemGameObj = newItem
            };

            var rt = newItem.GetComponent<RectTransform>();
            rt.localPosition = new Vector3(0, 0, 0);
            rt.localScale = new Vector3(1, 1, 1);
            newItem.GetComponentInChildren<RectTransform>().localScale = new Vector3(1, 1, 1);

            var text = newItem.GetComponentInChildren<TextMeshProUGUI>();
            text.text = t._count.ToString();

            Button tempButton = newItem.GetComponent<Button>();

            _items.Add(ii);
        }

        private void AddItem(int id, ItemSO item) //добавляет предмет в лист ItemInventory
        {
            Sprite sprite = Resources.Load<Sprite>(item._img);

            _items[id]._name = item._name;
            _items[id]._count = item._count;
            _items[id]._itemGameObj.GetComponentInChildren<Image>().sprite = sprite;
            _items[id]._isUsed = false;
            _items[id]._description = item._description;
        }

        private void TakeAll()
        {
            
            if (_quickItemView.ItemsListFilling != null)
            {
                for (int i = 0; i < _quickItemView.ItemsListFilling.Count;)
                {
                    _inventory.AddObject(new Item()
                    {
                        Description = _quickItemView.ItemsListFilling[i]._description,
                        Img = _quickItemView.ItemsListFilling[i]._img,
                        IsUsed = false,
                        Name = _quickItemView.ItemsListFilling[i]._name,
                        Count = _quickItemView.ItemsListFilling[i]._count
                    });
                    _items.Remove(_items.Find(x => x._name == _quickItemView.ItemsListFilling[i]._name));
                    GameObject childObject = transform.Find(_quickItemView.ItemsListFilling[i]._name).gameObject;
                    Destroy(childObject);
                    _quickItemView.ItemsListFilling.Remove(_quickItemView.ItemsListFilling[i]);
                }
            }
        }
    }
}
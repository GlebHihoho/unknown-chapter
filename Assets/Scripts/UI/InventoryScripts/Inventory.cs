using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Newtonsoft.Json;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;

namespace UI.InventoryScripts
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] public ItemsDB _data;

        [SerializeField] private List<ItemInventory> _items = new();

        [SerializeField] private GameObject _gameObjShow;

        [SerializeField] private GameObject _inventoryMainObject;

        [SerializeField] private EventSystem _es;

        [SerializeField] private int _currentID;
        [SerializeField] private ItemInventory _currentItem;

        [SerializeField] private GameObject _backGround;

        [SerializeField] private GameObject _selectItemImage;

        [SerializeField] private Button _useButton;
        [SerializeField] private Button _deleteButton;

        [SerializeField] private TextMeshProUGUI _nameOfItem;
        [SerializeField] private TextMeshProUGUI _descriptionOfItem;

        [SerializeField] private AllItemCollectionsSO _allItemCollectionsSO;

        public void Start()
        {
            //todo разобраться с json
            // string json = File.ReadAllText(Application.dataPath + "/items.json");
            // _data._items = JsonConvert.DeserializeObject<List<Item>>(json);

            ChangeInventory();

            UpdateInventory();
        }

        public void Update()
        {
            UpdateInventory();
        }

        private void ChangeInventory()
        {
            if (_items.Count == 0)
            {
                foreach (var t in _data._items)
                {
                    AddGraphics(t);
                }
            }

            for (int i = 0; i < _data._items.Count; i++)
            {
                var item = _data._items[i];
                AddItem(i, item);
            }
        }

        private void AddItem(int id, Item item) //добавляет предмет в лист ItemInventory
        {
            Texture2D itemImage = new Texture2D(60, 60);
            byte[] imageData = File.ReadAllBytes(Application.dataPath + item.Img);
            itemImage.LoadImage(imageData);
            Sprite sprite = Sprite.Create(itemImage, new Rect(0, 0, itemImage.width, itemImage.height),
                Vector2.one * 0.5f);

//            _items[id]._id = item.ID;
            _items[id]._name = item.Name;
            _items[id]._count = item.Count;
            _items[id]._itemGameObj.GetComponentInChildren<Image>().sprite = sprite;
            _items[id]._isUsed = item.IsUsed;
            _items[id]._description = item.Description;

            if (item.Count > 1)
            {
                _items[id]._itemGameObj.GetComponentInChildren<TextMeshProUGUI>().text = item.Count.ToString();
            }
            else
            {
                _items[id]._itemGameObj.GetComponentInChildren<TextMeshProUGUI>().text = "";
            }
        }

        private void AddGraphics(Item t) //добавляет кнопку предмета в инвентарь
        {
            var newItem = Instantiate(_gameObjShow, _inventoryMainObject.transform);

            newItem.name = t.Name;

            var ii = new ItemInventory
            {
                _itemGameObj = newItem
            };

            var rt = newItem.GetComponent<RectTransform>();
            rt.localPosition = new Vector3(0, 0, 0);
            rt.localScale = new Vector3(1, 1, 1);
            newItem.GetComponentInChildren<RectTransform>().localScale = new Vector3(1, 1, 1);

            Button tempButton = newItem.GetComponent<Button>();

            tempButton.onClick.AddListener(SelectObject);

            _items.Add(ii);

        }

        // ReSharper disable Unity.PerformanceAnalysis
        public void UpdateInventory()
        {
            for (var i = 0; i < _data._items.Count; i++)
            {
                if (_items[i]._count > 1)
                {
                    _items[i]._itemGameObj.GetComponentInChildren<TextMeshProUGUI>().text = _items[i]._count.ToString();
                }
                else
                {
                    _items[i]._itemGameObj.GetComponentInChildren<TextMeshProUGUI>().text = "";
                }

                _items[i]._itemGameObj.transform.GetChild(1).GetComponent<Image>().enabled = _items[i]._isUsed;
            }
        }

        private void SelectObject()
        {
            if (_currentID == -1)
            {
                _currentItem = _items.Find(x => x._name == _es.currentSelectedGameObject.name);

                _selectItemImage.GetComponent<Image>().sprite =
                    _currentItem._itemGameObj.GetComponentInChildren<Image>().sprite;
                DescriptionObject(_currentItem);

                _deleteButton.onClick.AddListener(DeleteObject);

                _useButton.onClick.AddListener(UseObject);
            }

            UpdateInventory();
        }

        private void DeleteObject()
        {
            _data._items.Remove(_data._items.Find(x => x.Name == _currentItem._name));
            _items.Remove(_items.Find(x => x._name == _currentItem._name));
            GameObject childObject = _inventoryMainObject.transform.Find(_currentItem._name).gameObject;
            Destroy(childObject);
            UpdateInventory();
        }

        private void UseObject()
        {

        }

        // ReSharper disable Unity.PerformanceAnalysis
        public void AddObject(Item itemForAdd)
        {

            if (_data._items.Find(x => x.Name == itemForAdd.Name) != null)
            {
                _data._items.Find(x => x.Name == itemForAdd.Name).Count++;
                _items.Find(x => x._name == itemForAdd.Name)._count++;
            }
            else
            {
                _data._items.Add(itemForAdd);
                //ChangeInventory();
                AddGraphics(itemForAdd);
                Debug.Log(_items.Count);

                AddItem(_items.Count - 1, itemForAdd);
            }

            UpdateInventory();
        }

        private void DescriptionObject(ItemInventory currentItem)
        {
            _nameOfItem.text = currentItem._name;
            _descriptionOfItem.text = currentItem._description;
        }

        private void OnApplicationQuit()
        {
            string path = Application.dataPath + "/items.json";

            File.WriteAllText(path, JsonConvert.SerializeObject(_data._items));
        }

        public double GetItemAmount(string itemName)
        {
            var item = _items.Find(x => x._name == itemName);
            return (item != null) ? item._count : 0;
        }

        public void DialogueSystemItemDeleter(string itemName, double count)
        {
            var item = _items.Find(x => x._name == itemName);
            if (item._count > (int)count)
            {
                item._count -= (int)count;
            }
            else
            {
                _data._items.Remove(_data._items.Find(x => x.Name == itemName));
                _items.Remove(_items.Find(x => x._name == itemName));
                GameObject childObject = _inventoryMainObject.transform.Find(itemName).gameObject;
                Destroy(childObject);
                UpdateInventory();
            }
        }

        public void DialogueSystemItemAdder(string itemName, double count)
        {
            //todo переписать код с цикла, на увеличение количества "Count" через переменную
            var item = _allItemCollectionsSO._allItemCollections.Find(x => x._name == itemName);
            double countAdd;

            try
            {
                double currentCount = _data._items.Find(x => x.Name == itemName).Count;
                countAdd = currentCount + count;
            }
            catch (Exception e)
            {
                countAdd = count;
            }

            AddObject(new Item()
            {
                Description = item._description,
                Img = item._img,
                IsUsed = false,
                Name = item._name,
                // Count = (int)count,
                Count = item._count,
            });
            _data._items.Find(x => x.Name == itemName).Count = (int)countAdd;
            ChangeInventory();
        }

        public void DeleteAllItem()
        {
            for(int i = 0; i < _items.Count;)
            {
                var itemName = _items[i]._name;
                var itemCount = _items.Find(x => x._name == itemName)._count;
                DialogueSystemItemDeleter(itemName, itemCount);
            }
        }
    }


    [Serializable]
    public class ItemInventory
    {
        //public int _id;
        public string _name;
        public GameObject _itemGameObj;
        public int _count;
        public bool _isUsed;
        public string _description;
    }
}
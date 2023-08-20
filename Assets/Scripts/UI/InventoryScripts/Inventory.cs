using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.EventSystems;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;
using System.Text.Json;

namespace UI.InventoryScripts
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private ItemsDB _data;

        [SerializeField] private List<ItemInventory> _items = new();

        [SerializeField] private GameObject _gameObjShow;

        [SerializeField] private GameObject _inventoryMainObject;
        [SerializeField] private int _maxCount;

        [SerializeField] private Camera _cam;
        [SerializeField] private EventSystem _es;

        [SerializeField] private int _currentID;
        [SerializeField] private ItemInventory _currentItem;

        [SerializeField] private RectTransform _movingObject;
        [SerializeField] private Vector3 _offset;

        [SerializeField] private GameObject _backGround;

        private const int MaxNumOfObjectInCall = 10;
        private string _json = "";
        private ItemsDB _itemsDB;
        
        private List<Item> test = new List<Item>()
        {
            new Item()
            {
                _id = 1,
                _name = "Кольцо",
                _count = 1,
                _img = "",
                _description = @"\Assets\Graphics\Sprites\332938.png",
                _isUsed = true
            },
            new Item()
            {
                _id = 0,
                _name = "Empty",
                _count = 0,
                _img = "",
                _description = "",
                _isUsed = false
            },
        };

        public void Start()
        {
            // try
            // {
            //     StreamReader jsonFile = File.OpenText(@"items.json");
            //     _json = jsonFile.ReadToEnd();
            //     jsonFile.Close();
            // }
            // catch (Exception e)
            // {
            //     Console.WriteLine("Exception: " + e.Message);
            // }

            _data._items = test;

            
            if (_items.Count == 0)
            {  
                AddGraphics();
            }

            // for (int i = 1; i < _data._items.Count; i++)
            // {
            //     var item = _data._items[i];
            //     AddItem(i - 1, item);
            // }
            UpdateInventory();
        }

        public void Update()
        {
            if (_currentID != -1)
            {
                MoveObject();
            }

            if (Input.GetKeyDown(KeyCode.I))
            {
                _backGround.SetActive(!_backGround.activeSelf);
                if (_backGround.activeSelf)
                {
                    UpdateInventory();
                }
            }
        }

        private void AddItem(int id, Item item)
        {
            _items[id]._id = item._id;
            _items[id]._count = item._count;
            _items[id]._itemGameObj.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>(item._img);
            _items[id]._isUsed = item._isUsed;
            _items[id]._description = item._description;
        
            if (item._count > 1 && item._id != 0)
            {
                _items[id]._itemGameObj.GetComponentInChildren<TextMeshProUGUI>().text = item._count.ToString();
            }
            else
            {
                _items[id]._itemGameObj.GetComponentInChildren<TextMeshProUGUI>().text = "";
            }
        }

        private void AddInventoryItem(int id, ItemInventory invItem)
        {
            _items[id]._id = invItem._id;
            _items[id]._count = invItem._count;
            _items[id]._itemGameObj.GetComponent<Image>().sprite = Resources.Load<Sprite>(_data._items[invItem._id]._img);
            _items[id]._isUsed = invItem._isUsed;
            _items[id]._description = invItem._description;
        
            if (invItem._count > 1 && invItem._id != 0)
            {
                _items[id]._itemGameObj.GetComponentInChildren<TextMeshProUGUI>().text = invItem._count.ToString();
            }
            else
            {
                _items[id]._itemGameObj.GetComponentInChildren<TextMeshProUGUI>().text = "";
            }
        }

        private void AddGraphics()
        {
            for (int i = 0; i < _maxCount; i++)
            {
                var newItem = Instantiate(_gameObjShow, _inventoryMainObject.transform);
                
                newItem.name = i.ToString();

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
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private void UpdateInventory()
        {
            for (var i = 0; i < _maxCount; i++)
            {
                if (_items[i]._id != 0 && _items[i]._count > 1)
                {
                    _items[i]._itemGameObj.GetComponentInChildren<TextMeshProUGUI>().text = _items[i]._count.ToString();
                }
                else
                {
                    _items[i]._itemGameObj.GetComponentInChildren<TextMeshProUGUI>().text = "";
                }

                _items[i]._itemGameObj.GetComponent<Image>().sprite = Resources.Load<Sprite>(_data._items[_items[i]._id]._img);

                if (_items[i]._isUsed)
                {
                    _items[i]._itemGameObj.transform.GetChild(1).GetComponent<Image>().enabled = true;
                }
                else
                {
                    _items[i]._itemGameObj.transform.GetChild(1).GetComponent<Image>().enabled = false;
                }
            }
        }

        private void SelectObject()
        {
            if (_currentID == -1 || _currentItem._id == 0)
            {
                _currentID = int.Parse(_es.currentSelectedGameObject.name);
                _currentItem = CopyInventoryItem(_items[_currentID]);

                if (_currentItem._id != 0)
                {
                    _movingObject.gameObject.SetActive(true);
                    _movingObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(_data._items[_currentItem._id]._img);
                    AddItem(_currentID, _data._items[0]);
                }
            }
            else
            {
                var ii = _items[int.Parse(_es.currentSelectedGameObject.name)];

                if (_currentItem._id != ii._id)
                {
                    AddInventoryItem(_currentID, ii);

                    AddInventoryItem(int.Parse(_es.currentSelectedGameObject.name), _currentItem);
                }
                else
                {
                    if (ii._count + _currentItem._count <= MaxNumOfObjectInCall)
                    {
                        ii._count += _currentItem._count;
                    }
                    else
                    {
                        var newItem = _data._items[ii._id];
                        newItem._count = ii._count + _currentItem._count - MaxNumOfObjectInCall;
                        AddItem(_currentID, newItem);

                        ii._count = MaxNumOfObjectInCall;
                    }

                    ii._itemGameObj.GetComponentInChildren<TextMeshProUGUI>().text = ii._count.ToString();
                }

                _currentID = -1;

                _movingObject.gameObject.SetActive(false);
            }
            
            UpdateInventory();
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private void MoveObject()
        {
            Vector3 pos = Input.mousePosition + _offset;
            pos.z = _inventoryMainObject.GetComponent<RectTransform>().position.z;
            _movingObject.position = _cam.ScreenToWorldPoint(pos);
        }

        private ItemInventory CopyInventoryItem(ItemInventory old)
        {
            var newItemInventory = new ItemInventory
            {
                _id = old._id,
                _itemGameObj = old._itemGameObj,
                _count = old._count,
                _isUsed = old._isUsed,
                _description = old._description
            };

            return newItemInventory;
        }

        // private void DescriptionObject()
        // {
        //     _currentID = int.Parse(_es.currentSelectedGameObject.name);
        //     _currentItem = CopyInventoryItem(_items[_currentID]);
        //     
        //     print(_currentItem._description);
        // }

        private void OnApplicationQuit()
        {
            string jsonData = JsonUtility.ToJson(test[1]);
            string path = Application.dataPath + "/items.json";
            
            File.WriteAllText(path, JsonConvert.SerializeObject(test));
        }
    }

    [Serializable]
    public class ItemInventory
    {
        public int _id;
        public GameObject _itemGameObj;
        public int _count;
        public bool _isUsed;
        public string _description;
    }
}
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

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

        public void Start()
        {
            if (_items.Count == 0)
            {  
                AddGraphics();
            }

            for (int i = 0; i < _maxCount; i++) // тестовое заполнение ячеек
            {
                var item = _data.items[Random.Range(0, _data.items.Count)];
                AddItem(i, item);
            }
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
            _items[id]._id = item.id;
            _items[id]._count = item.count;
            _items[id]._itemGameObj.GetComponentInChildren<Image>().sprite = item.img;
            _items[id]._isUsed = item.isUsed;
        
            if (item.count > 1 && item.id != 0)
            {
                _items[id]._itemGameObj.GetComponentInChildren<TextMeshProUGUI>().text = item.count.ToString();
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
            _items[id]._itemGameObj.GetComponent<Image>().sprite = _data.items[invItem._id].img;
            _items[id]._isUsed = invItem._isUsed;
        
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

                _items[i]._itemGameObj.GetComponent<Image>().sprite = _data.items[_items[i]._id].img;

                if (_items[i]._isUsed)
                {
                    _items[i]._itemGameObj.transform.GetChild(1).GetComponent<Image>().enabled = true;
                    //_items[i]._itemGameObj.GetComponentInChildren<Image>().color = new Color(255, 255, 0, 140);
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
                    _movingObject.GetComponent<Image>().sprite = _data.items[_currentItem._id].img;
                    AddItem(_currentID, _data.items[0]);
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
                        var newItem = _data.items[ii._id];
                        newItem.count = ii._count + _currentItem._count - MaxNumOfObjectInCall;
                        AddItem(_currentID, newItem);

                        ii._count = MaxNumOfObjectInCall;
                    }

                    ii._itemGameObj.GetComponentInChildren<TextMeshProUGUI>().text = ii._count.ToString();
                }

                _currentID = -1;

                _movingObject.gameObject.SetActive(false);
            }
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
                _isUsed = old._isUsed
            };

            return newItemInventory;
        }
    }

    [System.Serializable]
    public class ItemInventory
    {
        public int _id;
        public GameObject _itemGameObj;
        public int _count;
        public bool _isUsed;
    }
}
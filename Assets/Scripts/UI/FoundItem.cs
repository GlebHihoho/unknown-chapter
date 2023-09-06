using System.Collections.Generic;
using System.IO;
using DefaultNamespace;
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
    
        [SerializeField] private List<ItemInventory> _items = new();


        [SerializeField] private Button _takeAllButton;

        private void Start()
        {
            //todo удалить лишние принты
            print("Создали экземпляр");
            _inventory = FindObjectOfType<Inventory>(true);
            print(_inventory);
            foreach (var itemSo in _listOfItems.items)
            {
                AddGraphics(itemSo);
            }
        
            for (int i = 0; i < _listOfItems.items.Count; i++)
            {
                var item = _listOfItems.items[i];
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
        
            Button tempButton = newItem.GetComponent<Button>();
        
            _items.Add(ii);
        }
    
        private void AddItem(int id, ItemSO item) //добавляет предмет в лист ItemInventory
        {
            Texture2D itemImage = new Texture2D(60,60);
            byte[] imageData = File.ReadAllBytes(Application.dataPath + item._img);
            itemImage.LoadImage(imageData);
            Sprite sprite = Sprite.Create(itemImage, new Rect(0, 0, itemImage.width, itemImage.height), Vector2.one * 0.5f);
            
            _items[id]._name = item._name;
            _items[id]._count = 1;
            _items[id]._itemGameObj.GetComponentInChildren<Image>().sprite =  sprite;
            _items[id]._isUsed = false;
            _items[id]._description = item._description;
        
        }

        private void TakeAll()
        {
            if (_listOfItems != null)
            {
                while (_listOfItems.items.Count != 0)
                {
                    _inventory.AddObject(new Item()
                    {
                        Count = 1,
                        Description = _listOfItems.items[0]._description,
                        Img = _listOfItems.items[0]._img,
                        IsUsed = false,
                        Name = _listOfItems.items[0]._name
                    });
                    _items.Remove(_items.Find(x => x._name == _listOfItems.items[0]._name));
                    GameObject childObject = transform.Find(_listOfItems.items[0]._name).gameObject;
                    Destroy(childObject);
                    _listOfItems.items.Remove(_listOfItems.items[0]);
                }
            }
        }
        
        
    }
}
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UI.InventoryScripts;

namespace DefaultNamespace.TestTools
{
    public class TestToolButtonUI : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI _currentCountItem;
        [SerializeField] private TextMeshProUGUI _countItemChange;
        [SerializeField] private Button _addItemButton;
        [SerializeField] private Button _deleteItemButton;
        [SerializeField] private Inventory _inventory;

        private void Start()
        {
            _inventory = FindObjectOfType<Inventory>(true);
        }

        public void ButtonFilling(ItemSO item)
        {
            // _image.sprite = item._img
            Texture2D texture = LoadTextureFromFile(item._img);
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);

            // Установить спрайт в компонент Image
            _image.sprite = sprite;
            _currentCountItem.text = _inventory._data._items.Find(x => x.Name == item._name).Count.ToString();
        }
        
        private Texture2D LoadTextureFromFile(string filePath)
        {
            // Загрузить текстуру из файла
            Texture2D texture = new Texture2D(2, 2); // Здесь вы можете указать размер текстуры

            if (System.IO.File.Exists(filePath))
            {
                byte[] data = System.IO.File.ReadAllBytes(filePath);
                if (texture.LoadImage(data))
                {
                    return texture;
                }
            }
            return null;
        }
    }
}
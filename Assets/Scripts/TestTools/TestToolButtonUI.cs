using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UI.InventoryScripts;
using Item = PixelCrushers.DialogueSystem.ChatMapper.Item;

namespace DefaultNamespace.TestTools
{
    public class TestToolButtonUI : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI _currentCountItem;
        [SerializeField] public Inventory _inventory;
        [SerializeField] private TMP_InputField _countItemChange;
        private ItemSO _itemSO;

        private void Start()
        {
            _inventory = FindObjectOfType<Inventory>(true);
        }

        private void Update()
        {
            _currentCountItem.text = ((int)_inventory.GetItemAmount(_itemSO._name)).ToString();
        }

        public void ButtonFilling(ItemSO item)
        {
            _itemSO = item;
            string originalPath = item._img;
            string unityPath = originalPath.Replace("\\", "/");
            string unityPath1 = unityPath.Replace("//", "/");
            
            // Загрузить текстуру из файла на основе пути к файлу
            // Texture2D texture = LoadTextureFromFile(item._img);
            Texture2D texture = LoadTextureFromFile("Assets/" + unityPath1);

            // Проверить, удалась ли загрузка текстуры
            if (texture != null)
            {
                // Создать спрайт из текстуры
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);

                // Установить спрайт в компонент Image
                _image.sprite = sprite;

                // Установить текст текущего количества предмета
                // int a = (int)_inventory.GetItemAmount(item._name);
                // _currentCountItem.text = a.ToString();
                _currentCountItem.text = ((int)_inventory.GetItemAmount(item._name)).ToString();
            }
            else
            {
                // Если не удалось загрузить текстуру, можно сделать обработку ошибки
                Debug.LogError("Не удалось загрузить текстуру из файла: " + item._img);
            }
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

        public void AddButton()
        {
            try
            {
                double result = Convert.ToDouble(_countItemChange.text);
                Debug.Log("Успешно сконвертировано в double: " + result);
                _inventory.DialogueSystemItemAdder(_itemSO._name, result);
            }
            catch (FormatException)
            {
                Debug.LogError("Невозможно сконвертировать в double: " + _countItemChange);
            }
        }
        
        public void DeleteButton()
        {
            try
            {
                double result = Convert.ToDouble(_countItemChange.text);
                Debug.Log("Успешно сконвертировано в double: " + result);
                _inventory.DialogueSystemItemDeleter(_itemSO._name, result);
            }
            catch (FormatException)
            {
                Debug.LogError("Невозможно сконвертировать в double: " + _countItemChange);
            }
        }
    }
}
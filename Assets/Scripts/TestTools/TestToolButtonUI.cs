using System;
using DefaultNamespace;
using ScriptableObjects;
using TMPro;
using UI.InventoryScripts;
using UnityEngine;
using UnityEngine.UI;

// ...

public class TestToolButtonUI : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _currentCountItem;
    [SerializeField] public Inventory _inventory;
    [SerializeField] private TMP_InputField _countItemChange;
    private ItemSO _itemSO;

    private void Start()
    {
        _inventory = FindAnyObjectByType<Inventory>(FindObjectsInactive.Include);
    }

    private void Update()
    {
        _currentCountItem.text = ((int)_inventory.GetItemAmount(_itemSO._name)).ToString();
    }

    public void ButtonFilling(ItemSO item)
    {
        _itemSO = item;

        //Получить спрайт из Image
        // Sprite sprite = _image.sprite;
        Sprite sprite = Resources.Load<Sprite>(item._img);

        // Проверить, не является ли спрайт null
        if (sprite != null)
        {
            // Получить текстуру из спрайта
            Texture2D texture = sprite.texture;

            // Создать новый спрайт из текстуры
            Sprite newSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);

            // Установить новый спрайт в Image
            _image.sprite = newSprite;

            // Установить текст текущего количества предмета
            _currentCountItem.text = ((int)_inventory.GetItemAmount(item._name)).ToString();
        }
        else
        {
            Debug.LogError("Спрайт в Image отсутствует.");
        }



        // _image.sprite = Resources.Load<Sprite>(item._img);
        // _currentCountItem.text = ((int)_inventory.GetItemAmount(item._name)).ToString();
        
        

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

    // ...
}

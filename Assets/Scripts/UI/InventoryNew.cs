// using System;
// using System.Collections.Generic;
// using TMPro;
// using UI.InventoryScripts;
// using UnityEngine;
// using UnityEngine.UI;
//
// namespace UI
// {
//     public class InventoryNew : MonoBehaviour
//     {
//         [SerializeField] private AllItemCollectionsSO _itemCollection;
//         [SerializeField] private List<ItemInventory> _itemInventory = new();
//         [SerializeField] private Transform _inventoryList;
//         [SerializeField] private GameObject _itemPrefab;
//         [SerializeField] private float _maxWidth = 100f;
//         [SerializeField] private float _maxHeight = 100f;
//         [SerializeField] private TextMeshProUGUI _itemNameText;
//         [SerializeField] private TextMeshProUGUI _itemDescriptionText;
//         [SerializeField] private Image _itemImage;
//         [SerializeField] private GameObject _deleteButtonPrefab;
//         [SerializeField] private Transform _deleteButtonTransform;
//     
//         private void Start()
//         {
//             UpdateInventory();
//         }
//
//         public void UpdateInventory()
//         {
//             foreach (Transform child in _inventoryList)
//             {
//                 Destroy(child.gameObject);
//             }
//
//             foreach (ItemInventory item in _itemInventory)
//             {
//                 GameObject newItemObject = Instantiate(_itemPrefab, _inventoryList);
//                 newItemObject.name = item._name;
//
//                 Image itemImage = newItemObject.GetComponentInChildren<Image>();
//                 if (itemImage != null)
//                 {
//                     itemImage.sprite = item._icon;
//                     float width = Mathf.Min(item._icon.rect.width * 3f, _maxWidth);
//                     float height = Mathf.Min(item._icon.rect.height * 3f, _maxHeight);
//                     itemImage.rectTransform.sizeDelta = new Vector2(width, height);
//                 }
//
//                 Button itemButton = newItemObject.GetComponentInChildren<Button>();
//                 if (itemButton != null)
//                 {
//                     itemButton.onClick.AddListener(() => OnItemClick(item));
//                 }
//
//                 if (item._count > 1f)
//                 {
//                     Transform itemObject = _inventoryList.Find(item._name);
//                     if (itemObject != null)
//                     {
//                         TextMeshProUGUI quantityText = itemObject.GetComponentInChildren<TextMeshProUGUI>();
//                         if (quantityText != null)
//                         {
//                             quantityText.text = item._count.ToString();
//                         }
//                     }
//                 }
//             }
//         }
//
//         public void AddItem(string itemName)
//         {
//             var obj = _itemCollection.itemCollection.Find(x => x.Name == itemName);
//             ItemInventory existingItem = _itemInventory.Find(item => item._name == itemName);
//     
//             if (existingItem != null)
//             {
//                 existingItem._count += obj.Count;
//                 UpdateItemText(existingItem);
//             }
//             else
//             {
//                 ItemInventory newItem = new ItemInventory();
//                 newItem._icon = obj.Icon;
//                 newItem._name = obj.Name;
//                 newItem._count = 1;
//                 newItem._description = obj.Description;
//                 newItem._healthIncrease = obj.HealthIncrease;
//                 newItem._damageIncrease = obj.DamageIncrease;
//         
//                 _itemInventory.Add(newItem);
//
//                 GameObject newItemObject = Instantiate(_itemPrefab, _inventoryList);
//                 newItemObject.name = obj.Name;
//                 Image itemImage = newItemObject.GetComponentInChildren<Image>();
//
//                 if (itemImage != null)
//                 {
//                     itemImage.sprite = obj.Icon;
//                     float width = Mathf.Min(obj.Icon.rect.width * 3f, _maxWidth);
//                     float height = Mathf.Min(obj.Icon.rect.height * 3f, _maxHeight);
//                     itemImage.rectTransform.sizeDelta = new Vector2(width, height);
//                 }
//
//                 Button itemButton = newItemObject.GetComponentInChildren<Button>();
//                 if (itemButton != null)
//                 {
//                     itemButton.onClick.AddListener(() => OnItemClick(newItem));
//                 }
//                 UpdateItemText(newItem);
//             }
//         }
//
//         private void OnItemClick(ItemInventory item)
//         {
//             _itemNameText.text = item._name;
//             _itemDescriptionText.text = item._description;
//             _itemImage.sprite = item._icon;
//             var obj = Instantiate(_deleteButtonPrefab, transform.position, Quaternion.identity, _deleteButtonTransform);
//             obj.GetComponent<Button>().onClick.AddListener(() => DeleteItem(item._name));
//         }
//
//         public void DeleteItem(string itemName)
//         {
//             ItemInventory inventory = _itemInventory.Find(item => item._name == itemName);
//             _itemInventory.Remove(inventory);
//             _itemNameText.text = "";
//             _itemDescriptionText.text = "";
//             _itemImage.sprite = null;
//             UpdateInventory();
//         }
//
//         private void UpdateItemText(ItemInventory item)
//         {
//             ItemInventory itemToUpdate = _itemInventory.Find(i => i._name == item._name);
//             if (itemToUpdate != null)
//             {
//                 Transform itemObject = _inventoryList.Find(itemToUpdate._name);
//                 if (itemObject != null)
//                 {
//                     TextMeshProUGUI quantityText = itemObject.GetComponentInChildren<TextMeshProUGUI>();
//                     if (quantityText != null)
//                     {
//                         quantityText.text = itemToUpdate._count.ToString();
//                     }
//                 }
//             }
//         }
//     
//         public InventoryData GetInventoryDate()
//         {
//             return new InventoryData
//             {
//                 ItemInventory = _itemInventory,
//             };
//         }
//     
//         public void SetInventoryDate(InventoryData data)
//         {
//             _itemInventory = data.ItemInventory;
//         }
//     }
//
//     [Serializable]
//     public class ItemInventory
//     {
//         public Sprite _icon;
//         public string _name;
//         public int _count;
//         public string _description;
//     }
// }
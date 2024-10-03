using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ActiveItemUI : MonoBehaviour
{

    ItemData item;

    [SerializeField] Image image;

    [Space]
    [SerializeField] TextMeshProUGUI itemName;
    [SerializeField] TextMeshProUGUI description;

    [Space]
    [SerializeField] Button useButton;
    [SerializeField] Button deleteButton;


    public static event Action<ItemData> OnUseItem;
    public static event Action<ItemData> OnDeleteItem;

    Sprite defaultImage;


    private void Awake()
    {
        useButton.onClick.AddListener(UseItem);
        deleteButton.onClick.AddListener(DeleteItem);

        useButton.interactable = false;
        deleteButton.interactable = false;

        InventoryUI.OnSetActive += SetItem;
        InventoryUI.OnClear += ClearItem;

        defaultImage = image.sprite;
    }


    private void OnDestroy()
    {
        useButton.onClick.RemoveListener(UseItem);
        deleteButton.onClick.RemoveListener(DeleteItem);

        InventoryUI.OnSetActive -= SetItem;
        InventoryUI.OnClear -= ClearItem;
    }


    private void SetItem(ItemData item)
    {
        this.item = item;

        image.sprite = item.Image;

        itemName.text = item.ItemName;
        description.text = item.Description;


        useButton.interactable = item.Usable;

        deleteButton.interactable = item.CanDelete;
    }


    private void ClearItem()
    {
        item = null;

        image.sprite = defaultImage;

        itemName.text = String.Empty;
        description.text= String.Empty;

        useButton.interactable= false;
        deleteButton.interactable = false;
    }


    private void UseItem() => OnUseItem?.Invoke(item);

    private void DeleteItem() => OnDeleteItem?.Invoke(item);

}

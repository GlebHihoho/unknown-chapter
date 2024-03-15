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


    private void Awake()
    {
        useButton.onClick.AddListener(UseItem);
        deleteButton.onClick.AddListener(DeleteItem);

        InventoryUI.OnSetActive += SetItem;
    }


    private void OnDestroy()
    {
        useButton.onClick.RemoveListener(UseItem);
        deleteButton.onClick.RemoveListener(DeleteItem);

        InventoryUI.OnSetActive -= SetItem;
    }


    private void SetItem(ItemData item)
    {
        this.item = item;

        image.sprite = item.Image;

        itemName.text = item.ItemName;
        description.text = item.Description;
    }


    private void UseItem()
    {

    }

    private void DeleteItem()
    {

    }

}

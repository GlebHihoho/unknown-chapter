using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Button))]
public class InventoryCell : MonoBehaviour
{

    ItemData item;

    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI numberLabel;

    [SerializeField] Image usable;


    [Space]
    [SerializeField] Image background;
    
    [SerializeField] Color selectedColour = Color.blue;


    Button select;

    InventoryUI inventoryUI;

    private void Awake()
    {
        select = GetComponent<Button>();
        select.onClick.AddListener(Select);
    }

    private void OnDestroy() => select.onClick.RemoveListener(Select);


    public void SetItem(ItemData item, InventoryUI inventoryUI)
    {
        this.item = item;
        this.inventoryUI = inventoryUI;

        icon.sprite = item.Icon;

        numberLabel.text = "0";
        numberLabel.enabled = false;

        usable.enabled = item.Usable;

        background.enabled = false;
    }


    public void UpdateItem(int quantity)
    {
        numberLabel.enabled = quantity > 1;
        numberLabel.text = quantity.ToString();
    }

    public void DeleteItem() => Destroy(gameObject);

    private void Select() => inventoryUI.SetActiveItem(item);


    public void SelectItem()
    {
        background.enabled = true;
        background.color = selectedColour;
    }


    public void DeselectItem()
    {
        background.enabled = false;
       // background.color = nonSelectedColour;
    }
}

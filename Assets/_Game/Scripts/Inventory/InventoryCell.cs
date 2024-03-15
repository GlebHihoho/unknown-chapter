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


    [Space]
    [SerializeField] Image background;
    
    [SerializeField] Color selectedColour = Color.blue;
    [SerializeField] Color nonSelectedColour = Color.black;

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
    }


    public void UpdateItem(int quantity) => numberLabel.text = quantity.ToString();

    public void DeleteItem() => Destroy(gameObject);

    private void Select() => inventoryUI.SetActiveItem(item);


    public void SelectItem()
    {      
        background.color = selectedColour;
    }


    public void DeselectItem()
    {
        background.color = nonSelectedColour;
    }
}

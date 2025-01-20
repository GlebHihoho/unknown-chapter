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
    [SerializeField] Image newItem;

    [SerializeField] Image border;
    [SerializeField] Color emptyCellBorder;
    [SerializeField] Color usedCellBorder;
    [SerializeField] Color selectedCellBorder;


    [Space]
    [SerializeField] Image background;
    
    [SerializeField] Color selectedColour = Color.blue;
    [SerializeField] Color defaultColor = Color.gray;


    Button select;
    ButtonCursor buttonCursor;

    InventoryUI inventoryUI;

    private void Awake()
    {
        select = GetComponent<Button>();
        buttonCursor = GetComponent<ButtonCursor>();
        
    }

    private void OnDestroy() => select.onClick.RemoveListener(Select);


    public void SetItem(ItemData item, InventoryUI inventoryUI)
    {
        select.enabled = true;
        select.onClick.AddListener(Select);

        buttonCursor.enabled = true;
        icon.gameObject.SetActive(true);

        this.item = item;
        this.inventoryUI = inventoryUI;

        icon.sprite = item.Icon;

        numberLabel.text = "0";
        numberLabel.enabled = false;

        usable.enabled = item.Usable;

        background.gameObject.SetActive(true);
        background.color = defaultColor;
        newItem.enabled = true;

        border.color = usedCellBorder;
    }


    public void UpdateItem(int quantity)
    {
        numberLabel.enabled = quantity > 1;
        numberLabel.text = quantity.ToString();
    }

    public void ClearCell()
    {
        background.gameObject.SetActive(false);
        item = null;
        select.onClick.RemoveListener(Select);

        select.enabled = false;
        buttonCursor.enabled = false;
        icon.gameObject.SetActive(false);
        usable.enabled = false;
        newItem.enabled = false;

        border.color = emptyCellBorder;
    }

    private void Select() => inventoryUI.SetActiveItem(item);


    public void SelectItem()
    {
        background.color = selectedColour;
        newItem.enabled = false;

        border.color = selectedCellBorder;
    }


    public void DeselectItem()
    {
        background.color = defaultColor;
        border.color = usedCellBorder;
    }
}

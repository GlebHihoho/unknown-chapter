using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{


    [SerializeField] InventoryCell cellPrefab;
    [SerializeField] Transform itemsPanel;

    Dictionary<ItemData, InventoryCell> inventory = new Dictionary<ItemData, InventoryCell>();

    ItemData activeItem;

    public static event Action<ItemData> OnSetActive;

    // Start is called before the first frame update
    void Start()
    {
        InventoryManager.OnItemAdded += AddItem;
        InventoryManager.OnItemRemoved += RemoveItem;
        InventoryManager.OnQuantityChanged += UpdateItem;

        gameObject.SetActive(false);
    }


    private void OnDestroy()
    {
        InventoryManager.OnItemAdded -= AddItem;
        InventoryManager.OnItemRemoved -= RemoveItem;
        InventoryManager.OnQuantityChanged -= UpdateItem;
    }


    private void AddItem(ItemData item)
    {

        InventoryCell cell = Instantiate(cellPrefab, itemsPanel);
        cell.SetItem(item, this);

        inventory.Add(item, cell);

        if (activeItem == null) SetActiveItem(item);
    }

    private void RemoveItem(ItemData item)
    {
        inventory[item].DeleteItem();

        inventory.Remove(item);

        if (activeItem == item && inventory.Count > 0) SetActiveItem(inventory.Keys.First());       
    }


    private void UpdateItem(ItemData item, int quantity)
    {
        inventory[item].UpdateItem(quantity);
    }


    public void SetActiveItem(ItemData item)
    {
        if (inventory.ContainsKey(item))
        {

            if (activeItem != null && inventory.ContainsKey(activeItem)) inventory[activeItem].DeselectItem();

            OnSetActive?.Invoke(item);
            activeItem = item;

            inventory[item].SelectItem();
        }
        else activeItem = null;
    }


}

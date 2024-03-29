using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class InventoryManager : MonoBehaviour, ISaveable
{

    private Dictionary<ItemData, int> items = new Dictionary<ItemData, int>();

    private Dictionary<string, ItemData> dialogToItem = new Dictionary<string, ItemData>();

    [SerializeField] AllItems allItemTypes;


    public static event Action<ItemData> OnItemAdded;
    public static event Action<ItemData, int> OnQuantityChanged;
    public static event Action<ItemData, int> OnQuantityDifference;
    public static event Action<ItemData> OnItemRemoved;



    private void Awake()
    {
        foreach (ItemData item in allItemTypes.Items)
        {
            if (item.DialogVariable != string.Empty) dialogToItem.Add(item.DialogVariable, item);
        }
    }


    private void OnEnable()
    {
        Collectible.OnItemGiven += IncreaseQuantity;

        ActiveItemUI.OnUseItem += UseItem;
        ActiveItemUI.OnDeleteItem += DeleteItem;

        Lua.RegisterFunction("UpdateItem", this, SymbolExtensions.GetMethodInfo(() => UpdateItem(string.Empty)));
    }

    private void OnDisable()
    {
        Collectible.OnItemGiven -= IncreaseQuantity;

        ActiveItemUI.OnUseItem -= UseItem;
        ActiveItemUI.OnDeleteItem -= DeleteItem;

        Lua.UnregisterFunction("UpdateItem");
    }


    private void IncreaseQuantity(ItemData item, int amount) => NotifyDialog(item.DialogVariable, SetQuantity(item, amount));


    private int SetQuantity(ItemData item, int amount, bool increment = true)
    {

        if (!items.ContainsKey(item))
        {
            items.Add(item, 0);
            OnItemAdded?.Invoke(item);
        }

        int oldValue = items[item];

        if (increment) amount = items[item] + amount;

        int newValue = Mathf.Clamp(amount, 0, item.InventoryMax);
        items[item] = newValue;

        OnQuantityChanged?.Invoke(item, newValue);
        OnQuantityDifference?.Invoke(item, newValue - oldValue);

        if (items[item] <= 0)
        {
            items.Remove(item);
            OnItemRemoved?.Invoke(item);
        }

        return newValue;
    }


    public void UpdateItem(string itemID)
    {
        if (dialogToItem.ContainsKey(itemID))
        {
            ItemData item = dialogToItem[itemID];
            SetQuantity(item, DialogueLua.GetVariable(itemID).asInt, false);
        }      
    }


    private void NotifyDialog(string dialogVariable, object value)
    {
        if (DialogueManager.Instance != null && DialogueManager.DatabaseManager != null && DialogueManager.MasterDatabase != null)
        {
            DialogueLua.SetVariable(dialogVariable, value);
            DialogueManager.SendUpdateTracker();
        }
    }


    private void UseItem(ItemData item)
    {
        Debug.Log("Using item: " + item.name + " \""+ item.ItemName+"\"");
    }

    private void DeleteItem(ItemData item)
    {
        NotifyDialog(item.DialogVariable, SetQuantity(item, 0, false));
    }


    public void Save(ref SaveData.Save save)
    {
        foreach (ItemData  key in items.Keys)
        {
            SaveData.InventoryItem item;

            item.itemData = key;
            item.quantity = items[key];

            save.inventory.Add(item);
        }
    }

    public void Load(SaveData.Save save)
    {

        foreach (ItemData item in items.Keys)
        {
            OnItemRemoved?.Invoke(item);
        }

        items.Clear();

        foreach (SaveData.InventoryItem item in save.inventory)
        {
            items.Add(item.itemData, item.quantity);
            OnItemAdded?.Invoke(item.itemData);
            OnQuantityChanged?.Invoke(item.itemData, item.quantity);
        }
    }
}

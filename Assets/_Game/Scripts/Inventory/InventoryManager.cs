using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class InventoryManager : MonoBehaviour
{

    private Dictionary<ItemData, int> items = new Dictionary<ItemData, int>();

    private Dictionary<string, ItemData> dialogToItem = new Dictionary<string, ItemData>();

    [SerializeField] AllItems allItemTypes;


    public static event Action<ItemData> OnItemAdded;
    public static event Action<ItemData, int> OnQuantityChanged;
    public static event Action<ItemData> OnItemRemoved;


    private void Awake()
    {

        foreach (ItemData item in allItemTypes.Items)
        {
            if (item.DialogVariable != string.Empty) dialogToItem.Add(item.DialogVariable, item);
        }

    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnEnable()
    {
        Collectible.OnItemGiven += AddItem;
        Lua.RegisterFunction("UpdateItem", this, SymbolExtensions.GetMethodInfo(() => UpdateItem(string.Empty)));
    }

    private void OnDisable()
    {
        Collectible.OnItemGiven -= AddItem;
        Lua.UnregisterFunction("UpdateItem");
    }


    public void AddItem(ItemData item, int amount = 1)
    {

  
        if (!items.ContainsKey(item))
        {
            items.Add(item, 0);
            OnItemAdded?.Invoke(item);
        }

        int newValue = Mathf.Clamp(items[item] + amount, 0, item.InventoryMax);
        items[item] = newValue;

        OnQuantityChanged?.Invoke(item, newValue);

        NotifyDialog(item.DialogVariable, newValue);
    }


    public void RemoveItem(ItemData item, int amount = 1)
    {

        if (items.ContainsKey(item))
        {
            int newValue = Mathf.Clamp(items[item] - amount, 0, item.InventoryMax);
            items[item] = newValue;

            OnQuantityChanged?.Invoke(item, newValue);

            if (items[item] <= 0)
            {
                items.Remove(item);
                OnItemRemoved?.Invoke(item);
            }

            NotifyDialog(item.DialogVariable, newValue);
        }
    }


    public void UpdateItem(string itemID)
    {

        if (dialogToItem.ContainsKey(itemID))
        {

            ItemData item = dialogToItem[itemID];

            if (!items.ContainsKey(item)) items.Add(item, 0);

            int newValue = DialogueLua.GetVariable(itemID).asInt;
            newValue = Mathf.Clamp(newValue, 0, item.InventoryMax);

            items[item] = newValue;

            OnQuantityChanged?.Invoke(item, newValue);

            if (items[item] <= 0)
            {
                items.Remove(item);
                OnItemRemoved?.Invoke(item);
            }
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


}

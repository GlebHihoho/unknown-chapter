using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PixelCrushers.DialogueSystem;

public class InventoryUI : MonoBehaviour
{


    [SerializeField] InventoryCell cellPrefab;
    [SerializeField] Transform itemsPanel;

    [SerializeField] ButtonUpdated inventoryButton;

    [SerializeField] ItemSoundData itemSounds;

    Dictionary<ItemData, InventoryCell> inventory = new Dictionary<ItemData, InventoryCell>();

    [SerializeField, Min(1)] int initialInventorySize = 48;
    List<InventoryCell> avialableCells = new List<InventoryCell>();

    [SerializeField, Min(1)] int extraInventorySize = 24;

    ItemData activeItem;

    public static event Action<ItemData> OnSetActive;
    public static event Action OnClear;

    bool activeWhileConversation = false;

    // Start is called before the first frame update
    void Start()
    {
        InventoryManager.OnItemAdded += AddItem;
        InventoryManager.OnItemRemoved += RemoveItem;
        InventoryManager.OnQuantityChanged += UpdateItem;

        DialogueManager.instance.conversationStarted += ConversationStarted;
        DialogueManager.instance.conversationEnded += ConversationEnded;

        AddCells(initialInventorySize);

        gameObject.SetActive(false);
    }



    private void OnDestroy()
    {
        InventoryManager.OnItemAdded -= AddItem;
        InventoryManager.OnItemRemoved -= RemoveItem;
        InventoryManager.OnQuantityChanged -= UpdateItem;

        if (DialogueManager.instance != null)
        {
            DialogueManager.instance.conversationStarted -= ConversationStarted;
            DialogueManager.instance.conversationEnded -= ConversationEnded;
        }
    }


    private void OnEnable() => inventoryButton.ResetUpdate();


    private void ConversationStarted(Transform t)
    {
        if (gameObject.activeSelf)
        {
            activeWhileConversation = true;
            gameObject.SetActive(false);
        }
    }


    private void ConversationEnded(Transform t)
    {
        if (activeWhileConversation)
        {
            gameObject.SetActive(true);
            activeWhileConversation = false;
        }
    }



    private void AddItem(ItemData item)
    {

        if (avialableCells.Count == 0) AddCells(extraInventorySize);

        if (avialableCells.Count > 0)
        {

            InventoryCell cell = avialableCells[0];
            avialableCells.RemoveAt(0);

            cell.SetItem(item, this);

            inventory.Add(item, cell);

            if (activeItem == null) SetActiveItem(item, true);

            if (!activeWhileConversation && !gameObject.activeSelf) inventoryButton.ShowUpdate();

            SoundManager.instance.PlayEffect(itemSounds.Receieved);
        }
    }

    private void RemoveItem(ItemData item)
    {
        inventory[item].ClearCell();

        avialableCells.Insert(0,inventory[item]);
        inventory.Remove(item);

        int i = 0;
        foreach (ItemData key in inventory.Keys)
        {
            inventory[key].transform.SetSiblingIndex(i);
            i++;
        }


        if (activeItem == item)
        {
            if (inventory.Count > 0) SetActiveItem(inventory.Keys.First(), true);
            else 
            { 
                activeItem = null;
                OnClear?.Invoke();
            }
        }

        SoundManager.instance.PlayEffect(itemSounds.Deleted);
    }


    private void UpdateItem(ItemData item, int quantity)
    {
        inventory[item].UpdateItem(quantity);

        if (!activeWhileConversation && !gameObject.activeSelf) inventoryButton.ShowUpdate();

    }

    public void SetActiveItem(ItemData item, bool addingOrRemoving = false)
    {
        if (inventory.ContainsKey(item))
        {

            if (activeItem != null && inventory.ContainsKey(activeItem)) inventory[activeItem].DeselectItem();

            OnSetActive?.Invoke(item);
            activeItem = item;

            inventory[item].SelectItem();
        }
        else activeItem = null;

        if (!addingOrRemoving) SoundManager.instance.PlayEffect(itemSounds.Selected);
    }


    private void AddCells(int cellsCount)
    {
        for (int i = 0; i < cellsCount; i++)
        {
            InventoryCell cell = Instantiate(cellPrefab, itemsPanel);
            cell.ClearCell();
            avialableCells.Add(cell);
        }
    }


}

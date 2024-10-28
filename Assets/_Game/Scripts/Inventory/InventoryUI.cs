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
        InventoryCell cell = Instantiate(cellPrefab, itemsPanel);
        cell.SetItem(item, this);

        inventory.Add(item, cell);

        if (activeItem == null) SetActiveItem(item, true);

        if (!activeWhileConversation && !gameObject.activeSelf) inventoryButton.ShowUpdate();

        SoundManager.instance.PlayEffect(itemSounds.Receieved);
    }

    private void RemoveItem(ItemData item)
    {
        inventory[item].DeleteItem();

        inventory.Remove(item);

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


}

using NUnit.Framework;
using System;
using UnityEngine;
using UnityEngine.Events;

public class ItemsContainer : Interactable
{

    [SerializeField] ItemData[] items;

    [SerializeField] GameObject fullContainer;
    [SerializeField] GameObject emptyContainer;

    //public static Action<ItemData[], Action<bool>> OnOpenContainer;
    public static event Action<ItemData[], Action> OnOpenContainer;
    public static event Action<ItemData, int> OnItemGiven;

     public UnityEvent OnItemsGiven;


    protected override void PerfomInteraction()
    {
        base.PerfomInteraction();

    //    OnOpenContainer?.Invoke(items);
        OnOpenContainer?.Invoke(items, TakingItems);

    }


    private void TakingItems()
    {

        foreach (ItemData item in items) 
        {
            OnItemGiven?.Invoke(item, 1);
        }

        OnItemsGiven.Invoke();

        ToggleContainer(false);
    }


    public void ToggleContainer(bool isEnabled)
    {
        fullContainer.SetActive(isEnabled);
        if (emptyContainer != null) emptyContainer.SetActive(!isEnabled);

        enabled = isEnabled;
        GetComponent<Collider>().enabled = isEnabled;
    }
}

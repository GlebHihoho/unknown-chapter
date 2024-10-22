using NUnit.Framework;
using System;
using UnityEngine;

public class ItemsContainer : Interactable
{

    [SerializeField] ItemData[] items;

    [SerializeField] GameObject fullContainer;
    [SerializeField] GameObject emptyContainer;

    //public static Action<ItemData[], Action<bool>> OnOpenContainer;
    public static event Action<ItemData[], Action> OnOpenContainer;
    public static event Action<ItemData, int> OnItemGiven;


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

        fullContainer.SetActive(false);
        if (emptyContainer != null) emptyContainer.SetActive(true);

        Destroy(this);
    }
}

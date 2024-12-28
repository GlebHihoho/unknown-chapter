using NUnit.Framework;
using System;
using System.Linq;
using System.Text;
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

        StringBuilder sb = new StringBuilder();

        if (items.Length == 1) sb.Append("Получен предмет: ");
        else sb.Append("Получены предметы: ");


        for (int i = 0; i < items.Length; i++)
        {
            sb.Append(items[i].ItemName);
            if (i < items.Length - 1) sb.Append(", ");

            OnItemGiven?.Invoke(items[i], 1);
        }

        OnItemsGiven.Invoke();
        UIMessage.instance.ShowMessage(sb.ToString());

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

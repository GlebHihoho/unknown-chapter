using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;
using System.Dynamic;

[RequireComponent(typeof(Outline))]
public class Collectible : Interactable
{

    [SerializeField] ItemData itemData;


    [Space]
    [SerializeField] int increment = 1;

 
    public static event Action<ItemData, int> OnItemGiven;


    protected override void PerfomInteraction()
    {
        base.PerfomInteraction();

        OnItemGiven?.Invoke(itemData, increment);
        UIMessage.instance.ShowMessage($"Получен предмет: {itemData.ItemName}") ;

        ResetVisuals();
        gameObject.SetActive(false);
    }


}

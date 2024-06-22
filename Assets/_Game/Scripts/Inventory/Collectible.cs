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

        ResetVisuals();
        gameObject.SetActive(false);
    }

    protected override void PerfomInspection()
    {
        base.PerfomInspection();

        DialogueManager.BarkString(itemData.ExamineBark, transform);
    }



}

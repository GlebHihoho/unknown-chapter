using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

[CreateAssetMenu(fileName ="ItemConfig", menuName ="Gameplay/Create Item Config")]
public class ItemData : ScriptableObject
{

    [VariablePopup(true)]
    [SerializeField] string dialogVariable = string.Empty;
    public string DialogVariable => dialogVariable;

    [Space]

    [SerializeField] Sprite icon;
    public Sprite Icon => icon;

    [SerializeField] Sprite image;
    public Sprite Image => image;

    [Space]

    [SerializeField, TextArea(0, 3)] string examineBark;
    public string ExamineBark => examineBark;

    [SerializeField, TextArea(0, 10)] string description;
    public string Description => description;

    [Space]

    [SerializeField, Min(1)] int inventoryMax = 100;
    public int InventoryMax => inventoryMax; 


    [SerializeField, Min(0)] float interactDistace = 2f;
    public float InteractDistance => interactDistace;






}

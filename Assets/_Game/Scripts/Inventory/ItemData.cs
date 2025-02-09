using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;
using System.Runtime.InteropServices.WindowsRuntime;

[CreateAssetMenu(fileName ="ItemConfig", menuName = "Gameplay/Interactable/ItemData")]
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

    [SerializeField] string itemName;
    public string ItemName => itemName;

    [SerializeField, TextArea(0, 3)] string examineBark;
    public string ExamineBark => examineBark;

    [SerializeField, TextArea(0, 10)] string description;
    public string Description => description;

    [Space]

    [SerializeField, Min(1)] int inventoryMax = 100;
    public int InventoryMax => inventoryMax; 

    [SerializeField, Min(0)] float interactDistace = 2f;
    public float InteractDistance => interactDistace;

    [SerializeField] bool canDelete = true;
    public bool CanDelete => canDelete;

    [Header("Modifiers")]

    [SerializeField, Range(-5, 5)] int physicalModifier = 0;
    public int PhysicalModifier => physicalModifier;

    [SerializeField, Range(-5, 5)] int perceptionModifier = 0;
    public int PerceptionlModifier => perceptionModifier;

    [SerializeField, Range(-5, 5)] int intellectModifier = 0;
    public int IntellectModifier => intellectModifier;

    [Header("Interactions")]
    [SerializeField] string examine = string.Empty;
    public string Examine => examine;


    public bool Usable
    {
        get
        {
            return examine != string.Empty;
        }
    }








}

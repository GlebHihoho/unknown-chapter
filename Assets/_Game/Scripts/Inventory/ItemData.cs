using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

[CreateAssetMenu(fileName ="ItemConfig", menuName ="Gameplay/Create Item Config")]
public class ItemData : ScriptableObject
{

    [VariablePopup(true)]
    [SerializeField] string _dialogVariable = string.Empty;
    public string dialogVariable => _dialogVariable;

    [Space]

    [SerializeField] Sprite _icon;
    public Sprite icon => _icon;

    [SerializeField] Sprite _image;
    public Sprite image => _image;

    [Space]

    [SerializeField] string _examineBark;
    public string examineBark => _examineBark;

    [SerializeField, TextArea(0, 10)] string _description;
    public string description => _description;

    [Space]

    [SerializeField, Min(1)] int _inventoryMax = 100;
    public int inventoryMax => _inventoryMax; 


    [SerializeField, Min(0)] float _interactDistace = 2f;
    public float interactDistance => _interactDistace;






}

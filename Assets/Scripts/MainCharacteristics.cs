using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;
using UI.InventoryScripts;
using Unity.VisualScripting;
using Update = UnityEngine.PlayerLoop.Update;

public class MainCharacteristics : MonoBehaviour
{
    [SerializeField] private double _physicalAbilities = 0; // физические способности
    [SerializeField] private double _perception = 0;        // восприятие
    [SerializeField] private double _intellect  = 0;        // интеллект 
    [SerializeField] private Inventory _inventory;


    public double GetSkill(string skillName)
    {
        switch (skillName)
        {
            case "PhysicalAbilities": return _physicalAbilities;
            case "Perception": return _perception;
            case "Intellect": return _intellect;
            default:
                Debug.Log($"GetSkill('{skillName}') is not a valid skill name.");
                return 0;
        }
    }

    private void OnEnable()
    {
        Lua.RegisterFunction("GetSkill", this, SymbolExtensions.GetMethodInfo(() => GetSkill("")));
        Lua.RegisterFunction("GetItemAmount", _inventory, SymbolExtensions.GetMethodInfo(() => _inventory.GetItemAmount(string.Empty)));
        Lua.RegisterFunction("DialogueSystemItemDeleter", _inventory, SymbolExtensions.GetMethodInfo(() => _inventory.DialogueSystemItemDeleter(string.Empty, 1)));
        Lua.RegisterFunction("DialogueSystemItemAdder", _inventory, SymbolExtensions.GetMethodInfo(() => _inventory.DialogueSystemItemAdder(string.Empty, 1)));
    }

    private void OnDisable()
    {
        Lua.UnregisterFunction("GetSkill");
        Lua.UnregisterFunction("GetItemAmount");
        Lua.UnregisterFunction("DialogueSystemItemDeleter");
        Lua.UnregisterFunction("DialogueSystemItemAdder");
    }

    
    public double GetPhysicalAbilities(double value)
    {
        return _physicalAbilities;
    }
    
    public double GetPerception(double value)
    {
        return _perception;
    }
    
    public double GetIntellect(double value)
    {
        return _intellect;
    }
}

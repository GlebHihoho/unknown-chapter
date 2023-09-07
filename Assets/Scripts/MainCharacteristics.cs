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
    private Inventory _inventory;


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
        
        
        // Lua.RegisterFunction("PhysicalAbilities", this, SymbolExtensions.GetMethodInfo(() => GetPhysicalAbilities(1)));
        // Lua.RegisterFunction("Perception", this, SymbolExtensions.GetMethodInfo(() => GetPerception(1)));
        // Lua.RegisterFunction("Intellect", this, SymbolExtensions.GetMethodInfo(() => GetIntellect(1)));
        Lua.RegisterFunction("GetItemAmount", this, SymbolExtensions.GetMethodInfo(() => _inventory.GetItemAmount(string.Empty)));
    }

    private void OnDisable()
    {
        Lua.UnregisterFunction("GetSkill");
        // Lua.UnregisterFunction("PhysicalAbilities");
        // Lua.UnregisterFunction("Perception");
        // Lua.UnregisterFunction("Intellect");
        Lua.UnregisterFunction("GetItemAmount");
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
    
    // private void SetPhysicalAbilities(double value)
    // {
    //     _physicalAbilities = value;
    // }
    //
    
    
    //
    // private void SetPerception(double value)
    // {
    //     _perception = value;
    // }
    
    
    //
    // public void SetIntellect(double value)
    // {
    //     _intellect = value;
    // }

    // public void AddCharacteristic(string nameCharacteristic)
    // {
    //     switch (nameCharacteristic)
    //     {
    //         case "_physicalAbilities":
    //             _physicalAbilities++;
    //             break;
    //         case "_perception":
    //             _perception++;
    //             break;
    //         case "_intellect":
    //             _intellect++;
    //             break;
    //     }
    // }
}

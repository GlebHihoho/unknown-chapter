using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;
using Unity.VisualScripting;

public class MainCharacteristics : MonoBehaviour
{
    [SerializeField] private double _physicalAbilities = 1; // физические способности
    [SerializeField] private double _perception = 1;        // восприятие
    [SerializeField] private double _intellect  = 0;        // интелект 

    private void Start()
    {
        var ds = GetComponent<DialogueSystemTrigger>();


    }

    private double GetPhysicalAbilities(double value)
    {
        return _physicalAbilities;
    }
    
    private double GetPerception(double value)
    {
        return _perception;
    }
    
    private double GetIntellect(double value)
    {
        return _intellect;
    }

    private void Update()
    {
        var textDescription = DialogueLua.GetQuestField("Захоронение тел", "Description").luaValue;

        if (Input.GetKeyDown(KeyCode.T))
        {
            DialogueLua.SetQuestField("Захоронение тел", "Description", textDescription + "/\n111");

        }
    }

    public void AddCharacteristic(string nameCharacteristic)
    {
        switch (nameCharacteristic)
        {
            case "_physicalAbilities":
                _physicalAbilities++;
                break;
            case "_perception":
                _perception++;
                break;
            case "_intellect":
                _intellect++;
                break;
        }
    }

    private void OnEnable()
    {
        // Lua.RegisterFunction("PhysicalAbilities", this, SymbolExtensions.GetMethodInfo(() => _physicalAbilities((double)0)));
        Lua.RegisterFunction("PhysicalAbilities", this, SymbolExtensions.GetMethodInfo(() => GetPhysicalAbilities(1)));
        Lua.RegisterFunction("Perception", this, SymbolExtensions.GetMethodInfo(() => GetPerception(1)));
        Lua.RegisterFunction("Intellect", this, SymbolExtensions.GetMethodInfo(() => GetIntellect(1)));

    }

    private void OnDisable()
    {
        Lua.UnregisterFunction("PhysicalAbilities");
        Lua.UnregisterFunction("Perception");
        Lua.UnregisterFunction("Intellect");
    }

    
    
    
    
}

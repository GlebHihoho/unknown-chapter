using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;
using Unity.VisualScripting;

public class MainCharacteristics : MonoBehaviour
{
    [SerializeField] private int _physicalAbilities = 1; // физические способности
    [SerializeField] private int _perception = 1;        // восприятие
    [SerializeField] private int _intellect  = 0;        // интеллект 

    private void Start()
    {
        var ds = GetComponent<DialogueSystemTrigger>();
    }

    private void Update()
    {
        var textDescription = DialogueLua.GetQuestField("Захоронение тел", "Description").luaValue;

        if (Input.GetKeyDown(KeyCode.T))
        {
            DialogueLua.SetQuestField("Захоронение тел", "Description", textDescription + "/\n111");
        }
    }

    private void OnEnable()
    {
        // Lua.RegisterFunction("PhysicalAbilities", this, SymbolExtensions.GetMethodInfo(() => _physicalAbilities((int)0)));
        Lua.RegisterFunction("PhysicalAbilities", this, SymbolExtensions.GetMethodInfo(() => SetPhysicalAbilities(1)));
        Lua.RegisterFunction("Perception", this, SymbolExtensions.GetMethodInfo(() => SetPerception(1)));
        Lua.RegisterFunction("Intellect", this, SymbolExtensions.GetMethodInfo(() => SetIntellect(1)));

    }

    private void OnDisable()
    {
        Lua.UnregisterFunction("PhysicalAbilities");
        Lua.UnregisterFunction("Perception");
        Lua.UnregisterFunction("Intellect");
    }

    public int GetPhysicalAbilities()
    {
        return _physicalAbilities;
    }

    private void SetPhysicalAbilities(int value)
    {
        _physicalAbilities = value;
    }
    
    public int GetPerception()
    {
        return _perception;
    }
    
    private void SetPerception(int value)
    {
        _perception = value;
    }
    
    public int GetIntellect()
    {
        return _intellect;
    }
    
    public void SetIntellect(int value)
    {
        _intellect = value;
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
}

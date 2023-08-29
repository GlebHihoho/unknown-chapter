using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;
using Unity.VisualScripting;
using Update = UnityEngine.PlayerLoop.Update;

public class MainCharacteristics : MonoBehaviour
{
    [SerializeField] private double _physicalAbilities = 1; // физические способности
    [SerializeField] private double _perception = 1;        // восприятие
    [SerializeField] private double _intellect  = 0;        // интеллект 
    private double _physicalAbilitiesDouble;
    private double _perceptionDouble;
    private double _intellectDouble;

    private void Update()
    {
        _physicalAbilitiesDouble = (Int32)_physicalAbilities;
        _perceptionDouble = (Int32)_perception;
        _intellectDouble = (Int32)_intellect;
    }

    private void OnEnable()
    {
        // Lua.RegisterFunction("PhysicalAbilities", this, SymbolExtensions.GetMethodInfo(() => _physicalAbilities((int)0)));
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

    private void SetPhysicalAbilities(double value)
    {
        _physicalAbilities = value;
    }
    
    public double GetPhysicalAbilities(double value)
    {
        return _physicalAbilities;
    }
    
    public double GetPerception(double value)
    {
        return _perception;
    }
    
    private void SetPerception(double value)
    {
        _perception = value;
    }
    
    public double GetIntellect(double value)
    {
        return _intellect;
    }
    
    public void SetIntellect(double value)
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

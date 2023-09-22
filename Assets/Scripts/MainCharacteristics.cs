using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;
using UI;
using UI.InventoryScripts;
using Unity.VisualScripting;
using Update = UnityEngine.PlayerLoop.Update;
using System.Net.Http.Headers;

public class MainCharacteristics : MonoBehaviour
{
    [SerializeField] private double _physicalAbilities = 0; // физические способности
    [SerializeField] private double _perception = 0;        // восприятие
    [SerializeField] private double _intellect  = 0;        // интеллект 
    [SerializeField] private Inventory _inventory;
    [SerializeField] private StatBlocksScript _statBlockPhys;
    [SerializeField] private StatBlocksScript _statBlockPerc;
    [SerializeField] private StatBlocksScript _statBlockInt;

    public event Action StatCurrent;


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
        Lua.RegisterFunction("CharacteristicIncrease", this, SymbolExtensions.GetMethodInfo(() => CharacteristicIncrease(string.Empty)));
        Lua.RegisterFunction("CharacteristicDecreasee", this, SymbolExtensions.GetMethodInfo(() => CharacteristicDecreasee(string.Empty)));
        Lua.RegisterFunction("ReloadConversation", this, SymbolExtensions.GetMethodInfo(() => ReloadConversation()));
    }

    private void OnDisable()
    {
        Lua.UnregisterFunction("GetSkill");
        Lua.UnregisterFunction("GetItemAmount");
        Lua.UnregisterFunction("DialogueSystemItemDeleter");
        Lua.UnregisterFunction("DialogueSystemItemAdder");
        Lua.UnregisterFunction("CharacteristicIncrease");
        Lua.UnregisterFunction("CharacteristicDecreasee");
        Lua.UnregisterFunction("ReloadConversation");
    }

    private void ChangeDialogueText(string a)
    {
        
    }

    private void ReloadConversation()
    {
        DialogueManager.conversationController.GotoState(DialogueManager.currentConversationState);
        DialogueManager.conversationController.GotoState(DialogueManager.conversationModel.FirstState);
        print(DialogueManager.currentConversationState);
        print(DialogueManager.conversationModel.FirstState);
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

    public void CharacteristicIncrease(string nameCharacteristic)
    {
        switch (nameCharacteristic)
        {
            case "PhysicalAbilities": _physicalAbilities++; /*_statBlockPhys.Updatic();*/
                /*if (StatCurrent != null) StatCurrent();*/
                break;
            case "Perception": _perception++; /*_statBlockPerc.Updatic();*/ break;
            case "Intellect": _intellect++; /*_statBlockInt.Updatic();*/ break;
        }

    }    
    
    
    public void CharacteristicDecreasee(string nameCharacteristic)
    {
        switch (nameCharacteristic)
        {
            case "PhysicalAbilities": _physicalAbilities--; break;
            case "Perception": _perception--; break;
            case "Intellect": _intellect--; break;
        }
    }
}

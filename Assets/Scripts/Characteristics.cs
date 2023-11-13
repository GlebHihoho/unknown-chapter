using UnityEngine;
using PixelCrushers.DialogueSystem;
using UI.InventoryScripts;

public class Characteristics : MonoBehaviour
{
    [SerializeField] private double _physicalAbilities = 0; // физические способности
    [SerializeField] private double _perception = 0;        // восприятие
    [SerializeField] private double _intellect  = 0;        // интеллект 
    [SerializeField] private Inventory _inventory;
    
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

    // TODO: вынести регистрацию методов в Lua в отдельный скрипт
    private void OnEnable()
    {
        Lua.RegisterFunction("GetSkill", this, SymbolExtensions.GetMethodInfo(() => GetSkill("")));
        Lua.RegisterFunction("GetItemAmount", _inventory, SymbolExtensions.GetMethodInfo(() => _inventory.GetItemAmount(string.Empty)));
        Lua.RegisterFunction("DialogueSystemItemDeleter", _inventory, SymbolExtensions.GetMethodInfo(() => _inventory.DialogueSystemItemDeleter(string.Empty, 1)));
        Lua.RegisterFunction("DialogueSystemItemAdder", _inventory, SymbolExtensions.GetMethodInfo(() => _inventory.DialogueSystemItemAdder(string.Empty, 1)));
        Lua.RegisterFunction("CharacteristicIncrease", this, SymbolExtensions.GetMethodInfo(() => CharacteristicIncrease(string.Empty)));
        Lua.RegisterFunction("CharacteristicDecreasee", this, SymbolExtensions.GetMethodInfo(() => CharacteristicDecreasee(string.Empty)));
        Lua.RegisterFunction("ChangeDialogueText", this, SymbolExtensions.GetMethodInfo(() => ChangeDialogueText()));
    }

    // TODO: вынести регистрацию методов в Lua в отдельный скрипт
    private void OnDisable()
    {
        Lua.UnregisterFunction("GetSkill");
        Lua.UnregisterFunction("GetItemAmount");
        Lua.UnregisterFunction("DialogueSystemItemDeleter");
        Lua.UnregisterFunction("DialogueSystemItemAdder");
        Lua.UnregisterFunction("CharacteristicIncrease");
        Lua.UnregisterFunction("CharacteristicDecreasee");
        Lua.UnregisterFunction("ChangeDialogueText");
    }

    private void ChangeDialogueText()
    {
        print(DialogueManager.currentConversationState.HasPCResponses);
    }
    
    public void CharacteristicIncrease(string nameCharacteristic)
    {
        switch (nameCharacteristic)
        {
            case "PhysicalAbilities" when _physicalAbilities < 5f: _physicalAbilities++; break;
            case "Perception" when _perception < 5f: _perception++; break;
            case "Intellect" when _intellect < 5f: _intellect++; break;
        }
    }
    
    public void CharacteristicDecreasee(string nameCharacteristic)
    {
        switch (nameCharacteristic)
        {
            case "PhysicalAbilities" when _physicalAbilities > 0f: _physicalAbilities--; break;
            case "Perception" when _perception > 0f: _perception--; break;
            case "Intellect" when _intellect > 0f: _intellect--; break;
        }
    }
}

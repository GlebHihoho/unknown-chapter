using UnityEngine;
using PixelCrushers.DialogueSystem;
using UI.InventoryScripts;

public class Characteristics : MonoBehaviour, ISaveable
{
    [SerializeField] private double _physicalAbilities = 0; // физические способности
    [SerializeField] private double _perception = 0;        // восприятие
    [SerializeField] private double _intellect  = 0;        // интеллект 


    
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

        Lua.RegisterFunction("CharacteristicIncrease", this, SymbolExtensions.GetMethodInfo(() => CharacteristicIncrease(string.Empty)));
        Lua.RegisterFunction("CharacteristicDecreasee", this, SymbolExtensions.GetMethodInfo(() => CharacteristicDecreasee(string.Empty)));
        Lua.RegisterFunction("ChangeDialogueText", this, SymbolExtensions.GetMethodInfo(() => ChangeDialogueText()));

        InventoryManager.OnQuantityDifference += ApplyItemModifier;
    }

    // TODO: вынести регистрацию методов в Lua в отдельный скрипт
    private void OnDisable()
    {
        Lua.UnregisterFunction("GetSkill");

        Lua.UnregisterFunction("CharacteristicIncrease");
        Lua.UnregisterFunction("CharacteristicDecreasee");
        Lua.UnregisterFunction("ChangeDialogueText");

        InventoryManager.OnQuantityDifference -= ApplyItemModifier;
    }



    private void ApplyItemModifier(ItemData item, int delta)
    {

        double Apply(double stat, int modifier)
        {
            return Mathf.Clamp((float)stat + modifier * delta, 0, 5);
        }

        _physicalAbilities = Apply(_physicalAbilities, item.PerceptionlModifier);
        _perception = Apply(_perception, item.PerceptionlModifier);
        _intellect = Apply(_intellect, item.IntellectModifier);
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

    public void Save(ref SaveData.Save save)
    {
        save.physical = _physicalAbilities;
        save.perception = _perception;
        save.intellect = _intellect;
    }

    public void Load(SaveData.Save save)
    {
        _physicalAbilities = save.physical;
        _perception = save.perception;
        _intellect = save.intellect;
    }
}

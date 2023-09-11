using UnityEngine;
using PixelCrushers.DialogueSystem;

public class SkillCheckExample : MonoBehaviour // (Based on TemplateCustomLua.cs)
{

    [Tooltip("Does the player have the Intimidate ability?")]
    public bool intimidate = false;

    public bool GetIntimidateBool() { return intimidate; }
    public void SetIntimidateBool(bool value) { intimidate = value; }

    [Tooltip("Typically leave unticked so temporary Dialogue Managers don't unregister your functions.")]
    public bool unregisterOnDisable = false;

    void OnEnable()
    {
        // Make the functions available to Lua:
        Lua.RegisterFunction(nameof(GetIntimidateBool), this, SymbolExtensions.GetMethodInfo(() => GetIntimidateBool()));
        Lua.RegisterFunction(nameof(SetIntimidateBool), this, SymbolExtensions.GetMethodInfo(() => SetIntimidateBool(false)));
    }

    void OnDisable()
    {
        if (unregisterOnDisable)
        {
            // Remove the functions from Lua:
            Lua.UnregisterFunction(nameof(GetIntimidateBool));
            Lua.UnregisterFunction(nameof(SetIntimidateBool));
        }
    }
}

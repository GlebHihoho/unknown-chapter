using PixelCrushers.DialogueSystem;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    [SerializeField] ConversationPartner[] characters;
    public ConversationPartner[] Characters => characters;


    private void OnEnable()
    {
        Lua.RegisterFunction("SetTalkIndex", this, SymbolExtensions.GetMethodInfo(() => SetTalkIndex(string.Empty, 0)));
    }

    private void OnDisable()
    {
        Lua.UnregisterFunction("SetTalkIndex");
    }

    private void SetTalkIndex(string actor, double index)
    {
        foreach (ConversationPartner character in characters) 
        { 
            character.SetTalkIndex(actor, index);
        }
    }
}

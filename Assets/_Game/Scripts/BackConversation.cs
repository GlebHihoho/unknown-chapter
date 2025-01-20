using UnityEngine;
using PixelCrushers.DialogueSystem;

public class BackConversation : MonoBehaviour
{

    string previousConversation;
    int previousEntryID;
    string currentConversation;
    int currentEntryID;

    void OnEnable()
    {
        // Call this Lua function in an entry's Script field to return to the previous conversation:
        Lua.RegisterFunction("Back", this, SymbolExtensions.GetMethodInfo(() => Back()));
    }

    void OnDisable()
    {
        Lua.UnregisterFunction("Back");
    }

    public void Back()
    {
        // Stop the current conversation and start the previous one:
        DialogueManager.StopConversation();
        DialogueManager.StartConversation(previousConversation, DialogueManager.currentActor, DialogueManager.currentConversant, previousEntryID);
    }

    private void OnConversationLine(Subtitle subtitle)
    {
        // When an NPC speaks, record the current conversation:
        if (subtitle.speakerInfo.isNPC)
        {
            currentConversation = DialogueManager.masterDatabase.GetConversation(subtitle.dialogueEntry.conversationID).Title;
            currentEntryID = subtitle.dialogueEntry.id;
        }
    }

    private void OnLinkedConversationStart(Transform actor)
    {
        // When we cross conversations, record the previous conversation:
        previousConversation = currentConversation;
        previousEntryID = currentEntryID;
    }
}

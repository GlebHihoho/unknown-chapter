using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PixelCrushers.DialogueSystem;

/// <summary>
/// This is a somewhat simplified version of ConversationPositionStack. 
/// It adds two Lua functions: RecordFallback and GotoFallback.
/// If you call RecordFallback in a dialogue entry node's Script field, it
/// will record the current position in the conversation onto a stack.
/// If you call GotoFallback, or if the script is added to a UI Button,
/// it will go back to the top position recorded on the stack.
/// </summary>
/// TODO - UI - работает ли оно или нет?
public class FallbackButton : MonoBehaviour
{
    public bool debug = false;

    private static bool registered = false;
    private bool iRegistered = false;
    private bool fallingBack = false;
    private DialogueEntry fallbackEntry;
    private Stack<DialogueEntry> stack = new Stack<DialogueEntry>();

    private void OnEnable()
    {
        // Register the Lua functions:
        if (!registered)
        {
            registered = true;
            iRegistered = true;
            Lua.RegisterFunction("RecordFallback", this, SymbolExtensions.GetMethodInfo(() => RecordFallback()));
            Lua.RegisterFunction("GotoFallback", this, SymbolExtensions.GetMethodInfo(() => FallBack()));
        }

        // Hook up the UI button:
        var button = GetComponent<Button>();
        if (button != null) button.onClick.AddListener(FallBack);
    }

    private void OnDisable()
    {
        if (iRegistered)
        {
            registered = false;
            iRegistered = false;
            Lua.UnregisterFunction("RecordFallback");
            Lua.UnregisterFunction("GotoFallback");
        }
    }

    private void RecordFallback()
    {
        DialogueEntry entry = null;
        if (fallingBack)
        {
            // If we're falling back, record the entry we're falling back to:
            entry = fallbackEntry;
        }
        else
        {
            // Otherwise record the entry whose Script called RecordFallback():
            var state = DialogueManager.currentConversationState;
            entry = state.hasNPCResponse ? state.firstNPCResponse.destinationEntry
                : state.hasPCResponses ? state.pcResponses[0].destinationEntry
                : null;
        }
        if (entry != null)
        {
            if (debug) Debug.Log("Record [" + entry.id + "]: " + entry.subtitleText);
            stack.Push(entry);
        }
        print("record");
    }

    private void FallBack()
    {
        if (stack.Count > 0)
        {
            var entry = stack.Pop();
            if (entry == DialogueManager.currentConversationState.subtitle.dialogueEntry)
            {
                // If we're falling back from a fallback node, assume it has recorded
                // itself onto the top of the stack. We need to pop that off and then
                // pop the next one down to get the actual place to fall back:
                if (stack.Count == 0) return;
                entry = stack.Pop();
            }
            if (debug) Debug.Log("Fall back to [" + entry.id + "]: " + entry.subtitleText);
            fallingBack = true;
            fallbackEntry = entry;
            var state = DialogueManager.conversationModel.GetState(entry);
            fallingBack = false;
            DialogueManager.conversationController.GotoState(state);
        }
        print("Goto");
    }
}

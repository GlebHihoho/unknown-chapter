using NUnit.Framework.Interfaces;
using PixelCrushers.DialogueSystem;
using PixelCrushers.DialogueSystem.ChatMapper;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public class ConversationPartner : Interactable
{

    [SerializeField] NPCData data;

    [Space]
    [SerializeField, Min(0)] int conversationIndex = 0;



    protected override void PerfomInteraction()
    {
        base.PerfomInteraction();

        if (conversationIndex >= 0 && conversationIndex < data.Conversations.Length)
            DialogueManager.StartConversation(data.Conversations[conversationIndex]);
    }

    public void Talk(int conversationIndex = 0)
    {
        if (conversationIndex >= 0 && conversationIndex < data.Conversations.Length)
            DialogueManager.StartConversation(data.Conversations[conversationIndex]);
    }



}

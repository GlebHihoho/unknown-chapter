using NUnit.Framework.Interfaces;
using PixelCrushers.DialogueSystem;
using PixelCrushers.DialogueSystem.ChatMapper;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public class ConversationPartner : Interactable
{

    [SerializeField] NPCData data;


    protected override void PerfomInteraction()
    {
        base.PerfomInteraction();

        DialogueManager.StartConversation(data.Conversation);
    }



}

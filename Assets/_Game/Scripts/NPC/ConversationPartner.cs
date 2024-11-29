using NUnit.Framework.Interfaces;
using PixelCrushers.DialogueSystem;
using PixelCrushers.DialogueSystem.ChatMapper;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public class ConversationPartner : Interactable
{

    [SerializeField] NPCData data;

    [Space]
    [SerializeField, Min(0)] int talkIndex = 0;
    public int TalkIndex => talkIndex;



    protected override void PerfomInteraction()
    {
        base.PerfomInteraction();

        if (talkIndex >= 0 && talkIndex < data.Conversations.Length)
            DialogueManager.StartConversation(data.Conversations[talkIndex]);
    }

    public void Talk(int talkIndex = 0)
    {
        if (talkIndex >= 0 && talkIndex < data.Conversations.Length)
            DialogueManager.StartConversation(data.Conversations[talkIndex]);
    }

    public void SetTalkIndex(string actor, double index)
    {
        if (actor == data.DialogueActor) SetTalkIndex(Mathf.FloorToInt((float)index));
    }

    public void SetTalkIndex(int index) => talkIndex = index;



}

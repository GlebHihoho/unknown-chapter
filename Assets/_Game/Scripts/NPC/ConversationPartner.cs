using NUnit.Framework.Interfaces;
using PixelCrushers.DialogueSystem;
using PixelCrushers.DialogueSystem.ChatMapper;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Outline))]
public class ConversationPartner : Interactable
{

    [SerializeField] NPCData data;

    [Space]
    [SerializeField, Min(0)] int talkIndex = 0;
    public int TalkIndex => talkIndex;

    bool hasMet = false;
    public bool HasMet => hasMet;

    public UnityEvent OnMet;



    protected override void PerfomInteraction()
    {
        base.PerfomInteraction();

        if (talkIndex >= 0 && talkIndex < data.Conversations.Length)
        {
            DialogueManager.StartConversation(data.Conversations[talkIndex]);

            if (!hasMet)
            {
                hasMet = true;
                OnMet.Invoke();
            }
        }
    }

    public void Talk(int talkIndex = 0)
    {
        if (talkIndex >= 0 && talkIndex < data.Conversations.Length)
            DialogueManager.StartConversation(data.Conversations[talkIndex]);

        if (!hasMet)
        {
            hasMet = true;
            OnMet.Invoke();
        }
    }

    public void SetTalkIndex(string actor, double index)
    {
        if (actor == data.DialogueActor) SetTalkIndex(Mathf.FloorToInt((float)index));
    }

    public void SetTalkIndex(int index) => talkIndex = index;


    public void SetMet(bool met) => hasMet = met;



}

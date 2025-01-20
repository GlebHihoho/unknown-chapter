using PixelCrushers.DialogueSystem;
using UnityEngine;

[CreateAssetMenu(fileName = "NPCData", menuName = "Gameplay/Interactable/NPCData")]
public class NPCData : ScriptableObject
{
    [SerializeField, ActorPopup] string dialogueActor;
    public string DialogueActor => dialogueActor;

    [SerializeField, ConversationPopup] string[] conversations;
    public string[] Conversations => conversations;

}

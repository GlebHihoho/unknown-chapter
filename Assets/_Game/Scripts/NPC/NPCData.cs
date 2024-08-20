using UnityEngine;

[CreateAssetMenu(fileName = "NPCData", menuName = "Gameplay/Interactable/NPCData")]
public class NPCData : ScriptableObject
{
    [SerializeField] string dialogueActor;
    public string DialogueActor => dialogueActor;

    [SerializeField] string[] conversations;
    public string[] Conversations => conversations;

}

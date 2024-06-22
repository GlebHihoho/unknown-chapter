using UnityEngine;

[CreateAssetMenu(fileName = "NPCData", menuName = "Gameplay/Interactable/NPCData")]
public class NPCData : ScriptableObject
{
    [SerializeField] string conversation;
    public string Conversation => conversation;

    [SerializeField, TextArea(0, 3)] string examineBark;
    public string ExamineBark => examineBark;
}

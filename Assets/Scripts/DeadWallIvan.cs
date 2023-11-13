using PixelCrushers.DialogueSystem;
using UnityEngine;

public class DeadWallIvan : MonoBehaviour
{
    private DialogueSystemController _dialogue;
    // [SerializeField] private DialogueSystemTrigger _dialogueIvanTrigger;
    [SerializeField] private Usable _dialogueIvanTrigger;


    void Start()
    {
        _dialogue = FindObjectOfType<DialogueSystemController>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            var isFirstMeet = DialogueLua.GetVariable("Первая встреча с Иваном").AsBool;
            if(!isFirstMeet)
            {
                _dialogueIvanTrigger.enabled = false;
            }
        }
    }
}
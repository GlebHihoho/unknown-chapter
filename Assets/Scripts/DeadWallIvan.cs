using PixelCrushers.DialogueSystem;
using UnityEngine;

// TODO: сделать скрипт универсальным. Ivan в названии не допустим
// Создать папку со скриптами связанными с сценариями
public class DeadWallIvan : MonoBehaviour
{
    // TODO: нужен ли
    private DialogueSystemController _dialogue;
    // [SerializeField] private DialogueSystemTrigger _dialogueIvanTrigger;
    [SerializeField] private Usable _dialogueIvanTrigger;


    // TODO - нужен ли
    void Start()
    {
        _dialogue = FindObjectOfType<DialogueSystemController>();
    }

    // TODO
    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
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

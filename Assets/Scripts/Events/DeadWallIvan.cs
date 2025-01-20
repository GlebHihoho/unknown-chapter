using PixelCrushers.DialogueSystem;
using UnityEngine;

namespace Events
{
    public class DeadWallIvan : MonoBehaviour
    {
        [SerializeField] private Usable _dialogueIvanTrigger;

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
}

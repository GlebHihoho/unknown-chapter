using PixelCrushers.DialogueSystem;
using UnityEngine;
using UnityEngine.Serialization;

namespace Events
{
    public class OpenNextDialogue : MonoBehaviour
    {
        [FormerlySerializedAs("beachConversation_2")] [SerializeField] private GameObject _beachConversation_2;

        public void OpenBeachConversation_2()
        {
            _beachConversation_2.GetComponent<DialogueSystemTrigger>().enabled = true;
        }
    }
}

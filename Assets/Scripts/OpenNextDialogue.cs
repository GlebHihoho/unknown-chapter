using System;
using PixelCrushers.DialogueSystem;
using UnityEngine;

namespace DefaultNamespace
{
    public class OpenNextDialogue : MonoBehaviour
    {
        // private DialogueSystemTrigger _dialogueSystemTrigger;
        [SerializeField] private GameObject beachConversation_2;
        //
        // private void Start()
        // {
        //     _dialogueSystemTrigger = GetComponent<DialogueSystemTrigger>();
        //     _dialogueSystemTrigger.enabled = true;
        // }

        public void OpenBeachConversation_2()
        {
            beachConversation_2.GetComponent<DialogueSystemTrigger>().enabled = true;
        }
    }
}
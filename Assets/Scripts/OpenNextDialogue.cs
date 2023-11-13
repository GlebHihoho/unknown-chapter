using System;
using PixelCrushers.DialogueSystem;
using UnityEngine;

// Создать папку со скриптами связанными с сценариями
namespace DefaultNamespace
{
    public class OpenNextDialogue : MonoBehaviour
    {
        [SerializeField] private GameObject beachConversation_2;

        public void OpenBeachConversation_2()
        {
            beachConversation_2.GetComponent<DialogueSystemTrigger>().enabled = true;
        }
    }
}

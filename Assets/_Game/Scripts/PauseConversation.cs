using PixelCrushers.DialogueSystem;
using UnityEngine;

public class PauseConversation : MonoBehaviour
{
    private void OnEnable() => DialogueManager.Pause();
    private void OnDisable() => DialogueManager.Unpause();
}

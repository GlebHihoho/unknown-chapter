using PixelCrushers.DialogueSystem;
using System;
using UnityEngine;

public class QuestsEvents : MonoBehaviour
{

    public static event Action<string> OnQuestChanged;
    public static event Action<QuestEntryArgs> OnEntryStateChange;

    private void OnQuestStateChange(string title) => OnQuestChanged?.Invoke(title);
    private void OnQuestEntryStateChange(QuestEntryArgs args) => OnEntryStateChange?.Invoke(args);
}

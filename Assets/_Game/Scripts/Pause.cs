using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using PixelCrushers.DialogueSystem;

public class Pause : MonoBehaviour
{

    public static event Action<bool> OnPause;
    public static event Action<bool> OnDisableKeys;
    public static event Action<bool> OnConversation;

    int pausedCount = 0;
    public bool IsPaused => pausedCount > 0;

    bool manualPause = false;

    public static Pause instance;


    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }


    private void Start() => GameControls.instance.OnPause += Pause_performed;
    private void OnDestroy() => GameControls.instance.OnPause -= Pause_performed;


    private void Pause_performed()
    {
        manualPause = !manualPause;
        SetPause(manualPause);

        if (manualPause) UIMessage.instance.ShowPermanentMessage("Пауза.");
        else UIMessage.instance.HidePermanentMessage();
    }


    private void OnEnable()
    {
        DialogueManager.Instance.conversationStarted += ConversationStarted;
        DialogueManager.instance.conversationEnded += ConversationEnded;

        SaveManager.OnLoadCompleted += ResetPause;

        GameConsole.OnConsoleActivated += ConsoleActivated;
    }


    private void OnDisable()
    {
        if (DialogueManager.Instance != null) // Fixing a bug with dialogue manager already absent, for some reason.
        {
            DialogueManager.Instance.conversationStarted -= ConversationStarted;
            DialogueManager.instance.conversationEnded -= ConversationEnded;
        }

        SaveManager.OnLoadCompleted -= ResetPause;

        GameConsole.OnConsoleActivated -= ConsoleActivated;
        
    }

    private void ConversationStarted(Transform t)
    {
        OnConversation?.Invoke(true);
        SetPause(true);
    }

    private void ConversationEnded(Transform t)
    {
        SetPause(false);
        OnConversation?.Invoke(false);
    }


    public void SetPause(bool isPaused, bool affectKeys = true)
    {

        if (isPaused) pausedCount++;
        else pausedCount--;

        pausedCount = Mathf.Max(0, pausedCount);

        OnPause?.Invoke(pausedCount > 0);

        if (affectKeys) OnDisableKeys?.Invoke(pausedCount > 0);
    }


    private void ResetPause()
    {
        pausedCount = 0;
        SetPause(false);
    }


    private void ConsoleActivated(bool isActive)
    {
        if (isActive)
        {
            SetPause(true);
        }
        else
        {
            SetPause(false);
        }
    }
}

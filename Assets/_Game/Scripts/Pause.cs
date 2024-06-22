using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using PixelCrushers.DialogueSystem;

public class Pause : MonoBehaviour
{

    public static event Action<bool> OnPause;

    bool isPaused = false;
    bool conversationinProgress = false;

    public static Pause instance;

    private void Awake()
    {
        if (instance == null) instance = this;

        PlayerInputActions inputActions = new();
        inputActions.Player.Enable();
        inputActions.Player.Pause.performed += Pause_performed;
    }


    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        SetPause(!isPaused);
    }


    private void OnEnable()
    {
        DialogueManager.Instance.conversationStarted += ConversationStarted;
        DialogueManager.instance.conversationEnded += ConversationEnded;

        SaveManager.OnLoadCompleted += ResetPause;
    }


    private void OnDisable()
    {
        if (DialogueManager.Instance != null) // Fixing a bug with dialogue manager already absent, for some reason.
        {
            DialogueManager.Instance.conversationStarted -= ConversationStarted;
            DialogueManager.instance.conversationEnded -= ConversationEnded;

            SaveManager.OnLoadCompleted -= ResetPause;
        }
    }

    private void ConversationStarted(Transform t)
    {
        SetPause(true);
        conversationinProgress = true;
    }

    private void ConversationEnded(Transform t)
    {
        conversationinProgress = false;
        SetPause(false);
    }


    public void SetPause(bool isPaused)
    {
        if (conversationinProgress) return;

        this.isPaused = isPaused;
        OnPause?.Invoke(isPaused);
    }


    private void ResetPause()
    {
        conversationinProgress = false;
        SetPause(false);
    }
}

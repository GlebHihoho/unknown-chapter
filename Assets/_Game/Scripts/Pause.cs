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
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) SetPause(!isPaused);
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

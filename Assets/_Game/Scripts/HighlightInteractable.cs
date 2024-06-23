using System;
using UnityEngine;

public class HighlightInteractable : MonoBehaviour
{

    public static event Action<bool> OnHighlightsEnabled;


    private void Start()
    {
        GameControls.instance.OnHighlightStarted += EnableHighlights;
        GameControls.instance.OnHighlightEnded += DisableHighlights;
    }

    private void OnDestroy()
    {
        GameControls.instance.OnHighlightStarted -= EnableHighlights;
        GameControls.instance.OnHighlightEnded -= DisableHighlights;
    }


    private void EnableHighlights(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnHighlightsEnabled?.Invoke(true);
    }

    private void DisableHighlights(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnHighlightsEnabled?.Invoke(false);
    }
}

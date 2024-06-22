using System;
using UnityEngine;

public class HighlightInteractable : MonoBehaviour
{

    public static event Action<bool> OnHighlightsEnabled;

    PlayerInputActions inputActions;

    private void Awake()
    {
        inputActions = new();
        inputActions.Player.Enable();
        inputActions.Player.Highlight.started += EnableHighlights;
        inputActions.Player.Highlight.canceled += DisableHighlights;
    }

    private void OnDestroy()
    {
        inputActions.Player.Disable();
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

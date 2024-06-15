using System;
using UnityEngine;

public class HighlightInteractable : MonoBehaviour
{

    public static event Action<bool> OnHighlightsEnabled;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt) || Input.GetKeyDown(KeyCode.RightAlt))
        {
            EnableHighlights();
        }
        else if (Input.GetKeyUp(KeyCode.LeftAlt) || Input.GetKeyUp(KeyCode.RightAlt))
        {
            DisableHighlights();
        }
    }


    private void EnableHighlights()
    {
        OnHighlightsEnabled?.Invoke(true);
    }

    private void DisableHighlights()
    {
        OnHighlightsEnabled?.Invoke(false);
    }
}

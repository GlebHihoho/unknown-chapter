using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class UIMessage : MonoBehaviour
{
    TextMeshProUGUI messageLabel;

    public static UIMessage instance;

    string permanentMessage = string.Empty;

    Queue<string> awaitingMessages = new();
    bool isPlaying = false;

    bool isPaused = false;

    string currentlyPlaying = string.Empty;

    private void Awake()
    {
        if (instance == null) instance = this;

        messageLabel = GetComponent<TextMeshProUGUI>();
        messageLabel.text = string.Empty;
    }

    private void Start() => Pause.OnPause += SetPause;
    private void OnDestroy() => Pause.OnPause -= SetPause;

    private void SetPause(bool isPaused)
    {
        this.isPaused = isPaused;

        if (!isPaused && awaitingMessages.Count > 0) ShowMessage();
    }

    public void ShowMessage(string message)
    {
        if (message != currentlyPlaying && !awaitingMessages.Contains(message))
        {
            awaitingMessages.Enqueue(message);
            if (!isPlaying) ShowMessage();
        }
    }


    private void ShowMessage()
    {
        isPlaying = true;

        if (isPaused) return;

        messageLabel.text = awaitingMessages.Dequeue();
        currentlyPlaying = messageLabel.text;

        Sequence sequence = DOTween.Sequence();

        sequence.Append(messageLabel.DOFade(1, 0.2f));
        sequence.AppendInterval(4);
        sequence.Append(messageLabel.DOFade(0, 0.5f));

        sequence.OnComplete(() =>
        {
            if (awaitingMessages.Count > 0) ShowMessage();
            else
            {
                isPlaying = false;
                currentlyPlaying = string.Empty;

                if (permanentMessage != string.Empty)
                {
                    ShowPermanentMessage(permanentMessage);
                }
            }
        });

        sequence.Play();
    }

    public void ShowPermanentMessage(string message)
    {
        messageLabel.text = message;
        permanentMessage = message;

        messageLabel.DOFade(1, 0.2f);
    }

    public void HidePermanentMessage()
    {
        permanentMessage = string.Empty;
        messageLabel.DOFade(0, 0.2f);
    }

}

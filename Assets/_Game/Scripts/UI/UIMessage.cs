using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TextMeshProUGUI))]
public class UIMessage : MonoBehaviour
{
    TextMeshProUGUI messageLabel;

    [SerializeField] Image background;

    public static UIMessage instance;

    string permanentMessage = string.Empty;

    Queue<string> awaitingMessages = new();
    bool isPlaying = false;

    bool isPaused = false;

    bool panelVisible = false;

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

        if (!isPaused && awaitingMessages.Count > 0)
            if (!panelVisible) ShowPanel(); else ShowMessage();
    }

    public void ShowMessage(string message)
    {
        if (message != currentlyPlaying && !awaitingMessages.Contains(message))
        {
            awaitingMessages.Enqueue(message);
            if (!isPlaying) ShowPanel();
        }
    }


    private void ShowMessage()
    {
        

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
                currentlyPlaying = string.Empty;
                HidePanel();

                if (permanentMessage != string.Empty)
                {
                    ShowPermanentMessage(permanentMessage);
                }
            }
        });

        sequence.Play();
    }


    private void ShowPanel()
    {
        isPlaying = true;

        if (isPaused) return;
        
        background.rectTransform.DOScaleX(1, 0.5f).OnComplete(() => 
        {
            panelVisible = true;
            ShowMessage();           
        });
    }


    private void HidePanel()
    {
        background.rectTransform.DOScaleX(0, 0.2f).OnComplete(() => panelVisible = false);
        isPlaying = false;
    }

    public void ShowPermanentMessage(string message)
    {
        messageLabel.text = message;
        permanentMessage = message;

        Sequence sequence = DOTween.Sequence();
        sequence.Append(background.rectTransform.DOScaleX(1, 0.5f).OnComplete(() => panelVisible = true));
        sequence.Append(messageLabel.DOFade(1, 0.2f));
        sequence.Play();
    }

    public void HidePermanentMessage()
    {
        permanentMessage = string.Empty;

        Sequence sequence = DOTween.Sequence();
        sequence.Append(messageLabel.DOFade(0, 0.2f));
        sequence.Append(background.rectTransform.DOScaleX(0, 0.2f));
        sequence.Play().OnComplete(() => panelVisible = false);
    }

}

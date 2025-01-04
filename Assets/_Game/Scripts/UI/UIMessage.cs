using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TextMeshProUGUI))]
public class UIMessage : MonoBehaviour
{

    enum Status { Idle, ShowingPanel, ShowingMessage}
    Status status = Status.Idle;

    TextMeshProUGUI messageLabel;

    [SerializeField] Image background;

    public static UIMessage instance;

    string permanentMessage = string.Empty;

    Queue<string> awaitingMessages = new();


    bool isPaused = false;

    string currentlyPlaying = string.Empty;

    Sequence activeAnimation;


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

        background.enabled = !isPaused;
        messageLabel.enabled = !isPaused;

        if (activeAnimation != null)
        {
            if (isPaused) activeAnimation.Pause();
            else activeAnimation.Play();
        }

        if (!isPaused && awaitingMessages.Count > 0 && status == Status.Idle) ShowPanel();
         //   if (!panelVisible) ShowPanel(); else ShowMessage();
    }


    public void ShowMessage(string message)
    {
        if (message != currentlyPlaying && !awaitingMessages.Contains(message))
        {
            awaitingMessages.Enqueue(message);
            if (status == Status.Idle) ShowPanel();
        }
    }


    private void ShowMessage()
    {
        
        if (isPaused) return;

        status = Status.ShowingMessage;

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

        activeAnimation = sequence;
        sequence.Play();
    }


    private void ShowPanel()
    {
     
        if (isPaused) return;

        status = Status.ShowingPanel;

        Sequence sequence = DOTween.Sequence();

        sequence.Append(
        background.rectTransform.DOScaleX(1, 0.5f).OnComplete(() => 
        {
            ShowMessage();           
        }));

        activeAnimation = sequence;
        sequence.Play();
    }


    private void HidePanel()
    {
        background.rectTransform.DOScaleX(0, 0.2f).OnComplete(() => status = Status.Idle);
    }

    public void ShowPermanentMessage(string message)
    {
        messageLabel.text = message;
        permanentMessage = message;

        background.enabled = true;
        messageLabel.enabled = true;

        Sequence sequence = DOTween.Sequence();
        sequence.Append(background.rectTransform.DOScaleX(1, 0.5f).OnComplete(() => status = Status.ShowingPanel));
        sequence.Append(messageLabel.DOFade(1, 0.2f));
        sequence.Play();
    }

    public void HidePermanentMessage()
    {
        permanentMessage = string.Empty;

        Sequence sequence = DOTween.Sequence();
        sequence.Append(messageLabel.DOFade(0, 0.2f));
        sequence.Append(background.rectTransform.DOScaleX(0, 0.2f));
        sequence.Play().OnComplete(() => status = Status.Idle);

    }

}

using DG.Tweening;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class UIMessage : MonoBehaviour
{
    TextMeshProUGUI messageLabel;

    public static UIMessage instance;

    private void Awake()
    {
        if (instance == null) instance = this;

        messageLabel = GetComponent<TextMeshProUGUI>();
        messageLabel.text = string.Empty;
    }


    public void ShowMessage(string message)
    {
        messageLabel.text = message;

        Sequence sequence = DOTween.Sequence();

        sequence.Append(messageLabel.DOFade(1, 0.2f));
        sequence.AppendInterval(4);
        sequence.Append(messageLabel.DOFade(0, 0.5f));
    }

}

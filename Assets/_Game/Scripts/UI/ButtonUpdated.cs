using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(RectTransform))]
public class ButtonUpdated : MonoBehaviour
{
    RectTransform rectTransform;

    private void Awake() => rectTransform = GetComponent<RectTransform>();

    public void ShowUpdate()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(rectTransform.DOScale(1.5f, 0.4f));
        sequence.Append(rectTransform.DOScale(1, 0.4f));
        sequence.Play();
    }
}

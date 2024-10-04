using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Button))]
public class ButtonUpdated : MonoBehaviour
{
    RectTransform rectTransform;

    WaitForSeconds remindPause = new WaitForSeconds(3);

    Coroutine reminder;

    private void Awake() => rectTransform = GetComponent<RectTransform>();


    public void ShowUpdate()
    {
        ResetUpdate();
        reminder = StartCoroutine(Remind());

        Sequence sequence = DOTween.Sequence();
        sequence.Append(rectTransform.DOScale(1.5f, 0.4f));
        sequence.Append(rectTransform.DOScale(1, 0.4f));
           
        sequence.Play();
    }


    public void ResetUpdate()
    {
        if (reminder != null)
        {
            StopCoroutine(reminder);
            reminder = null;
        }
    }


    private void Shake()
    {
        Sequence sequence = DOTween.Sequence();

        sequence.Append(rectTransform.DOLocalMoveX(5f, 0.1f));
        sequence.Append(rectTransform.DOLocalMoveX(-5f, 0.1f));
        sequence.Append(rectTransform.DOLocalMoveX(0, 0.1f));

        sequence.Play();
    }


    IEnumerator Remind()
    {
        while (true)
        {
            yield return remindPause;
            Shake();
        }

    }
}

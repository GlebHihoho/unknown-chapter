using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Button))]
public class ButtonUpdated : MonoBehaviour
{
    RectTransform rectTransform;

    WaitForSeconds remindPause = new WaitForSeconds(3);
    WaitForSeconds delayAfterPause = new WaitForSeconds(0.5f);

    Coroutine reminder;

    bool isPaused = false;
    bool awatingUpdate = false;


    private void Awake() => rectTransform = GetComponent<RectTransform>();


    private void Start() => Pause.OnPause += SetPause;
    private void OnDestroy() => Pause.OnPause -= SetPause;


    private void SetPause(bool isPaused)
    {
        this.isPaused = isPaused;

        if (!isPaused)
        {
            if (awatingUpdate) StartCoroutine(AfterPause());
            awatingUpdate = false;
        }
    }

    IEnumerator AfterPause()
    {
        yield return delayAfterPause;
        ShowUpdate();
    }


    public void ShowUpdate()
    {
        if (!isPaused)
        {
            ResetUpdate();
            reminder = StartCoroutine(Remind());

            Sequence sequence = DOTween.Sequence();
            sequence.Append(rectTransform.DOScale(1.5f, 0.4f));
            sequence.Append(rectTransform.DOScale(1, 0.4f));

            sequence.Play();
        }
        else awatingUpdate = true;
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

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect))]
public class AutoScrollDown : MonoBehaviour
{

    WaitForEndOfFrame waitFrame = new WaitForEndOfFrame();

    ScrollRect scrollRect;

    private void Awake() => scrollRect = GetComponent<ScrollRect>();

    public void Scroll() => StartCoroutine(ScrollingToBottom());

    IEnumerator ScrollingToBottom()
    {
        yield return waitFrame;
        scrollRect.verticalNormalizedPosition = 0;
    }
}

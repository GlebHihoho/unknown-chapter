using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonCursor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    Button button;

    [SerializeField] CursorData unavaialableCursor;

    private void Awake() => button = GetComponent<Button>();



    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!button.interactable) GameCursor.instance.SetCursor(unavaialableCursor);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!button.interactable) GameCursor.instance.RestorePreviousCursor();
    }
}

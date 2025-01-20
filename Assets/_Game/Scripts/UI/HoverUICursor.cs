using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(UIBehaviour))]
public class HoverUICursor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField] UIBehaviour control;

    [SerializeField] CursorData interactiveCursor;
    [SerializeField] CursorData unavaialableCursor;


    bool isPointerOver = false;

    private void Awake()
    {
        if (control == null) control = GetComponent<UIBehaviour>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (control.enabled) GameCursor.instance.SetCursor(interactiveCursor);
        else GameCursor.instance.SetCursor(unavaialableCursor);

        isPointerOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GameCursor.instance.SetCursor(null);

        isPointerOver = false;
    }


    private void OnDisable()
    {
        if (isPointerOver)
        {
            GameCursor.instance.SetCursor(null);
            isPointerOver = false;
        }
    }

}

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonCursor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    Button button;

    [SerializeField] CursorData interactiveCursor;
    [SerializeField] CursorData unavaialableCursor;

    bool isPointerOver = false;

    private void Awake() => button = GetComponent<Button>();



    public void OnPointerEnter(PointerEventData eventData)
    {
        if (button.interactable) GameCursor.instance.SetCursor(interactiveCursor);
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
            isPointerOver= false;
        }
    }
}

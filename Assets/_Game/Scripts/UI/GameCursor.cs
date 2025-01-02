using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameCursor : MonoBehaviour
{

    public static GameCursor instance;

    [SerializeField] RectTransform parent;
    [SerializeField] RectTransform cursor;

    [SerializeField] Image image;

    [SerializeField] CursorData defaultCursor;

    Sprite prevCursor;


    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
       
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {      
        Cursor.visible = false;
        image.sprite = defaultCursor.Sprite;
    }


    public void SetCursor(CursorData cursor)
    {
        prevCursor = image.sprite;

        if (cursor != null) image.sprite = cursor.Sprite;
        else image.sprite = defaultCursor.Sprite;
    }

    public void RestorePreviousCursor()
    {
        if (prevCursor != null) image.sprite = prevCursor;
    }


    // Update is called once per frame
    void Update()
    {
        Vector3 newPos;
        newPos.x = Pointer.current.position.x.ReadValue();
        newPos.y = Pointer.current.position.y.ReadValue();
        newPos.z = 0;

        Vector2 pos; 
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parent, newPos, null, out pos);
        cursor.anchoredPosition = pos;

    }


}

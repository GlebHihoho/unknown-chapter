using UnityEngine;
using UnityEngine.UI;

public class ScrollbarHandleSizeController : MonoBehaviour
{
    private float topNormalizedValue = 0f; // Верхняя граница полосы прокрутки.

    public Scrollbar scrollbar; // Перетащите компонент Scrollbar с полоской прокрутки в это поле в редакторе Unity.

    void Start()
    {
        scrollbar.value = topNormalizedValue;
    }
}

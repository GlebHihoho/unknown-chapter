using UnityEngine;
using UnityEngine.UI;

// TODO: UI?
// нужен ли этот скрипт
public class ScrollbarHandleSizeController : MonoBehaviour
{
    public Scrollbar scrollbar; // Перетащите компонент Scrollbar с полоской прокрутки в это поле в редакторе Unity.
    
    private float topNormalizedValue = 0f; // Верхняя граница полосы прокрутки.
    
    void Start()
    {
        scrollbar.value = topNormalizedValue;
    }
}

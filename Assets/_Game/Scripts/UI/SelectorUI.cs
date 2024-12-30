using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SelectorUI : MonoBehaviour
{

    [SerializeField] Button buttonLeft;
    [SerializeField] Button buttonRight;

    [SerializeField] string[] values;
    [SerializeField] TextMeshProUGUI value;

    [SerializeField] GameObject indicatorPanel;
    [SerializeField] Image selectorItemPrefab;

    Image[] selectorItems;

    [SerializeField] Sprite activeItem;
    [SerializeField] Sprite inactiveItem;


    public UnityEvent<int> OnChangeSelection;

    bool initialized = false;

    int index;

    private void Awake()
    {
        if (values.Length > 0)
        {
            buttonLeft.onClick.AddListener(SelectPrev);
            buttonRight.onClick.AddListener(SelectNext);

            value.text = values[0];

            selectorItems = new Image[values.Length];

            for (int i = 0; i < values.Length; i++)
            {
                Image image = Instantiate(selectorItemPrefab, indicatorPanel.transform);
                selectorItems[i] = image;
            }

            selectorItems[0].sprite = activeItem;
        }

        initialized = true;
    }


    private void SelectPrev()
    {
        selectorItems[index].sprite = inactiveItem;

        index--;
        if (index < 0) index = values.Length - 1;
        value.text = values[index];

        selectorItems[index].sprite = activeItem;

        OnChangeSelection.Invoke(index);
    }


    private void SelectNext()
    {
        selectorItems[index].sprite = inactiveItem;

        index++;
        if (index >= values.Length) index = 0;
        value.text = values[index];

        selectorItems[index].sprite = activeItem;

        OnChangeSelection.Invoke(index);
    }

    public void Select(int index)
    {

        if (!initialized) return;

        selectorItems[this.index].sprite = inactiveItem;

        index = Mathf.Clamp(index, 0, values.Length - 1);
        value.text = values[index];
        selectorItems[index].sprite = activeItem;
    }
}

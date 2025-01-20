using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderUI : MonoBehaviour
{

    [SerializeField] Slider slider;
    [SerializeField] TextMeshProUGUI value;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() => SetValue(slider.value);


    public void SetValue(float value)
    {
        int percent = Mathf.RoundToInt(value * 100);
        this.value.text = percent.ToString();
    }
}

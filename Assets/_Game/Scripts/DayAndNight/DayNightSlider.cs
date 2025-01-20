using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class DayNightSlider : MonoBehaviour
{

    Slider slider;
    [SerializeField] DayAndNight dayAndNight;

    private void Awake() => slider = GetComponent<Slider>();

    private void OnEnable() => slider.value = dayAndNight.CurrentTime;
}

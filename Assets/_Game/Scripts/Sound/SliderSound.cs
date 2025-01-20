using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SliderSound : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{

    Slider slider;

    [SerializeField] MenuSoundData sounds;

    bool isUsed = false;

    float value = 0;


    private void Awake()
    {
        slider = GetComponent<Slider>();
        value = slider.value;
    }


    private void Update()
    {
        if (isUsed)
        {
            if (slider.value != value)
            {
                SoundManager.instance.PlayContinuousEffect(sounds.Slider);
                value = slider.value;
            }
            else
            {
                SoundManager.instance.StopContinuousEffect(sounds.Slider);
            }
        }
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        SoundManager.instance.StopContinuousEffect(sounds.Slider);
        isUsed = false;
    }

    public void OnPointerDown(PointerEventData eventData) => isUsed = true;
}

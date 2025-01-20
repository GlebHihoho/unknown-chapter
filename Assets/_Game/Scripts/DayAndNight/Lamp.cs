using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour
{
    [SerializeField] GameObject lamp;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnEnable()
    {
        DayAndNight.OnNightStarts += LampOn;
        DayAndNight.OnNightEnds += LampOff;

        LampOff();
    }


    private void OnDisable()
    {
        DayAndNight.OnNightStarts -= LampOn;
        DayAndNight.OnNightEnds -= LampOff;
    }

    void LampOn() => lamp.SetActive(true);

    void LampOff() => lamp.SetActive(false);
}

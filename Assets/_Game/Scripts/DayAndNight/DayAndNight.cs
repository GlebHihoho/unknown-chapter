using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DayAndNight : MonoBehaviour
{

    [SerializeField] DayAndNightSettings settings;

    [SerializeField] Light sun;
    [SerializeField] Transform sunPivot;


    float currentTime = 0;

    public static event Action OnNightStarts;
    public static event Action OnNightEnds;

    bool isNight = false;

    bool isPaused = false;

    float prevAngle = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isPaused = !isPaused;
        }

        if (isPaused) return;

        currentTime += Time.deltaTime / settings.DayDuration;
        currentTime = Mathf.Repeat(currentTime, 1);

        float sunAngle = 365 * currentTime;


        if (Mathf.Abs(sunAngle - prevAngle) >= settings.SunAngleThreshold)
        {
            sunPivot.rotation = Quaternion.Euler(sunAngle, 0, 0);
            prevAngle = sunAngle;
        }

       
        RenderSettings.fogDensity = settings.FogDensity.Evaluate(currentTime);
        RenderSettings.fogColor = settings.FogGradient.Evaluate(currentTime);


        RenderSettings.ambientLight = settings.AmbientGradient.Evaluate(currentTime);
        sun.color = settings.SunGradient.Evaluate(currentTime);
        sun.intensity = settings.SunPower.Evaluate(currentTime);

        if (currentTime >= settings.NightStarts && currentTime <= settings.NightEnds && !isNight)
        {
            OnNightStarts?.Invoke();
            isNight = true;
        }
        else if (!(currentTime >= settings.NightStarts && currentTime <= settings.NightEnds) && isNight)
        {
            OnNightEnds?.Invoke();
            isNight = false;
        }
    }
}

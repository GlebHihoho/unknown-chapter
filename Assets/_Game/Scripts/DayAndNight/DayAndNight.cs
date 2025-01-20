using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DayAndNight : MonoBehaviour, ISaveable
{

    [SerializeField] DayAndNightSettings settings;

    [SerializeField] Light sun;
    [SerializeField] Transform sunPivot;


    float currentTime = 0;
    public float CurrentTime => currentTime;

    public static event Action OnNightStarts;
    public static event Action OnNightEnds;

    bool isNight = false;

    bool isPaused = false;

    bool manualMode = false;

    float prevAngle = 0;


    private void OnEnable() => Pause.OnPause += SetPause;
    private void OnDisable() => Pause.OnPause -= SetPause;

    private void SetPause(bool isPaused) => this.isPaused = isPaused;


    // Update is called once per frame
    void Update()
    {

        if (isPaused || manualMode) return;

        currentTime += Time.deltaTime / settings.DayDuration;
        currentTime = Mathf.Repeat(currentTime, 1);

        UpdateCycle();
    }


    public void SetTime(float timeOfDay)
    {
        if (!manualMode) return;

        currentTime = Mathf.Clamp(timeOfDay, 0, 1);

        UpdateCycle();
    }


    private void UpdateCycle()
    {
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

    public void ToggleManual(bool isToggled)
    {
        manualMode = isToggled;
    }

    public void Save(ref SaveData.Save save)
    {
        save.timeOfDay = currentTime;
    }

    public void Load(SaveData.Save save)
    {
        currentTime = Mathf.Clamp(save.timeOfDay, 0, 1);
    }
}

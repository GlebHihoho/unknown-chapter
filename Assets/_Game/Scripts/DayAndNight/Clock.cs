using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{

    [SerializeField] DayAndNightSettings settings;

    [SerializeField] Transform hours;
    [SerializeField] Transform minutes;

    float hoursSpeed;
    float minutesSpeed;

    bool isPaused = false;

    private void OnEnable() => Pause.OnPause += SetPause;
    private void OnDisable() => Pause.OnPause -= SetPause;

    private void SetPause(bool isPaused) => this.isPaused = isPaused;


    // Start is called before the first frame update
    void Start()
    {
        hoursSpeed = 360 / settings.DayDuration * 2;
        minutesSpeed = hoursSpeed * 60;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPaused) return;

        hours.Rotate(0, hoursSpeed * Time.deltaTime, 0);
        minutes.Rotate(0, minutesSpeed * Time.deltaTime, 0);
    }

   
}

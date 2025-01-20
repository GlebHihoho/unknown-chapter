using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{

    [SerializeField] Light light;
    [SerializeField] ParticleSystem flame;
    [SerializeField] ParticleSystem bugs;


    [SerializeField, Range(0, 5)] float minDelay = 0;
    [SerializeField, Range(0, 5)] float maxDelay = 3;

    enum Status { Inactive, Burning, Paused}

    Status status = Status.Inactive;
    Status prevStatus = Status.Inactive;


    private void OnEnable()
    {
        DayAndNight.OnNightStarts += ActivateWithDelay;
        DayAndNight.OnNightEnds += DesactivateWithDelay;

        Pause.OnPause += PauseTorch;

        DesactivateTorch();
    }


    private void OnDisable()
    {
        DayAndNight.OnNightStarts -= ActivateWithDelay;
        DayAndNight.OnNightEnds -= DesactivateWithDelay;

        Pause.OnPause -= PauseTorch;
    }


    private void ActivateWithDelay()
    {
        StartCoroutine(ActivateDelayed());
    }


    private void DesactivateWithDelay()
    {
        StartCoroutine(DesactivateDelayed());
    }

    IEnumerator ActivateDelayed()
    {
        yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
        ActivateTorch();
    }


    IEnumerator DesactivateDelayed()
    {
        yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
        DesactivateTorch();
    }

    private void ActivateTorch()
    {
        status = Status.Burning;

        light.enabled = true;
        flame.Play();
        bugs.Play();
    }


    private void DesactivateTorch()
    {
        status = Status.Inactive;

        light.enabled = false;
        flame.Stop();
        bugs.Stop();
    }


    private void PauseTorch(bool isPause)
    {

        if (isPause)
        {
            if (status == Status.Burning)
            {
                flame.Pause();
                bugs.Pause();
            }

            prevStatus = status;
            status = Status.Paused;
        }
        else
        {
            status = prevStatus;

            if (status == Status.Burning)
            {
                flame.Play();
                bugs.Play();
            }
        }

    }

}

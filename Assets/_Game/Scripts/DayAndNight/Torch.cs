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
        DayAndNight.OnNightStarts += ActivateWithDelay;
        DayAndNight.OnNightEnds += DesactivateWithDelay;

        DesactivateTorch();
    }


    private void OnDisable()
    {
        DayAndNight.OnNightStarts -= ActivateWithDelay;
        DayAndNight.OnNightEnds -= DesactivateWithDelay;
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
        light.enabled = true;
        flame.Play();
        bugs.Play();
    }


    private void DesactivateTorch()
    {
        light.enabled = false;
        flame.Stop();
        bugs.Stop();
    }
}

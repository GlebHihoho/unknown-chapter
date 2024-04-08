using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockArrow : MonoBehaviour
{

    [SerializeField] Material dayMaterial;
    [SerializeField] Material nightMaterial;

    MeshRenderer renderer;

    private void Awake() => renderer = GetComponent<MeshRenderer>();

    private void OnEnable()
    {
        DayAndNight.OnNightStarts += SetNight;
        DayAndNight.OnNightEnds += SetDay;
    }

    private void OnDisable()
    {
        DayAndNight.OnNightStarts -= SetNight;
        DayAndNight.OnNightEnds -= SetDay;
    }


    private void SetNight() => renderer.sharedMaterial = nightMaterial;

    private void SetDay() => renderer.sharedMaterial = dayMaterial;
}

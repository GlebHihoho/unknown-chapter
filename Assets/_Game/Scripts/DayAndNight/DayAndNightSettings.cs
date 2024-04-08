using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="DayAndNightSettings",menuName ="Gameplay/Day and Night Settings")]
public class DayAndNightSettings : ScriptableObject
{
    [Header("Fog")]
    [SerializeField] Gradient fogGradient;
    public Gradient FogGradient => fogGradient;

    [SerializeField] AnimationCurve fogDensity;
    public AnimationCurve FogDensity => fogDensity;

    [Header("Light")]
    [SerializeField] Gradient ambientGradient;
    public Gradient AmbientGradient => ambientGradient;

    [SerializeField] Gradient sunGradient;
    public Gradient SunGradient => sunGradient;

    [SerializeField] AnimationCurve sunPower;
    public AnimationCurve SunPower => sunPower;

    [SerializeField, Range(0, 0.5f)] float sunAngleThreshold = 0.1f;
    public float SunAngleThreshold => sunAngleThreshold;

    [Header("Duration")]
    [SerializeField] float dayDuration = 1;
    public float DayDuration => dayDuration;

    [Header("Night")]
    [SerializeField, Range(0, 1)] float nightStarts = 0.3f;
    public float NightStarts => nightStarts;

    [SerializeField, Range(0, 1)] float nightEnds = 0.7f;
    public float NightEnds => nightEnds;

}

using System;
using UnityEngine;

public class MusicalBottle : Interactable
{
    [SerializeField] AudioClip clip;

    public static event Action<AudioClip> OnClipCalled;

    protected override void PerfomInteraction()
    {
        base.PerfomInteraction();

        OnClipCalled?.Invoke(clip);
    }
}

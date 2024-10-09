using System;
using UnityEngine;

[RequireComponent(typeof(TriggerVisuals))]
public class MapCoverTrigger : MonoBehaviour
{

    [SerializeField] string zoneName;

    public static event Action<string> OnEnter;

    private void OnTriggerEnter(Collider other) => OnEnter?.Invoke(zoneName);

}

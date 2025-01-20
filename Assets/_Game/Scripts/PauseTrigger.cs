using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseTrigger : MonoBehaviour
{

    [SerializeField] bool affectKeys = true;

    private void OnEnable()
    {
        if (Pause.instance != null)
            Pause.instance.SetPause(true, affectKeys);
    }

    private void OnDisable()
    {
        if (Pause.instance != null)
            Pause.instance.SetPause(false, affectKeys);
    }
}

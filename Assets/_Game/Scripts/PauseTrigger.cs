using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseTrigger : MonoBehaviour
{
    private void OnEnable() => Pause.instance.SetPause(true);

    private void OnDisable() => Pause.instance.SetPause(false);
}

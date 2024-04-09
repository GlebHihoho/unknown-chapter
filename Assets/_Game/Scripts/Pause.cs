using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Pause : MonoBehaviour
{

    public static event Action<bool> OnPause;

    bool isPaused = false;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) SetPause(!isPaused);
    }


    public void SetPause(bool isPaused)
    {
        this.isPaused = isPaused;
        OnPause?.Invoke(isPaused);
    }
}

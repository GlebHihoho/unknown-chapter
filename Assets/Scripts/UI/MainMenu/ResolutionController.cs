using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionController : MonoBehaviour
{
    public void OnValueChanged(int val)
    {
        switch (val)
        {
            case 0:
                Screen.SetResolution(2960, 1440, true);
                break;
            case 1:
                Screen.SetResolution(2560, 1440, true);
                break;
            case 2:
                Screen.SetResolution(2160, 1080, true);
                break;
            case 3:
                Screen.SetResolution(1920, 1080, true);
                break;
            case 4:
                Screen.SetResolution(1600, 900, true);
                break;
            case 5:
                Screen.SetResolution(1366, 768, true);
                break;
            case 6:
                Screen.SetResolution(1280, 720, true);
                break;
        }
    }
}

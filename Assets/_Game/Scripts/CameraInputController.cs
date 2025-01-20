using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.AI;

public class CameraInputController : MonoBehaviour
{
    CinemachineInputAxisController controller;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CinemachineInputAxisController>();
        controller.enabled = false;


        GameControls.instance.OnCameraRotateStarted += CameraRotateStarted;
        GameControls.instance.OnCameraRotateEnded += CameraRotateEnded;
    }



    private void CameraRotateStarted()
    {
        controller.enabled = true;
    }

    private void CameraRotateEnded()
    {
        controller.enabled=false;
    }

}

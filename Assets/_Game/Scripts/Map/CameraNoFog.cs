using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;


public class CameraNoFog : MonoBehaviour
{

    bool isFogUsed;
    new Camera camera;

    // Start is called before the first frame update
    void Awake()
    {
        isFogUsed = RenderSettings.fog;
        camera = GetComponent<Camera>();
    }


    private void OnEnable()
    {
        RenderPipelineManager.beginCameraRendering += BeginRender;
        RenderPipelineManager.endCameraRendering += EndRender;
    }

    private void OnDisable()
    {
        RenderPipelineManager.beginCameraRendering -= BeginRender;
        RenderPipelineManager.endCameraRendering -= EndRender;
    }

    private void BeginRender(ScriptableRenderContext arg1, Camera arg2)
    {
        if (arg2 == camera) RenderSettings.fog = false;
    }

    private void EndRender(ScriptableRenderContext arg1, Camera arg2)
    {
        if (arg2 == camera) RenderSettings.fog = isFogUsed;
    }


    
}

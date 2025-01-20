using TMPro;
using UnityEngine;

public class FPS_UI : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI counter;


    void Start() => FPS_Counter.OnFPSUpdate += FPSUpdate;
    private void OnDestroy() => FPS_Counter.OnFPSUpdate -= FPSUpdate;

    private void FPSUpdate(int fps) => counter.text = $"FPS: {fps}";


}

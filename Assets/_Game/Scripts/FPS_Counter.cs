using System;
using UnityEngine;

public class FPS_Counter : MonoBehaviour
{

    int frameIndex;
    float[] frameRates;

  
    public static event Action<int> OnFPSUpdate;

    private void Awake() => frameRates = new float[50];

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        frameRates[frameIndex] = Time.deltaTime;
        frameIndex = (frameIndex + 1) % frameRates.Length;

        OnFPSUpdate?.Invoke(CalculateFPS());
    }

    int CalculateFPS()
    {
        float total = 0;

        foreach (float deltaTime in frameRates)
        {
            total += deltaTime;
        }

        return Mathf.RoundToInt(frameRates.Length / total);
    }
}

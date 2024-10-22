using Cinemachine;
using UnityEngine;

public class WavesSound : MonoBehaviour
{
    [Header("Path")]
    [SerializeField] CinemachinePathBase path;
    [SerializeField] GameObject player;

    float position;
    CinemachinePathBase.PositionUnits positionUnits = CinemachinePathBase.PositionUnits.PathUnits;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SetCartPosition(path.FindClosestPoint(player.transform.position, 0, -1, 10));
    }

    private void SetCartPosition(float distanceAlongPath)
    {
        position = path.StandardizeUnit(distanceAlongPath, positionUnits);
        transform.position = path.EvaluatePositionAtUnit(position, positionUnits);
        transform.rotation = path.EvaluateOrientationAtUnit(position, positionUnits);
    }
}

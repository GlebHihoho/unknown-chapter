using Unity.Cinemachine;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;

public class WavesSound : MonoBehaviour
{
    [Header("Path")]
    [SerializeField] SplineContainer path;
    [SerializeField] GameObject player;


    // Update is called once per frame
    void Update()
    {

        Vector3 localPoint = path.transform.InverseTransformPoint(player.transform.position);

        SplineUtility.GetNearestPoint(path.Spline, localPoint, out float3 newPosition, out float t);

        transform.position = path.transform.TransformPoint(newPosition);

    }
}

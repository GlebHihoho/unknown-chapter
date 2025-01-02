using UnityEngine;

public class MapMarkerActivator : MonoBehaviour
{

    [SerializeField] MapMarker marker;


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerSave playerSave))
        {
            marker.Activate();
            enabled = false;
        }       
    }
}

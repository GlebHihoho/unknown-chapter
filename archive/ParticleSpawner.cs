using UnityEngine;
using UnityEngine.Serialization;

public class ParticleSpawner : MonoBehaviour
{
    [FormerlySerializedAs("particleSystemPrefab")] public GameObject _particleSystemPrefab;
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            int groundLayerMask = LayerMask.GetMask("Ground"); // Получить маску слоя "Ground"

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayerMask))
            {
                Vector3 spawnPosition = hit.point;
                GameObject particleSystem = Instantiate(_particleSystemPrefab, spawnPosition + new Vector3(0f, 0.2f, 0f), Quaternion.Euler(90f, 0f, 0f));
            }
        }
    }
}

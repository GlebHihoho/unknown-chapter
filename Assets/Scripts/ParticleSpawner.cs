using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: delete????
public class ParticleSpawner : MonoBehaviour
{
    public GameObject particleSystemPrefab;
    
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
                GameObject particleSystem = Instantiate(particleSystemPrefab, spawnPosition + new Vector3(0f, 0.2f, 0f), Quaternion.Euler(90f, 0f, 0f));
                // Настройте систему частиц по вашему усмотрению
            }
        }
    }
}

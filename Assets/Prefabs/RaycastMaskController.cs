using UnityEngine;

public class RaycastMaskController : MonoBehaviour
{
    public Shader raycastMaskShader; // Ссылка на шейдер с эффектом маски
    private Material material; // Материал объекта
    public Transform player; // Ссылка на игрока
    public float maskRadius = 0.1f; // Радиус маски
    public LayerMask maskLayer; // Слой для рейкаста
    public float raycastHeightOffset = 1.0f; // Смещение луча вверх
    
    
    public Material maskedMaterial; // Ссылка на материал с круговой маской
    private MeshRenderer meshRenderer; // Компонент MeshRenderer объекта

    private void Start()
    {
        // // Получаем компонент Mesh Renderer и его материал
        // Renderer meshRenderer = GetComponent<Renderer>();
        // material = meshRenderer.material;
        //
        // material.shader = raycastMaskShader; // Применяем шейдер
        // material.SetFloat("_MaskRadius", maskRadius);
        
        // Получаем компонент рендерера объекта
        meshRenderer = GetComponent<MeshRenderer>();

        // Применяем изначальный материал к объекту
        meshRenderer.material = maskedMaterial;

        // Установим радиус маски в шейдере
        maskedMaterial.SetFloat("_MaskRadius", maskRadius);
    }

    private void Update()
    {
        Vector3 cameraPosition = Camera.main.transform.position;
        Vector3 playerPosition = player.position + Vector3.up * raycastHeightOffset;
        Vector3 rayDirection = playerPosition - cameraPosition;

        RaycastHit hit;
        if (Physics.Raycast(cameraPosition, rayDirection, out hit, rayDirection.magnitude,maskLayer))
        {
            // Применяем новый материал с круговой маской к объекту
            meshRenderer.material = maskedMaterial;
            maskedMaterial.SetVector("_MaskPosition", hit.point);
            print("да");
        }
        else
        {
            meshRenderer.material = maskedMaterial;
            maskedMaterial.SetVector("_MaskPosition", Vector4.one * -1);
            print("нет");
        }
    }
    
    // private void OnDrawGizmos()
    // {
    //     Vector3 cameraPosition = Camera.main.transform.position;
    //     Vector3 playerPosition = player.position + Vector3.up * raycastHeightOffset;
    //     Vector3 rayDirection = playerPosition - cameraPosition;
    //     Gizmos.color = Color.green;
    //     Gizmos.DrawRay(cameraPosition, rayDirection);
    // }
}
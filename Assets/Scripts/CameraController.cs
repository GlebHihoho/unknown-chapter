using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _player;
    [SerializeField] private float sensitivity = 2f;
    [SerializeField] private float zoomSpeed = 2f;
    [SerializeField] private float minZoomDistance = 1f;
    [SerializeField] private float maxZoomDistance = 10f;
    [SerializeField] private float initialCameraDistance = 5f; // Расстояние камеры при начальном зуме

    private float currentZoomDistance = 5f; // Текущее расстояние камеры
    private float zoomFactor = 1f; // Фактор зума
    private Vector3 cameraOffset; // Смещение камеры относительно персонажа
    private float rotationY = 45f; // Угол обзора по горизонтали

    private void Start()
    {
        // Определяем начальное смещение камеры от персонажа
        cameraOffset = _camera.transform.position - _player.position;

        // Устанавливаем начальное расстояние камеры
        currentZoomDistance = initialCameraDistance;
    }

    private void Update()
    {
        // Получаем ввод с мыши для вращения по горизонтали
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        rotationY += mouseX;

        // Поворачиваем камеру вокруг персонажа по горизонтали
        Quaternion cameraRotation = Quaternion.Euler(0f, rotationY, 0f);

        // Рассчитываем новое расстояние и смещение камеры
        currentZoomDistance -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        currentZoomDistance = Mathf.Clamp(currentZoomDistance, minZoomDistance, maxZoomDistance);
        zoomFactor = Mathf.Lerp(zoomFactor, currentZoomDistance / initialCameraDistance, Time.deltaTime * 5f);
        Vector3 newCameraOffset = cameraOffset * zoomFactor;

        // Применяем новое положение камеры
        _camera.transform.position = _player.position + cameraRotation * newCameraOffset;
        _camera.transform.LookAt(_player.position);
    }
}
using UnityEngine;
using UnityEngine.Serialization;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _player;
    [FormerlySerializedAs("sensitivity")] [SerializeField] private float _sensitivity = 2f;
    [FormerlySerializedAs("zoomSpeed")] [SerializeField] private float _zoomSpeed = 2f;
    [FormerlySerializedAs("minZoomDistance")] [SerializeField] private float _minZoomDistance = 1f;
    [FormerlySerializedAs("maxZoomDistance")] [SerializeField] private float _maxZoomDistance = 10f;
    [FormerlySerializedAs("initialCameraDistance")] [SerializeField] private float _initialCameraDistance = 5f;

    private float currentZoomDistance = 5f; // Текущее расстояние камеры
    private float zoomFactor = 1f; // Фактор зума
    private Vector3 cameraOffset; // Смещение камеры относительно персонажа
    private float rotationY = 45f; // Угол обзора по горизонтали

    private Quaternion cameraRotation;
    private Vector3 newCameraOffset;

    private void Start()
    {
        cameraOffset = _camera.transform.position - _player.position;

        // Устанавливаем начальное расстояние камеры
        currentZoomDistance = _initialCameraDistance;
        
        _camera.transform.position = _player.position + cameraRotation * (cameraOffset * zoomFactor);
    }

    private void Update()
    {
        _camera.transform.position = _player.position + cameraRotation * (cameraOffset * zoomFactor);

        float scrollWheelInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollWheelInput != 0f)
        {
            CameraZoom();
        }
        
        if (Input.GetMouseButton(2))
        {
            CameraScroll();
        }
        _camera.transform.LookAt(_player.position);

    }

    private void CameraScroll()
    {
        // Получаем ввод с мыши для вращения по горизонтали
        float mouseX = Input.GetAxis("Mouse X") * _sensitivity;
        rotationY += mouseX;

        // Поворачиваем камеру вокруг персонажа по горизонтали
        cameraRotation = Quaternion.Euler(0f, rotationY, 0f);

        // Применяем новое положение камеры
        _camera.transform.position = _player.position + cameraRotation * (cameraOffset * zoomFactor);
        _camera.transform.LookAt(_player.position);
    }

    private void CameraZoom()
    {
        // Рассчитываем новое расстояние и смещение камеры
        float zoomDelta = Input.GetAxis("Mouse ScrollWheel") * _zoomSpeed;
        currentZoomDistance -= zoomDelta;
        currentZoomDistance = Mathf.Clamp(currentZoomDistance, _minZoomDistance, _maxZoomDistance);
        zoomFactor = currentZoomDistance / _initialCameraDistance;

        // Пересчитываем смещение камеры с учетом нового расстояния
        Vector3 newCameraOffset = cameraOffset * zoomFactor;

        // Применяем новое положение камеры
        _camera.transform.position = _player.position + cameraRotation * (cameraOffset * zoomFactor);
        _camera.transform.LookAt(_player.position);
    }
}
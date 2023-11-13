using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _player;
    [SerializeField] private float _sensitivity = 2f;
    [SerializeField] private float _zoomSpeed = 2f;
    [SerializeField] private float _minZoomDistance = 1f;
    [SerializeField] private float _maxZoomDistance = 10f;
    [SerializeField] private float _initialCameraDistance = 5f;

    private float _currentZoomDistance = 5f; // Текущее расстояние камеры
    private float _zoomFactor = 1f; // Фактор зума
    private Vector3 _cameraOffset; // Смещение камеры относительно персонажа
    private float _rotationY = 45f; // Угол обзора по горизонтали
    private Quaternion _cameraRotation;

    private void Start()
    {
        _cameraOffset = _camera.transform.position - _player.position;
        _currentZoomDistance = _initialCameraDistance;
        _camera.transform.position = _player.position + _cameraRotation * (_cameraOffset * _zoomFactor);
    }

    private void Update()
    {
        _camera.transform.position = _player.position + _cameraRotation * (_cameraOffset * _zoomFactor);
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
        float mouseX = Input.GetAxis("Mouse X") * _sensitivity;
        _rotationY += mouseX;
        _cameraRotation = Quaternion.Euler(0f, _rotationY, 0f);
        UpdateCameraPosition();
    }

    private void CameraZoom()
    {
        float zoomDelta = Input.GetAxis("Mouse ScrollWheel") * _zoomSpeed;
        _currentZoomDistance -= zoomDelta;
        _currentZoomDistance = Mathf.Clamp(_currentZoomDistance, _minZoomDistance, _maxZoomDistance);
        _zoomFactor = _currentZoomDistance / _initialCameraDistance;
        UpdateCameraPosition();
    }
    
    private void UpdateCameraPosition()
    {
        _camera.transform.position = _player.position + _cameraRotation * (_cameraOffset * _zoomFactor);
        _camera.transform.LookAt(_player.position);
    }
}

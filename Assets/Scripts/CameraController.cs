using System.Collections;
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
    [SerializeField] private LayerMask _obstacleMask; // Добавляем маску препятствий

    [SerializeField] private Transform _targetObject;
    [SerializeField] private LayerMask _wallMask;

    private float _currentZoomDistance = 5f; // Текущее расстояние камеры
    private float _zoomFactor = 1f; // Фактор зума
    private Vector3 _cameraOffset; // Смещение камеры относительно персонажа
    private float _rotationY = 45f; // Угол обзора по горизонтали
    private Quaternion _cameraRotation;

    private Camera _mainCamera;
    private bool _isZooming = false; // Флаг, указывающий, происходит ли зум в данный момент
    private float _previousZoomDistance; // Переменная для хранения предыдущего расстояния зума

    private bool
        _hasSavedInitialZoom = false; // Флаг для отслеживания того, было ли уже сохранено начальное значение зума

    private bool isCameraRotating = false;
    private bool isPaused = false;

    private void Start()
    {
        _cameraOffset = _camera.transform.position - _player.position;
        _currentZoomDistance = _initialCameraDistance;
        _camera.transform.position = _player.position + _cameraRotation * (_cameraOffset * _zoomFactor);

        _mainCamera = GetComponent<Camera>();

        GameControls.instance.OnCameraRotateStarted += CameraRotateStarted;
        GameControls.instance.OnCameraRotateEnded += CameraRotateEnded;

        Pause.OnPause += SetPause;
    }


    private void OnDestroy()
    {
        GameControls.instance.OnCameraRotateStarted -= CameraRotateStarted;
        GameControls.instance.OnCameraRotateEnded -= CameraRotateEnded;

        Pause.OnPause -= SetPause;
    }


    private void CameraRotateStarted() => isCameraRotating = true;
    private void CameraRotateEnded() => isCameraRotating = false;

    private void SetPause(bool paused) => isPaused = paused;


    private void Update()
    {
        _camera.transform.position = _player.position + _cameraRotation * (_cameraOffset * _zoomFactor);
        float scrollWheelInput = Input.GetAxis("Mouse ScrollWheel");

        if (!isPaused && scrollWheelInput != 0f)
        {
            CameraZoom();
        }

        CameraScroll();

        CheckObstaclesBetweenCameraAndPlayer();
        _camera.transform.LookAt(_player.position);
    }

    private void CameraScroll()
    {
        if (!isPaused && isCameraRotating)
        {

            float mouseX = Input.GetAxis("Mouse X") * _sensitivity;
            _rotationY += mouseX;
            _cameraRotation = Quaternion.Euler(0f, _rotationY, 0f);
            UpdateCameraPosition();
        }
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

    public void CheckObstaclesBetweenCameraAndPlayer()
    {
        Vector3 offset = _targetObject.position - transform.position;
        RaycastHit[] hitObjects = Physics.RaycastAll(transform.position, offset, offset.magnitude, _wallMask);

        if (hitObjects.Length >= 1)
        {
            if (!_isZooming)
            {
                StartCoroutine(ZoomIn());
            }
        }
        else
        {
            if (_isZooming)
            {
                StopCoroutine(ZoomIn());
                _isZooming = false;

                if (_hasSavedInitialZoom)
                {
                    _currentZoomDistance = _previousZoomDistance;
                    _zoomFactor = _currentZoomDistance / _initialCameraDistance;
                    UpdateCameraPosition();
                    _hasSavedInitialZoom = false;
                }
            }
        }
    }

    private IEnumerator ZoomIn()
    {
        _isZooming = true;

        if (!_hasSavedInitialZoom)
        {
            _previousZoomDistance = _currentZoomDistance;
            _hasSavedInitialZoom = true;
        }

        while (true)
        {
            _currentZoomDistance -= _zoomSpeed * (Time.deltaTime * 4);
            _currentZoomDistance = Mathf.Max(_currentZoomDistance, _minZoomDistance);
            _zoomFactor = _currentZoomDistance / _initialCameraDistance;
            UpdateCameraPosition();
            Vector3 newOffset = _targetObject.position - transform.position;
            RaycastHit[] newHitObjects = Physics.RaycastAll(transform.position, newOffset, newOffset.magnitude, _wallMask);

            if (newHitObjects.Length == 0)
            {
                break;
            }

            yield return null;
        }
        _isZooming = false;
    }
}

using UnityEngine;

// TODO: удалим?? либо вынести в папку архив
public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _player;
    [SerializeField] private float sensitivity = 2f;
    [SerializeField] private float zoomSpeed = 2f;
    [SerializeField] private float minZoomDistance = 1f;
    [SerializeField] private float maxZoomDistance = 10f;
    [SerializeField] private float initialCameraHeight = 5f; // Высота камеры над персонажем при начальном зуме
    [SerializeField] private float zoomExponent = 2f; // Экспонента для увеличения высоты при отдалении

    private Vector3 offset;
    private float rotationY = 45f; // Устанавливаем начальный угол обзора в 45 градусов по Y
    private float currentZoomDistance = 5f; // Начальное значение расстояния камеры
    private float cameraHeight; // Текущая высота камеры

    private void Start()
    {
        offset = Quaternion.Euler(0f, rotationY, 0f) * new Vector3(0f, initialCameraHeight, -currentZoomDistance);
        _camera.transform.position = _player.position + offset;
        cameraHeight = initialCameraHeight;
    }

    private void Update()
    {
        // Получаем ввод с мыши для вращения по горизонтали
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        rotationY += mouseX;

        // Поворачиваем камеру вокруг персонажа по горизонтали
        Quaternion cameraRotation = Quaternion.Euler(0f, rotationY, 0f);
        Vector3 newPosition = _player.position + cameraRotation * offset;

        // Применяем новую позицию
        _camera.transform.position = newPosition;

        // Зум с помощью колесика мыши
        float zoomInput = Input.GetAxis("Mouse ScrollWheel");
        currentZoomDistance -= zoomInput * zoomSpeed;
        currentZoomDistance = Mathf.Clamp(currentZoomDistance, minZoomDistance, maxZoomDistance);

        // Увеличиваем координату Y с использованием экспоненты
        cameraHeight = initialCameraHeight + Mathf.Pow(currentZoomDistance, zoomExponent);

        // Обновляем расстояние камеры от персонажа
        offset = Quaternion.Euler(0f, rotationY, 0f) * new Vector3(0f, cameraHeight, -currentZoomDistance);

        // Применяем новое расстояние камеры и направление камеры
        _camera.transform.position = _player.position + offset;
        _camera.transform.LookAt(_player.position);
    }

}

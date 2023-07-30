using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField] private float _speedWalk = 4f;
    [SerializeField] private float _speedSprint = 12f;
    [SerializeField] private float _rotationSpeed = 5f;
    [SerializeField] private float _zoomSpeed = 5f; // Adjust this to control camera zoom speed
    [SerializeField] private float _orthographicSizeMin = 2f;
    [SerializeField] private float _orthographicSizeMax = 15f;

    public float _moveSpeed = 0f;

    private Transform characterTransform;
    private Transform cameraTransform;
    private bool isMouseWheelDown = false;
    private bool isSprinting = false;

    private void Start()
    {
        characterTransform = transform;
        cameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        if (Input.anyKey)
        {
            Move();
        }

        // Check if the mouse wheel is pressed down or released
        if (Input.GetMouseButtonDown(2)) // Mouse wheel button
        {
            isMouseWheelDown = true;
        }
        else if (Input.GetMouseButtonUp(2))
        {
            isMouseWheelDown = false;
        }

        if (isMouseWheelDown)
        {
            float rotationInput = Input.GetAxis("Mouse X");
            RotateCamera(rotationInput);
        }

        float zoomInput = Input.GetAxis("Mouse ScrollWheel");
        ZoomCamera(zoomInput);

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            isSprinting = true;
        }
        else
        {
            isSprinting = false;
        }
        
    }

    private void Move()
    {
        if (isSprinting)
        {
            _moveSpeed = _speedSprint;
        }
        else
        {
            _moveSpeed = _speedWalk;
        }
        Vector3 forwardDirection = cameraTransform.forward;
        forwardDirection.y = 0;
        forwardDirection = Vector3.Normalize(forwardDirection);
        Vector3 rightDirection = Quaternion.Euler(new Vector3(0, 90, 0)) * forwardDirection;

        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 rightMovement = rightDirection * _moveSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
        Vector3 upMovement = forwardDirection * _moveSpeed * Time.deltaTime * Input.GetAxis("Vertical");

        Vector3 heading = Vector3.Normalize(rightMovement + upMovement);

        characterTransform.forward = heading;
        characterTransform.position += rightMovement;
        characterTransform.position += upMovement;

        cameraTransform.position = characterTransform.position;
    }

    private void RotateCamera(float rotationInput)
    {
        if (Mathf.Abs(rotationInput) > 0.0f)
        {
            cameraTransform.RotateAround(characterTransform.position, Vector3.up, rotationInput * _rotationSpeed);
        }
    }

    private void ZoomCamera(float zoomInput)
    {
        Camera.main.orthographicSize -= zoomInput * _zoomSpeed;
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, _orthographicSizeMin, _orthographicSizeMax);
    }
}

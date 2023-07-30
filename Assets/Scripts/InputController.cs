using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 4f;

    private Vector3 forward, right;
    private Transform characterTransform;
    private Transform cameraTransform;

    private void Start()
    {
        characterTransform = transform;
        cameraTransform = Camera.main.transform;

        forward = cameraTransform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
    }

    private void Update()
    {
        if (Input.anyKey)
            Move();
    }

    private void Move()
    {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 rightMovement = right * _moveSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
        Vector3 upMovement = forward * _moveSpeed * Time.deltaTime * Input.GetAxis("Vertical");

        Vector3 heading = Vector3.Normalize(rightMovement + upMovement);

        characterTransform.forward = heading;
        characterTransform.position += rightMovement;
        characterTransform.position += upMovement;

        // Update the camera position to center on the character
        cameraTransform.position = characterTransform.position;
    }
}
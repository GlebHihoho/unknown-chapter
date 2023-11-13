using System;
using UnityEngine;

// TODO: удаляем?
public class InputController : MonoBehaviour
{
    [SerializeField] private float _speedWalk = 4f;
    [SerializeField] private float _speedSprint = 12f;
    [SerializeField] private float _rotationSpeed = 5f;
    [SerializeField] private float _zoomSpeed = 5f; // Adjust this to control camera zoom speed
    [SerializeField] private float _orthographicSizeMin = 2f;
    [SerializeField] private float _orthographicSizeMax = 15f;

    private float _moveSpeed = 0f;
    private Animator _characterAnimator;
    
    private float Velocity = 0f;
    private int _velocityHash;
    private float velocityTransitionTime = 0.5f;
    private float velocityTransitionTimer = 0f;
    

    private Transform characterTransform;
    private Transform cameraTransform;
    private bool isMouseWheelDown = false;
    private bool isSprinting = false;
    
    
    
    private float _originalOrthographicSize;
    
    private Vector3 targetPosition; // Целевая позиция для движения
    private bool isMovingToTarget;
    
    
    
    [SerializeField] private float _zoomOffset = 1.0f;



    [SerializeField] private Vector3 cameraOffset = new Vector3(5f, 5f, 5f); // Расстояние между персонажем и камерой
    

    private void Start()
    {
        characterTransform = transform;
        cameraTransform = Camera.main.transform;
        _characterAnimator = GetComponent<Animator>();
        // _velocityHash = _characterAnimator.StringToHash("Velocity");
        _velocityHash = Animator.StringToHash("Velocity");
    }

    private void Update()
    {
        
        
        // if (Input.GetMouseButtonDown(0)) // Левая кнопка мыши
        // {
        //     // Получаем позицию щелчка мыши на земле
        //     Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //     RaycastHit hit;
        //     print(ray);
        //     
        //     if (Physics.Raycast(ray, out hit))
        //     {
        //         // Устанавливаем целевую позицию для движения
        //         SetTargetPosition(hit.point);
        //     }
        // }
        //
        // if (isMovingToTarget)
        // {
        //     MoveToTarget(); // Двигаемся к цели
        // }
        
        
        
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            if (Velocity <= 0.5)
            {
                Velocity += Time.deltaTime * 1.5f;
            }
            Move();
            _characterAnimator.SetFloat(_velocityHash, Velocity);
        }
        else
        {
            if (Velocity >= 0)
            {
                Velocity -= Time.deltaTime;
            }
            _characterAnimator.SetFloat(_velocityHash, Velocity);
        }
        
        if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && Velocity >= 0.5f)
        {
            // isSprinting = true;
            // _characterAnimator.SetBool("IsSprint", true);
            if (Velocity <= 1f)
            {
                Velocity += Time.deltaTime / 2f;
                isSprinting = true;
            }
        }
        else
        {
            // _characterAnimator.SetBool("IsSprint", false);
            if (Velocity >= 0.5f)
            {
                Velocity -= Time.deltaTime / 2f;
                isSprinting = false;

            }

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
        
        
        
        // cameraTransform.position = characterTransform.position + new Vector3(-7f, 4f, 0f);
        // cameraTransform.LookAt(characterTransform);
        // float characterRotationY = characterTransform.eulerAngles.y;
        
        
        // cameraTransform.eulerAngles = new Vector3(cameraTransform.eulerAngles.x, characterRotationY, cameraTransform.eulerAngles.z);
    }
    
    // // Установка целевой позиции для движения
    // private void SetTargetPosition(Vector3 position)
    // {
    //     targetPosition = position;
    //     isMovingToTarget = true;
    // }
    //
    // // Метод для движения к целевой позиции
    // private void MoveToTarget()
    // {
    //     if (!Input.GetKeyDown(KeyCode.Escape))
    //     {
    //         // Вычисляем направление к целевой точке
    //         Vector3 moveDirection = (targetPosition - characterTransform.position).normalized;
    //
    //         // Проверяем расстояние до цели
    //         float distanceToTarget = Vector3.Distance(characterTransform.position, targetPosition);
    //
    //         // Устанавливаем скорость движения (по желанию)
    //         float moveSpeed = _speedWalk;
    //
    //         // Если расстояние до цели слишком мало, завершаем движение
    //         if (distanceToTarget < 0.1f)
    //         {
    //             isMovingToTarget = false;
    //         }
    //         else
    //         {
    //             // Выполняем перемещение к целевой точке с учетом скорости
    //             characterTransform.position += moveDirection * moveSpeed * Time.deltaTime;
    //             Vector3 newForward = new Vector3(moveDirection.x, characterTransform.forward.y, moveDirection.z);
    //             characterTransform.forward = newForward;
    //         }
    //     }
    //     
    // }
}

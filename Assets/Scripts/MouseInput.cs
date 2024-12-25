using MxM;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class MouseInput : MonoBehaviour
{
    [SerializeField] private float _tolerance = 0.6f;
    [SerializeField] private GameObject _movePointPrefab;
    [SerializeField] private MxMTrajectoryGenerator _m_trajectoryGenerator;
    
    [Header("Input Profiles")] [SerializeField] private MxMInputProfile _m_generalLocomotion = null;
    
    private MxMAnimator animator;
    private GameObject _particleObject;
    private bool _isParticleMovePoint = false;
    private Vector3 _currentClickPoint;
    private NavMeshAgent agent;

    public LayerMask WhatCanBeClickedOn;


    public static MouseInput instance;

    public event Action OnNewInput;
    public event Action OnDestinationSet;
    public event Action OnDeleteMovePoint;


    bool isPaused = false;


    bool pointerOverInteractable = false;

    bool settingDestination = false;

    Vector3 destination;

    [SerializeField, Min(0)] float walkingDistance = 5;
    [SerializeField, Min(0)] float runningDistance = 17;


    private void Awake()
    {
        if (instance == null) instance = this;

        agent = GetComponent<NavMeshAgent>();

        animator = GetComponentInChildren<MxMAnimator>();

        _m_trajectoryGenerator = GetComponentInChildren<MxMTrajectoryGenerator>();
        _m_trajectoryGenerator.InputProfile = _m_generalLocomotion;
    }

    void Start()
    {
        //GameControls.instance.OnMove += OldSetDestination;
        GameControls.instance.OnMoveStarted += MoveStarted;
        GameControls.instance.OnMoveEnded += MoveEnded;

        Interactable.OnPointerEnter += InteractablePointerEnter;
        Interactable.OnPointerExit += InteractablePointerExit;
    }

   


    private void OnDestroy()
    {
        //GameControls.instance.OnMove -= OldSetDestination;
        GameControls.instance.OnMoveStarted -= MoveStarted;
        GameControls.instance.OnMoveEnded -= MoveEnded;

        Interactable.OnPointerEnter -= InteractablePointerEnter;
        Interactable.OnPointerExit -= InteractablePointerExit;
    }


    private void InteractablePointerEnter() => pointerOverInteractable = true;
    private void InteractablePointerExit() => pointerOverInteractable = false;


    private void MoveStarted() => settingDestination = true;

    private void MoveEnded() => settingDestination = false;


    private void OnEnable()
    {
        SaveManager.OnStartingLoad += ResetMovement;
        Pause.OnPause += SetPause;
    }

    private void OnDisable()
    {
        SaveManager.OnStartingLoad -= ResetMovement;
        Pause.OnPause -= SetPause;
    }

    public void ResetMovement()
    {
        if (_particleObject != null) DeleteMovePoint();
        agent.isStopped = true;
        agent.ResetPath();    
    }



    private void SetPause(bool isPaused)
    {

        this.isPaused = isPaused;


        if (isPaused) 
        {
            animator.Pause();
            agent.isStopped = true;
        }
        else
        {
            agent.isStopped = false;
            animator.UnPause();
        }     
    }


    private void Update()
    {
        if (settingDestination) OldSetDestination();
    }


    void FixedUpdate()
    {

        if (agent.enabled)
        {
            if (agent.remainingDistance <= _tolerance && agent.hasPath && _isParticleMovePoint)
            {
                _isParticleMovePoint = false;
                DeleteMovePoint();
            }


            if (agent.remainingDistance < agent.stoppingDistance)
            {
                if (!animator.IsIdle)
                {
                    animator.BeginIdle();
                    animator.ClearFavourTags();
                    animator.RootMotion = EMxMRootMotion.Off;
                }
            }
            else
            {

                animator.RootMotion = EMxMRootMotion.On;


                if (agent.remainingDistance > agent.stoppingDistance && agent.remainingDistance < walkingDistance)
                {
                    animator.SetFavourTag("Walk", 0.5f);
                }
                else if (agent.remainingDistance > walkingDistance && agent.remainingDistance < runningDistance)
                {
                    animator.SetFavourTag("Run", 0.9f);
                }
                else
                {
                    animator.SetFavourTag("Sprint", 0.9f);
                }
            }
        }

    }


    private void OldSetDestination()
    {
        if (pointerOverInteractable || EventSystem.current.IsPointerOverGameObject()) return;

        SetDestination();

        OnNewInput?.Invoke();
    }


    public void SetDestination(bool showMovePoint = true)
    {

        if (isPaused) return;

        Vector3 newPos;
        newPos.x = Pointer.current.position.x.ReadValue();
        newPos.y = Pointer.current.position.y.ReadValue();
        newPos.z = 0;


        Ray myRay = Camera.main.ScreenPointToRay(newPos);
        RaycastHit hitInfo;

        if (Physics.Raycast(myRay, out hitInfo, 100, WhatCanBeClickedOn))
        {
            animator.RootMotion = EMxMRootMotion.On;

            _currentClickPoint = hitInfo.point;


            if (!_isParticleMovePoint)
            {
                CreateMovePoint(showMovePoint);
            }
            else
            {
                UpdateMovePoint();
            }

            destination = hitInfo.point;
            agent.SetDestination(hitInfo.point);
        }

        OnDestinationSet?.Invoke();
    }



    private void CreateMovePoint(bool showMovePoint)
    {
        _particleObject = Instantiate(
            _movePointPrefab,
            _currentClickPoint + new Vector3(0f, 0.2f, 0f),
            Quaternion.Euler(90f, 0f, 0f)
        );
        _isParticleMovePoint = true;

        if (!showMovePoint) _particleObject.GetComponent<Renderer>().enabled = false;
    }


    public void DeleteMovePoint()
    {
        Destroy(_particleObject);
        _isParticleMovePoint = false;
        OnDeleteMovePoint?.Invoke();
    }

    private void UpdateMovePoint()
    {
        _particleObject.transform.position = _currentClickPoint + new Vector3(0f, 0.2f, 0f);
    }


}

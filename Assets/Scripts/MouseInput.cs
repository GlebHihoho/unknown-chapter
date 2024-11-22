using MxM;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class MouseInput : MonoBehaviour
{
    [SerializeField] private float _tolerance = 0.6f;
    [SerializeField] private GameObject _movePointPrefab;
    [SerializeField] private LocomotionSpeedRamp _m_locomotionSpeedRamp;
    [SerializeField] private MxMTrajectoryGenerator _m_trajectoryGenerator;
    [SerializeField] private MxMInputProfile _m_sprintLocomotion = null;
    
    [Header("Input Profiles")] [SerializeField] private MxMInputProfile _m_generalLocomotion = null;
    
    private MxMAnimator _m_mxmAnimator;
    private GameObject _particleObject;
    private bool _isParticleMovePoint = false;
    private Vector3 _currentClickPoint;
    private NavMeshAgent _myAgent;

    public LayerMask WhatCanBeClickedOn;


    public static MouseInput instance;

    public event Action OnNewInput;
    public event Action OnDestinationSet;
    public event Action OnDeleteMovePoint;


    bool isPaused = false;
    bool agentEnabled = true;

    Vector3 destination;


    private void Awake()
    {
        if (instance == null) instance = this;

        _myAgent = GetComponent<NavMeshAgent>();
        _m_locomotionSpeedRamp = GetComponent<LocomotionSpeedRamp>();

        _m_mxmAnimator = GetComponentInChildren<MxMAnimator>();

        _m_trajectoryGenerator = GetComponentInChildren<MxMTrajectoryGenerator>();
        _m_trajectoryGenerator.InputProfile = _m_generalLocomotion;
    }

    void Start() => GameControls.instance.OnMove += SetDestination;
    private void OnDestroy() => GameControls.instance.OnMove -= SetDestination;


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
        _myAgent.isStopped = true;
        _myAgent.ResetPath();    
    }



    private void SetPause(bool isPaused)
    {

        this.isPaused = isPaused;


        if (isPaused) 
        {
            _m_mxmAnimator.Pause();

            agentEnabled = _myAgent.enabled;
            _myAgent.enabled = false;
        }
        else
        {
            _myAgent.enabled = agentEnabled;

            if (_isParticleMovePoint && _myAgent.enabled) _myAgent.SetDestination(destination);

            _m_mxmAnimator.UnPause();
        }     
    }


    void FixedUpdate()
    {

        if (_myAgent.enabled)
        {
            if (_myAgent.remainingDistance <= _tolerance && _myAgent.hasPath && _isParticleMovePoint)
            {
                _isParticleMovePoint = false;
                DeleteMovePoint();
            }
        }

    }


    private void SetDestination(InputAction.CallbackContext obj)
    {
        SetDestination();

        OnNewInput?.Invoke();
    }

    public void SetDestination(bool showMovePoint = true)
    {
        Vector3 newPos;
        newPos.x = Pointer.current.position.x.ReadValue();
        newPos.y = Pointer.current.position.y.ReadValue();
        newPos.z = 0;


        Ray myRay = Camera.main.ScreenPointToRay(newPos);
        RaycastHit hitInfo;

        if (Physics.Raycast(myRay, out hitInfo, 100, WhatCanBeClickedOn))
        {
            _m_mxmAnimator.RootMotion = EMxMRootMotion.On;
            _myAgent.enabled = true;

            _currentClickPoint = hitInfo.point;
            _m_locomotionSpeedRamp.BeginSprint();
            _m_trajectoryGenerator.MaxSpeed = 5f;
            _m_trajectoryGenerator.PositionBias = 6f;
            _m_trajectoryGenerator.DirectionBias = 6f;
            _m_mxmAnimator.SetCalibrationData("Sprint");
            _m_trajectoryGenerator.InputProfile = _m_sprintLocomotion;

            if (!_isParticleMovePoint)
            {
                CreateMovePoint(showMovePoint);
            }
            else
            {
                UpdateMovePoint();
            }

            destination = hitInfo.point;
            _myAgent.SetDestination(hitInfo.point);
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

    public void SetIdle()
    {
        _m_mxmAnimator.RootMotion = EMxMRootMotion.Off;
        _myAgent.enabled = false;
    }

}

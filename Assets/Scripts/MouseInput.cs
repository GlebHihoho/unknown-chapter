using MxM;
using UnityEngine;
using UnityEngine.AI;

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
    
    void Start()
    {
        _myAgent = GetComponent<NavMeshAgent>();
        _m_locomotionSpeedRamp = GetComponent<LocomotionSpeedRamp>();

        _m_mxmAnimator = GetComponentInChildren<MxMAnimator>();

        _m_trajectoryGenerator = GetComponentInChildren<MxMTrajectoryGenerator>();
        _m_trajectoryGenerator.InputProfile = _m_generalLocomotion;
    }

    void FixedUpdate()
    {
        if (_myAgent.remainingDistance <= _tolerance && _myAgent.hasPath && _isParticleMovePoint)
        {
            _isParticleMovePoint = false;
            DeleteMovePoint();
        }

        if (Input.GetMouseButton(1))
        {
            Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(myRay, out hitInfo, 100, WhatCanBeClickedOn))
            {
                _currentClickPoint = hitInfo.point;
                _m_locomotionSpeedRamp.BeginSprint();
                _m_trajectoryGenerator.MaxSpeed = 5f;
                _m_trajectoryGenerator.PositionBias = 6f;
                _m_trajectoryGenerator.DirectionBias = 6f;
                _m_mxmAnimator.SetCalibrationData("Sprint");
                _m_trajectoryGenerator.InputProfile = _m_sprintLocomotion;

                if (!_isParticleMovePoint)
                {
                    CreateMovePoint();
                }
                else
                {
                    UpdateMovePoint();
                }

                _myAgent.SetDestination(hitInfo.point);
                _myAgent.isStopped = true; // Остановить навигацию
            }
        }
    }

    private void CreateMovePoint()
    {
        _particleObject = Instantiate(
            _movePointPrefab,
            _currentClickPoint + new Vector3(0f, 0.2f, 0f),
            Quaternion.Euler(90f, 0f, 0f)
        );
        _isParticleMovePoint = true;
    }

    public void DeleteMovePoint()
    {
        Destroy(_particleObject);
        _isParticleMovePoint = false;
    }
    private void UpdateMovePoint()
    {
        _particleObject.transform.position = _currentClickPoint + new Vector3(0f, 0.2f, 0f);
    }
}

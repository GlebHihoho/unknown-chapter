using System.Collections;
using System.Collections.Generic;
using MxM;
using UnityEngine;
using UnityEngine.AI;

public class MouseInput : MonoBehaviour
{
    // TODO naming
    public LayerMask WhatCanBeClickedOn;
    private NavMeshAgent _myAgent;
    // TODO удалим?
    [SerializeField] private Vector3 previousClickPoint; // Сохраняем предыдущую точку нажатия
    // TODO удалим?
    [SerializeField] private float tolerance = 1f; // Допуск по координатам X и Z
    [SerializeField] private GameObject _movePointPrefab;
    // naming
    private GameObject _particleObject;
    private bool _isParticleMovePoint = false;
    private Vector3 currentClickPoint;
    // TODO - вынести выше все SerializeField ??? naming
    [SerializeField] private LocomotionSpeedRamp m_locomotionSpeedRamp;
    [SerializeField] private MxMTrajectoryGenerator m_trajectoryGenerator;
    private MxMAnimator m_mxmAnimator;

    [Header("Input Profiles")] [SerializeField]
    private MxMInputProfile m_generalLocomotion = null;

    [SerializeField] private MxMInputProfile m_sprintLocomotion = null;


    void Start()
    {
        _myAgent = GetComponent<NavMeshAgent>();
        m_locomotionSpeedRamp = GetComponent<LocomotionSpeedRamp>();

        m_mxmAnimator = GetComponentInChildren<MxMAnimator>();

        m_trajectoryGenerator = GetComponentInChildren<MxMTrajectoryGenerator>();
        m_trajectoryGenerator.InputProfile = m_generalLocomotion;
    }

    void FixedUpdate()
    {
        // TODO: magic number 0.6f
        if (_myAgent.remainingDistance <= 0.6f && _myAgent.hasPath && _isParticleMovePoint)
        {
            _isParticleMovePoint = false;
            DeleteParticle();
        }

        if (Input.GetMouseButton(1))
        {
            Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(myRay, out hitInfo, 100, WhatCanBeClickedOn))
            {
                currentClickPoint = hitInfo.point;
                // TODO удалить
                Debug.Log("Двойной щелчок");
                m_locomotionSpeedRamp.BeginSprint();
                m_trajectoryGenerator.MaxSpeed = 5f;
                m_trajectoryGenerator.PositionBias = 6f;
                m_trajectoryGenerator.DirectionBias = 6f;
                m_mxmAnimator.SetCalibrationData("Sprint");
                m_trajectoryGenerator.InputProfile = m_sprintLocomotion;

                if (!_isParticleMovePoint)
                {
                    CreatePaticle();
                }
                else
                {
                    // DeleteParticle();
                    // CreatePaticle();
                    // Удаление и создание частицы не нужно, достаточно обновить её позицию
                    UpdateParticlePosition();
                }

                _myAgent.SetDestination(hitInfo.point);
                _myAgent.isStopped = true; // Остановить навигацию
                previousClickPoint = hitInfo.point; // Сохраняем текущую точку нажатия
            }
        }
    }

    // TODO: naming CreateMovePoint
    private void CreatePaticle()
    {
        _particleObject = Instantiate(
            _movePointPrefab,
            currentClickPoint + new Vector3(0f, 0.2f, 0f),
            Quaternion.Euler(90f, 0f, 0f)
        );
        _isParticleMovePoint = true;
    }

    // TODO: naming DeleteMovePoint
    public void DeleteParticle()
    {
        Destroy(_particleObject);
        _isParticleMovePoint = false;
    }
    // TODO: naming UpdateMovePoint
    private void UpdateParticlePosition()
    {
        // Обновление позиции существующей частицы
        _particleObject.transform.position = currentClickPoint + new Vector3(0f, 0.2f, 0f);
    }
}

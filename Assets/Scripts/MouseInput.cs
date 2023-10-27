using System.Collections;
using System.Collections.Generic;
using MxM;
using UnityEngine;
using UnityEngine.AI;

public class MouseInput : MonoBehaviour
{
    public LayerMask WhatCanBeClickedOn;
    private NavMeshAgent _myAgent;
    [SerializeField] private Vector3 previousClickPoint; // Сохраняем предыдущую точку нажатия
    [SerializeField] private float tolerance = 1f; // Допуск по координатам X и Z
    
    [SerializeField] private LocomotionSpeedRamp m_locomotionSpeedRamp;
    [SerializeField] private MxMTrajectoryGenerator m_trajectoryGenerator;
    private MxMAnimator m_mxmAnimator;

    [Header("Input Profiles")]
    [SerializeField]
    private MxMInputProfile m_generalLocomotion = null;
    
    [SerializeField]
    private MxMInputProfile m_sprintLocomotion = null;
    

    void Start()
    {
        _myAgent = GetComponent<NavMeshAgent>();
        m_locomotionSpeedRamp = GetComponent<LocomotionSpeedRamp>();
        
        m_mxmAnimator = GetComponentInChildren<MxMAnimator>();

        m_trajectoryGenerator = GetComponentInChildren<MxMTrajectoryGenerator>();
        m_trajectoryGenerator.InputProfile = m_generalLocomotion;

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
        
            if (Physics.Raycast(myRay, out hitInfo, 100, WhatCanBeClickedOn))
            {
                Vector3 currentClickPoint = hitInfo.point;
                if (Vector3.Distance(currentClickPoint, previousClickPoint) <= tolerance)
                {
                    Debug.Log("Двойной щелчок");
                    m_locomotionSpeedRamp.BeginSprint();
                    m_trajectoryGenerator.MaxSpeed = 4.8f;
                    m_trajectoryGenerator.PositionBias = 6f;
                    m_trajectoryGenerator.DirectionBias = 6f;
                    m_mxmAnimator.SetCalibrationData("Sprint");
                    m_trajectoryGenerator.InputProfile = m_sprintLocomotion;
                }
                else
                {
                    Debug.Log("Одиночный щелчок");
                    m_locomotionSpeedRamp.ResetFromSprint();
                    m_trajectoryGenerator.MaxSpeed = 2f;
                    m_trajectoryGenerator.PositionBias = 10f;
                    m_trajectoryGenerator.DirectionBias = 10f;
                    m_mxmAnimator.SetCalibrationData("General");
                    m_trajectoryGenerator.InputProfile = m_generalLocomotion;
                }
                
                // _myAgent.SetDestination(hitInfo.point);
                // previousClickPoint = hitInfo.point; // Сохраняем текущую точку нажатия
                
                _myAgent.SetDestination(hitInfo.point);
                _myAgent.isStopped = true; // Остановить навигацию
                previousClickPoint = hitInfo.point; // Сохраняем текущую точку нажатия
            }
        }
    }
}

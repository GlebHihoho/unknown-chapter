﻿using System;
using MxM;
using PixelCrushers.DialogueSystem;
using UI.InventoryScripts;
using UnityEngine;
using UnityEngine.AI;

namespace UI
{
    public class UIStateController : MonoBehaviour
    {
        [SerializeField] private CameraController _camera;
        private MouseInput _mouseInput;
        private GameObject _playerHUD;
        private NavMeshAgent _navMeshAgent;
        private MxMTrajectoryGenerator _mxMTrajectoryGenerator;
        private MxMAnimator _mxMAnimator;


        private void Start()
        {
            _mouseInput = FindAnyObjectByType<MouseInput>(FindObjectsInactive.Include);
            _navMeshAgent = FindAnyObjectByType<NavMeshAgent>(FindObjectsInactive.Include);
            _mxMTrajectoryGenerator = FindAnyObjectByType<MxMTrajectoryGenerator>(FindObjectsInactive.Include);
            _mxMAnimator = FindAnyObjectByType<MxMAnimator>(FindObjectsInactive.Include);
            _playerHUD = FindAnyObjectByType<ItemsDB>(FindObjectsInactive.Include).gameObject;
        }

        private void Update()
        {
            var DSC = GetComponent<DialogueSystemController>();
            var gg = DSC.currentConversant;
            if (gg != null)
            {
                _camera.enabled = false;
                _mouseInput.enabled = false;
                _playerHUD.SetActive(false);
                _navMeshAgent.enabled = false;
                // _mxMTrajectoryGenerator.enabled = false;
                _mxMAnimator.enabled = false;
                _mouseInput.DeleteMovePoint();
            }
            else
            {
                _camera.enabled = true;
                _mouseInput.enabled = true;
                _playerHUD.SetActive(true);
                _navMeshAgent.enabled = true;
                // _mxMTrajectoryGenerator.enabled = true;
                _mxMAnimator.enabled = true;
            }
        }
    }
    
    
}
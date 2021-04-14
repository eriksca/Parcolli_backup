using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class CinemachineScript : MonoBehaviour
{
    [SerializeField]
    private InputAction action;
    [SerializeField]
    private CinemachineVirtualCamera vcam1; //overworld
    [SerializeField]
    private CinemachineVirtualCamera vcam2; //NPC player

    private Animator animator;

    private bool overworldCamera = true;

    private void Awake()
    {
        //animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        action.Enable();
    }

    private void OnDisable()
    {
        action.Disable();
    }
    // Start is called before the first frame update
    void Start()
    {
        action.performed += flip => SwitchPriority();
    }

    private void SwitchState()
    {
        if (overworldCamera)
        {
            animator.Play("NPC");
        }
        else
        {
            animator.Play("overworldCamera");
        }
        overworldCamera = !overworldCamera;
        
    }

    private void SwitchPriority()
    {
        if (overworldCamera)
        {
            vcam1.Priority = 0;
            vcam2.Priority = 1;
        }
        else
        {
            vcam1.Priority = 1;
            vcam2.Priority = 0;
        }
        overworldCamera = !overworldCamera;
    }

  
}

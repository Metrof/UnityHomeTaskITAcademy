using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class NavAgentMovement : MonoBehaviour
{
    Camera _cam;
    NavMeshAgent _agent;
    Controller _controller;
    private void Awake()
    {
        _cam = Camera.main;
        _agent = GetComponent<NavMeshAgent>();
        _controller = new Controller();
    }
    private void OnEnable()
    {
        _controller.Enable();
        _controller.Player.SetDestination.performed += SetDestination;
    }

    private void OnDisable()
    {
        _controller.Player.SetDestination.performed -= SetDestination;
        _controller.Disable();
    }
    private void SetDestination(InputAction.CallbackContext obj)
    {
        Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            _agent.SetDestination(hit.point);
        }
    }
}

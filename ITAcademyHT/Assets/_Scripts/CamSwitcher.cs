using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class CamSwitcher : MonoBehaviour
{
    [SerializeField] CinemachineFreeLook _cinemachine;
    [SerializeField] int _newPriority = 9;

    private void OnTriggerEnter(Collider other)
    {
        if (_cinemachine != null) _cinemachine.Priority = _newPriority;
    }
}

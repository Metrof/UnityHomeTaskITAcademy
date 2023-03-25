using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Collider))]
public class Swamp : MonoBehaviour
{
    [SerializeField] float _deceleration = 0.5f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out NavMeshAgent navMeshAgent)) navMeshAgent.speed *= _deceleration;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out NavMeshAgent navMeshAgent)) navMeshAgent.speed /= _deceleration;
    }
}

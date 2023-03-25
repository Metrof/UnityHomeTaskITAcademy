using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Door : MonoBehaviour
{
    [SerializeField] float _targetAngle = 90;

    HingeJoint _joint;

    private void Awake()
    {
        _joint = GetComponentInChildren<HingeJoint>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (_joint == null) return;
        Vector3 ySize = Vector3.Cross(transform.forward, transform.position - other.transform.position);
        if (ySize.y > 0)
        {
             JointSpring jointSpring = _joint.spring;
            jointSpring.targetPosition = _targetAngle;
            _joint.spring = jointSpring;
        }
        else
        {
            JointSpring jointSpring = _joint.spring;
            jointSpring.targetPosition = -_targetAngle;
            _joint.spring = jointSpring;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_joint == null) return;
        JointSpring jointSpring = _joint.spring;
        jointSpring.targetPosition = 0;
        _joint.spring = jointSpring;
    }
}

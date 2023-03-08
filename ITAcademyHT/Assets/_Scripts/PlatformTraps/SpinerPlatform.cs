using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HingeJoint2D))]
public class SpinerPlatform : TriggerObject
{
    [SerializeField] float _spinSpeed = 400;
    HingeJoint2D m_Joint;

    private void Awake()
    {
        m_Joint = GetComponent<HingeJoint2D>();
    }
    public override void TriggerActivate()
    {
        JointMotor2D jointMotor = m_Joint.motor;
        jointMotor.motorSpeed = _spinSpeed;
        m_Joint.motor = jointMotor;
    }
}

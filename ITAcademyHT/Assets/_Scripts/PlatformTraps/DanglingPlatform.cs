using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpringJoint2D))]
public class DanglingPlatform : TriggerObject
{
    [SerializeField] float _attractionDistance = 1;
    SpringJoint2D m_Joint;

    private void Awake()
    {
        m_Joint = GetComponent<SpringJoint2D>();
    }
    public override void TriggerActivate()
    {
        m_Joint.distance = _attractionDistance;
    }
}

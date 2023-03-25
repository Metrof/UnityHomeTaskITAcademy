using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane
{
    private Vector3 m_Normal;
    private float m_Distance;

    public Plane()
    {
        m_Normal = Vector3.up;
        m_Distance = 0.0f;
    }

    public Plane(Vector3 inNormal, Vector3 inPoint)
    {
        m_Normal = Vector3.Normalize(inNormal);
        m_Distance = -Vector3.Dot(m_Normal, inPoint);
    }

    public void UpdateParam(Vector3 inNormal, Vector3 inPoint)
    {
        m_Normal = Vector3.Normalize(inNormal);
        m_Distance = -Vector3.Dot(m_Normal, inPoint);
        Debug.Log("Plane: m_Normal " + m_Normal);
        Debug.Log("Plane: m_Distance " + m_Distance);
    }

    public Vector3 GetNormal()
    {
        return m_Normal;
    }

    public float DistFromPlane(Vector3 point)
    {
        return Vector3.Dot(m_Normal, point) + m_Distance;
    }

    public bool isLeftSideFromPlane(Vector3 point)
    {
        return (Vector3.Dot(m_Normal, point) + m_Distance) > 0.0f;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotRotation : MonoBehaviour
{
    [SerializeField] float _rotationSpeed = 9;

    private void Update()
    {
        transform.Rotate(0, Input.GetAxis("Mouse X") * _rotationSpeed, 0);
    }
}

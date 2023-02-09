using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotMove : MonoBehaviour
{
    [SerializeField] float _speed = 3;
    Rigidbody _body;

    private void Awake()
    {
        _body = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Vector3 moveDir = Vector3.zero;
        if (Input.GetAxis("Horizontal") != 0) moveDir.x = Input.GetAxis("Horizontal") * _speed;
        if (Input.GetAxis("Vertical") != 0) moveDir.z = Input.GetAxis("Vertical") * _speed;
        _body.velocity = moveDir;
    }
}

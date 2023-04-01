using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotMove : MonoBehaviour
{
    [SerializeField] float _gravity = -9.81f;
    [SerializeField] float _speed = 5;
    [SerializeField] float _rotationSpeed = 5;

    CharacterController _controller;
    public CharacterController Controller
    {
        get { return _controller ??= GetComponent<CharacterController>(); }
    }
    private void Update()
    {
        float ver = Input.GetAxis("Vertical");
        float hor = Input.GetAxis("Horizontal");

        float rotation = Input.GetAxis("Mouse X");

        Vector3 movement = new(hor * _speed, _gravity, ver * _speed);
        movement *= Time.deltaTime;

        Controller.Move(transform.TransformDirection(movement));
        Controller.transform.Rotate(Vector3.up, rotation * _rotationSpeed);
    }
}

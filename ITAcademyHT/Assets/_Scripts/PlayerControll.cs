using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    [SerializeField] float _gravity = -9.81f;
    [SerializeField] float _mass = 5;
    [SerializeField] float _speed = 20;
    [SerializeField] float _rotationSpeed = 2;
    [SerializeField] float _jumpForce = 20;
    [SerializeField] Joystick _joystick;

    float _vertSpeed;

    CharacterController _controller;
    public CharacterController Controller
    {
        get { return _controller ??= GetComponent<CharacterController>(); }
    }
    private void Update()
    {
        _vertSpeed += _gravity * _mass * Time.deltaTime;

        Vector3 movement = _joystick.Direction() * _speed;
        movement = transform.TransformDirection(movement);
        if (movement != Vector3.zero)
        {
            Quaternion rotateAngle = Quaternion.LookRotation(movement + Controller.transform.position - Controller.transform.position);
            Controller.transform.rotation = Quaternion.Lerp(Controller.transform.rotation, rotateAngle, Time.deltaTime * _rotationSpeed);
        }
        movement.y = _vertSpeed;
        movement *= Time.deltaTime;
        Controller.Move(movement);
    }
    public void Jump()
    {
        if (Controller.isGrounded) _vertSpeed = _jumpForce;
    }
}

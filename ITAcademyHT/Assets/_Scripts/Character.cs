using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class Character : MonoBehaviour
{
    [SerializeField] float _speed = 400;

    private Controller _controller;
    private Rigidbody2D _rb;
    private Vector3 _moveDir;
    private Matrix4x4 _isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 0, 45));

    private void Awake()
    {
        _controller = new Controller();
        _rb = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        _controller.Enable();
        _controller.Player.Move.performed += Move;
        _controller.Player.Move.canceled += StopMove;
    }

    private void OnDisable()
    {
        _controller.Player.Move.performed -= Move;
        _controller.Player.Move.canceled -= StopMove;
        _controller.Disable();
    }
    private void Move(InputAction.CallbackContext obj) => _moveDir = _isoMatrix.MultiplyPoint3x4(obj.ReadValue<Vector2>());
    private void StopMove(InputAction.CallbackContext obj) => _moveDir = Vector3.zero;
    private void Update()
    {
        _rb.velocity = _speed * Time.deltaTime * _moveDir;
    }
}

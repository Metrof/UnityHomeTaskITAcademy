using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XInput;
using UnityEngine.TextCore.Text;

public class Player : MonoBehaviour
{
    [SerializeField] float _gravity = -9.81f;
    [SerializeField] float _speed = 5;
    [SerializeField] float _rotationSpeed = 5;

    private float _rotationAngle = 0.0f;
    private CharacterController _controller;
    private Camera _characterCamera;
    private Controller _inputController;
    private Vector2 _inputDir;
    private Vector3 _rotatedMovement;
    public CharacterController Controller { get { return _controller ??= GetComponentInChildren<CharacterController>(); } }
    public Camera CharacterCamera { get { return _characterCamera ??= FindObjectOfType<Camera>(); } }
    private void Awake()
    {
        _inputController = new Controller();
    }
    private void OnEnable()
    {
        _inputController.Enable();
    }

    private void OnDisable()
    {
        _inputController.Disable();
    }

    private void Update()
    {
        _inputDir = _inputController.Player.Move.ReadValue<Vector2>();
        _rotatedMovement = Quaternion.Euler(0.0f, CharacterCamera.transform.rotation.eulerAngles.y, 0.0f) * _inputDir;

        Vector3 movement = new(_inputDir.x * _speed, _gravity, _inputDir.y * _speed);
        movement *= Time.deltaTime;

        Controller.Move(transform.TransformDirection(movement));

        if (_inputDir.sqrMagnitude > 0)
        {
            _rotationAngle = Mathf.Atan2(_rotatedMovement.x, _rotatedMovement.z) * Mathf.Rad2Deg;
        }
        Quaternion currentRotation = Controller.transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0.0f, _rotationAngle, 0.0f);
        Controller.transform.rotation = Quaternion.Lerp(currentRotation, targetRotation, _rotationSpeed * Time.deltaTime);
    }
}

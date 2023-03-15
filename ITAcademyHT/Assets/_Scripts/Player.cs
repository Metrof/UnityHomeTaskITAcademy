using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] float _gravity = -9.81f;
    [SerializeField] float _speed = 5;
    [SerializeField] Transform _cameraPos;

    private CharacterController _controller;
    private Camera _characterCamera;
    private Controller _inputController;
    private AudioSource _playerAudioSource;
    private Vector2 _inputDir;
    public CharacterController Controller { get { return _controller ??= GetComponent<CharacterController>(); } }
    public Camera CharacterCamera { get { return _characterCamera ??= FindObjectOfType<Camera>(); } }
    public Transform CameraPos { get { return _cameraPos ??= transform; } }
    private void Awake()
    {
        _inputController = new Controller();
        _playerAudioSource = GetComponentInChildren<AudioSource>();
    }
    private void OnEnable()
    {
        _inputController.Enable();
        _inputController.Player.Move.performed += PlayStepsSound;
        _inputController.Player.Move.canceled += MuteStepsSound;
    }

    private void OnDisable()
    {
        _inputController.Player.Move.performed -= PlayStepsSound;
        _inputController.Player.Move.canceled -= MuteStepsSound;
        _inputController.Disable();
    }

    private void Update()
    {
        _inputDir = _inputController.Player.Move.ReadValue<Vector2>();
        Vector3 movement = new(_inputDir.x * _speed, _gravity, _inputDir.y * _speed);
        movement *= Time.deltaTime;
        Controller.Move(transform.TransformDirection(movement));

        Controller.transform.rotation = Quaternion.Euler(0.0f, CharacterCamera.transform.rotation.eulerAngles.y, 0.0f);
    }

    private void PlayStepsSound(InputAction.CallbackContext obj)
    {
        _playerAudioSource?.Play();
    }
    private void MuteStepsSound(InputAction.CallbackContext obj)
    {
        _playerAudioSource?.Stop();
    }
}

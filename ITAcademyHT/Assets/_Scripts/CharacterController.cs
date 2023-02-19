using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class Character : MonoBehaviour
{
    [SerializeField] private float _gravity = -9.81f;
    [SerializeField] float _mass = 5;
    [SerializeField] private float jumpSpeed = 7;
    [SerializeField] private float movementSpeed = 1;
    [SerializeField] private float sprintSpeed = 2;
    [SerializeField] private float rotationSpeed = 0.2f;
    [SerializeField] private float animationBlendSpeed = 0.2f;

    private InputAction _moveInput;
    private InputAction _runInput;
    private InputAction _jumpInput;
    private CharacterController controller;
    private Camera characterCamera;
    private Animator animator;
    private Controls _controller;
    private float rotationAngle = 0.0f;
    private float targetAnimationSpeed = 0.0f;
    private float currentSpeed;

    private bool isJumping = false;
    private float _vertSpeed = 0.0f;

    private Vector3 movement = Vector3.zero;
    private Vector3 rotatedMovement = Vector3.zero;
    public CharacterController Controller { get { return controller ??= GetComponent<CharacterController>(); } }

    public Camera CharacterCamera { get { return characterCamera ??= FindObjectOfType<Camera>(); } }

    public Animator CharacterAnimator { get { return animator ??= GetComponent<Animator>(); } }

    private void Awake()
    {
        _controller = new Controls();
        _moveInput = _controller.Controller.Movement;
        _runInput = _controller.Controller.Run;
        _jumpInput = _controller.Controller.Jump;
    }

    private void Start()
    {
        currentSpeed = movementSpeed;
    }

    private void OnEnable()
    {
        _controller.Enable();
        _jumpInput.performed += context => Jump();
        _moveInput.performed += context => Move(_controller.Controller.Movement.ReadValue<Vector2>());
        _moveInput.canceled += context => StopMove();
        _runInput.performed += context => Sprint();
        _runInput.canceled += context => OffSprint();
    }

    private void OnDisable()
    {
        _jumpInput.performed -= context => Jump();
        _moveInput.performed -= context => Move(_controller.Controller.Movement.ReadValue<Vector2>());
        _moveInput.canceled -= context => StopMove();
        _runInput.performed -= context => Sprint();
        _runInput.canceled -= context => OffSprint();
        _controller.Disable();
    }

    void Update()
    {
        _vertSpeed += _gravity * _mass;
        Vector2 inputDir = _controller.Controller.Movement.ReadValue<Vector2>();

        //CharacterAnimator.SetFloat("SpeedY", _vertSpeed / jumpSpeed);
        if (isJumping && _vertSpeed < 0)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, 1f, LayerMask.GetMask("Default")))
            {
                isJumping = false;
                CharacterAnimator.SetTrigger("Land");
            }
        }

        movement.x = inputDir.x;
        movement.z = inputDir.y;
        rotatedMovement = Quaternion.Euler(0.0f, CharacterCamera.transform.rotation.eulerAngles.y, 0.0f) * movement;
        //movement.y = _vertSpeed;

        movement *= Time.deltaTime;
        Controller.Move(CharacterCamera.transform.TransformDirection(movement));
        if (_moveInput.inProgress)
        {
            rotationAngle = Mathf.Atan2(rotatedMovement.x, rotatedMovement.z) * Mathf.Rad2Deg;
        }

        //CharacterAnimator.SetFloat("Speed", Mathf.Lerp(CharacterAnimator.GetFloat("Speed"), targetAnimationSpeed, animationBlendSpeed));
        Quaternion currentRotation = Controller.transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0.0f, rotationAngle, 0.0f);
        Controller.transform.localRotation = Quaternion.Lerp(currentRotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void Move(Vector2 dir)
    {
        CharacterAnimator.SetTrigger("Walk");
        movement.x = dir.x;
        movement.z = dir.y;
        targetAnimationSpeed = 0.5f * currentSpeed;
    }

    private void StopMove()
    {
        CharacterAnimator.SetTrigger("Idle");
        movement = Vector3.zero;
        targetAnimationSpeed = 0.0f;
    }

    private void Sprint()
    {
        if (_moveInput.inProgress)
        {
            CharacterAnimator.SetTrigger("Run");
            currentSpeed = sprintSpeed;
            targetAnimationSpeed = 1.0f * currentSpeed;
        }
    }

    private void OffSprint()
    {
        if (_moveInput.inProgress)
        {
            CharacterAnimator.SetTrigger("Walk");
            currentSpeed = movementSpeed;
            targetAnimationSpeed = 0.5f * currentSpeed;
        }
        else
        {
            CharacterAnimator.SetTrigger("Idle");
            targetAnimationSpeed = 0.0f;
        }
    }

    private void Jump()
    {
        if (!isJumping)
        {
            isJumping = true;
            CharacterAnimator.SetTrigger("Jump");
            _vertSpeed += jumpSpeed;
        }
    }
}

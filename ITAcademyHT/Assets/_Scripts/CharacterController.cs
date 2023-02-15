using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterController : MonoBehaviour
{
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpSpeed = 7;
    [SerializeField] private float movementSpeed = 2.0f;
    [SerializeField] private float sprintSpeed = 5.0f;
    [SerializeField] private float rotationSpeed = 0.2f;
    [SerializeField] private float animationBlendSpeed = 0.2f;

    private InputAction move;
    private InputAction sprint;
    private InputAction jump;
    private CharacterController controller;
    private Camera characterCamera;
    private Animator animator;
    private PlayerControls playerControls;
    private float rotationAngle = 0.0f;
    private float targetAnimationSpeed = 0.0f;
    private float currentSpeed;

    private bool isJumping = false;
    private float speedY = 0.0f;

    private Vector3 movement = Vector3.zero;
    private Vector3 rotatedMovement = Vector3.zero;
    public CharacterController Controller { get { return controller ??= GetComponent<CharacterController>(); } }

    public Camera CharacterCamera { get { return characterCamera ??= FindObjectOfType<Camera>(); } }

    public Animator CharacterAnimator { get { return animator ??= GetComponent<Animator>(); } }

    private void Awake()
    {
        playerControls = new PlayerControls();
        move = playerControls.Player.Move;
        sprint = playerControls.Player.Sprint;
        jump = playerControls.Player.Jump;
    }

    private void Start()
    {
        currentSpeed = movementSpeed;
    }

    private void OnEnable()
    {
        playerControls.Enable();
        jump.started += Jump;
        move.performed += Move;
        move.canceled += StopMove;
        sprint.performed += Sprint;
        sprint.canceled += OffSprint;
    }

    private void OnDisable()
    {
        jump.started -= Jump;
        sprint.canceled -= OffSprint;
        sprint.performed -= Sprint;
        move.performed -= Move;
        move.canceled -= StopMove;
        playerControls.Disable();
    }

    void Update()
    {
        if (!Controller.isGrounded)
        {
            speedY += gravity * Time.deltaTime;
        }
        else if (speedY < -0.1f)
        {
            speedY = -0.1f;
        }
        Debug.Log(speedY);
        Debug.Log(Controller.isGrounded);
        CharacterAnimator.SetFloat("SpeedY", speedY / jumpSpeed);
        if (isJumping && speedY < 0)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, 1f, LayerMask.GetMask("Default")))
            {
                isJumping = false;
                CharacterAnimator.SetTrigger("Land");
            }
        }

        Vector3 verticalMovement = Vector3.up * speedY;
        rotatedMovement = Quaternion.Euler(0.0f, CharacterCamera.transform.rotation.eulerAngles.y, 0.0f) * movement;

        Controller.Move((verticalMovement + rotatedMovement * currentSpeed) * Time.deltaTime);
        if (move.inProgress)
        {
            rotationAngle = Mathf.Atan2(rotatedMovement.x, rotatedMovement.z) * Mathf.Rad2Deg;
        }

        CharacterAnimator.SetFloat("Speed", Mathf.Lerp(CharacterAnimator.GetFloat("Speed"), targetAnimationSpeed, animationBlendSpeed));
        Quaternion currentRotation = Controller.transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0.0f, rotationAngle, 0.0f);
        Controller.transform.rotation = Quaternion.Lerp(currentRotation, targetRotation, rotationSpeed);
    }

    private void Move(InputAction.CallbackContext obj)
    {
        Vector2 moveDirection = obj.ReadValue<Vector2>();
        movement.x = moveDirection.x;
        movement.z = moveDirection.y;
        targetAnimationSpeed = 0.5f;
    }

    private void StopMove(InputAction.CallbackContext obj)
    {
        movement = Vector3.zero;
        targetAnimationSpeed = 0.0f;
    }

    private void Sprint(InputAction.CallbackContext obj)
    {
        if (move.inProgress)
        {
            currentSpeed = sprintSpeed;
            targetAnimationSpeed = 1.0f;
        }
    }

    private void OffSprint(InputAction.CallbackContext obj)
    {
        if (move.inProgress)
        {
            currentSpeed = movementSpeed;
            targetAnimationSpeed = 0.5f;
        }
        else
        {
            targetAnimationSpeed = 0.0f;
        }
    }

    private void Jump(InputAction.CallbackContext obj)
    {
        if (!isJumping)
        {
            isJumping = true;
            CharacterAnimator.SetTrigger("Jump");
            speedY += jumpSpeed;
        }
    }
}

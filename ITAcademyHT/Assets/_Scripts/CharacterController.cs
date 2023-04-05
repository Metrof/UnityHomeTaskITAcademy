using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

[RequireComponent(typeof(CharacterController))]
public class Character : MonoBehaviour
{
    [SerializeField] private float _gravity = -9.81f;
    [SerializeField] float _mass = 1;
    [SerializeField] private float _jumpSpeed = 7;
    [SerializeField] private float _movementSpeed = 3;
    [SerializeField] private float _sprintSpeed = 6;
    [SerializeField] private float _rotationSpeed = 4;
    [SerializeField] private float _rotationSprintSpeed = 10;
    [SerializeField] private float _animationBlendSpeed = 0.2f;
    [SerializeField] private PlayableDirector _deathPlayableDirector;

    private InputAction _runInput;
    private InputAction _jumpInput;
    private CharacterController controller;
    private Camera characterCamera;
    private Animator animator;
    private Controls _controller;

    private bool isDeath = false;

    private StateMachine _movementSM;
    private MoveState _moving;
    private JumpingState _jumping;
    private AttackingState _attacking;

    public float AnimationBlendSpeed { get { return _animationBlendSpeed; } }

    public float RotationSprintSpeed { get { return _rotationSprintSpeed; } }

    public float RotationSpeed { get { return _rotationSpeed; } }

    public float SprintSpeed { get { return _sprintSpeed; } }

    public float MovementSpeed { get { return _movementSpeed; } }

    public float Gravity { get { return _gravity; } }

    public float Mass { get { return _mass; } }

    public float JumpSpeed { get { return _jumpSpeed; } }

    public StateMachine StateMachine { get { return _movementSM ??=  new StateMachine(); } }

    public MoveState MoveState { get { return _moving ??= new MoveState(this, _movementSM); } }

    public JumpingState Jumping { get { return _jumping ??= new JumpingState(this, _movementSM); } }

    public AttackingState Attacking { get { return _attacking ??= new AttackingState(this, _movementSM); } }

    public CharacterController Controller { get { return controller ??= GetComponent<CharacterController>(); } }

    public Camera CharacterCamera { get { return characterCamera ??= FindObjectOfType<Camera>(); } }

    public Animator CharacterAnimator { get { return animator ??= GetComponent<Animator>(); } }

    public Controls ControlsController { get { return _controller ??= new Controls(); } }

    private void Awake()
    {
        _controller = new Controls();
        _runInput = _controller.Controller.Run;
        _jumpInput = _controller.Controller.Jump;
    }

    private void Start()
    {
        _movementSM = new StateMachine();
        _jumping = new JumpingState(this, _movementSM);
        _moving = new MoveState(this, _movementSM);
        _attacking = new AttackingState(this, _movementSM);

        _movementSM.Initialize(MoveState);
    }

    private void OnEnable()
    {
        _controller.Enable();
        _jumpInput.performed += context => MoveState.Jump();
        _runInput.performed += context => MoveState.EnterSprint();
        _runInput.canceled += context => MoveState.CancelSprint();
        _controller.Controller.Death.performed += context => Death();
        _controller.Controller.Respawn.performed += context => Respawn();
        _controller.Controller.RandomAttack.performed += context => MoveState.Attack();
    }

    private void OnDisable()
    {
        _controller.Controller.Death.performed -= context => Death();
        _controller.Controller.Respawn.performed -= context => Respawn();
        _controller.Controller.RandomAttack.performed -= context => MoveState.Attack();
        _jumpInput.performed -= context => MoveState.Jump();
        _runInput.performed -= context => MoveState.EnterSprint();
        _runInput.canceled -= context => MoveState.CancelSprint();
        _controller.Disable();
    }

    void Update()
    {
        _movementSM.CurrentState.HandleInput();

        _movementSM.CurrentState.LogicUpdate();
    }
    public void StartOfSpawn() => StateMachine.CurrentState.SwitchUnmoveble(true);
    public void EndOfSpawn() => StateMachine.CurrentState.SwitchUnmoveble(false);
    private void Death()
    {
        if (isDeath) return;
        StateMachine.CurrentState.SwitchUnmoveble(true);
        isDeath = true;
        CharacterAnimator.SetTrigger("Death");
        _deathPlayableDirector.Play();
    }
    private void Respawn()
    {
        if (!isDeath) return;
        isDeath = false;
        CharacterAnimator.SetTrigger("Respawn");
        _deathPlayableDirector.Stop();
    }
    public void EndOfAttack()
    {
        _attacking.CombinationEnd();
    }
    public void StartCutscene()
    {
        _controller.Disable();
    }
    public void CutsceneEnd()
    {
        _controller.Enable();
    }
}

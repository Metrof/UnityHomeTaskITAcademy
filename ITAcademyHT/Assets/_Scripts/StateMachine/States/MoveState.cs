using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{
    protected float _speed;
    protected float _rotationSpeed;
    protected float _animationBlendSpeed;

    private float _rotationAngle = 0.0f;
    protected float _targetAnimationSpeed;
    protected Vector3 _moveDir;
    private Vector2 _inputDir;
    private Vector3 _rotatedMovement;

    private bool _isSprint;
    private bool _isJumpiing;

    private float TargetAnimationSpeed
    {
        get 
        {
            if (_inputDir.sqrMagnitude > 0) return _isSprint ? 1 : 0.5f;
            return 0; 
        }
    }
    public MoveState(Character character, StateMachine stateMachine) : base(character, stateMachine)
    {
        _animationBlendSpeed = character.AnimationBlendSpeed;
    }

    public void EnterSprint()
    {
        _speed = character.SprintSpeed;
        _rotationSpeed = character.RotationSprintSpeed;
        _isSprint = true;
    }
    public void CancelSprint()
    {
        _speed = character.MovementSpeed;
        _rotationSpeed = character.RotationSpeed;
        _isSprint = false;
    }
    public override void Enter()
    {
        base.Enter();
        _isJumpiing = false;
        _speed = _isSprint ? character.SprintSpeed : character.MovementSpeed;
        _rotationSpeed = _isSprint ? character.RotationSprintSpeed : character.RotationSpeed;
    }
    public override void HandleInput()
    {
        base.HandleInput();

        _inputDir = character.ControlsController.Controller.Movement.ReadValue<Vector2>();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        _moveDir.x = _inputDir.x;
        _moveDir.z = _inputDir.y;
        _rotatedMovement = Quaternion.Euler(0.0f, character.CharacterCamera.transform.rotation.eulerAngles.y, 0.0f) * _moveDir;
        Vector3 down = Vector3.up * _vertSpeed;
        Vector3 hor = Vector3.zero;

        character.Controller.Move(down * Time.deltaTime + hor);

        if (_moveDir.sqrMagnitude > 0)
        {
            _rotationAngle = Mathf.Atan2(_rotatedMovement.x, _rotatedMovement.z) * Mathf.Rad2Deg;
        }
        character.CharacterAnimator.SetFloat("Speed", Mathf.Lerp(character.CharacterAnimator.GetFloat("Speed"), TargetAnimationSpeed, _animationBlendSpeed));
        Quaternion currentRotation = character.Controller.transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0.0f, _rotationAngle, 0.0f);

        if (!_isUnmoveble)
        {
            hor = character.CharacterCamera.transform.TransformDirection(_moveDir * _speed * Time.deltaTime);
            character.Controller.transform.rotation = Quaternion.Lerp(currentRotation, targetRotation, _rotationSpeed * Time.deltaTime);
        }

        character.Controller.Move(down * Time.deltaTime + hor);
    }
    public void Jump()
    {
        if (!_isJumpiing && character.StateMachine.CurrentState is MoveState)
        {
            character.StateMachine.ChangeState(character.Jumping);
            _isJumpiing = true;
        }
    }
    public void Attack()
    {
        if (!_isJumpiing)
        {
            character.StateMachine.ChangeState(character.Attacking);
        }
    }
}

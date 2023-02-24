using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public abstract class State 
{
    protected Character character;
    protected StateMachine stateMachine;


    protected bool _isUnmoveble = false;
    protected float _vertSpeed = 0.0f;
    private float _gravity;
    private float _mass;

    protected State(Character character, StateMachine stateMachine)
    {
        this.character = character;
        this.stateMachine = stateMachine;
    }

    public virtual void Enter()
    {
        _gravity = character.Gravity;
        _mass = character.Mass;
    }

    public virtual void HandleInput()
    {

    }

    public virtual void LogicUpdate()
    {
        if (!character.Controller.isGrounded)
        {
            _vertSpeed += _gravity * _mass * Time.deltaTime;
        }
        else if (_vertSpeed < -0.1f)
        {
            _vertSpeed = -0.1f;
        }
    }

    public virtual void Exit()
    {

    }

    public void SwitchUnmoveble(bool move) => _isUnmoveble = move;
}

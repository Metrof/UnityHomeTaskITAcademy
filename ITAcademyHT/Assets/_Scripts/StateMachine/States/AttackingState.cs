using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingState : State
{
    const int _attackCount = 3;
    private int _nextAttack;
    public AttackingState(Character character, StateMachine stateMachine) : base(character, stateMachine)
    {
    }
    public override void Enter()
    {
        base.Enter();
        character.CharacterAnimator.applyRootMotion = true;
        _isUnmoveble = true;
        _nextAttack = Random.Range(1, _attackCount + 1);
        character.CharacterAnimator.SetInteger("ComboNum", _nextAttack);
        _nextAttack = 0;
    }
    public void CombinationEnd()
    {
        if (_nextAttack == 0)
        {
            character.CharacterAnimator.SetInteger("ComboNum", _nextAttack);
            character.CharacterAnimator.applyRootMotion = false;
            _isUnmoveble = false;
            character.StateMachine.ChangeState(character.MoveState);
        }
    }
}

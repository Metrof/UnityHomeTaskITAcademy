using UnityEngine;

public class JumpingState : MoveState
{
    public JumpingState(Character character, StateMachine stateMachine) : base(character, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        character.CharacterAnimator.SetTrigger("Jump");
        _vertSpeed = character.JumpSpeed;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        character.CharacterAnimator.SetFloat("SpeedY", _vertSpeed / character.JumpSpeed);
        if (_vertSpeed < 0)
        {
            RaycastHit hit;
            if (Physics.Raycast(character.transform.position, Vector3.down, out hit, 0.8f, LayerMask.GetMask("Default")))
            {
                character.CharacterAnimator.SetTrigger("Land");
                character.StateMachine.ChangeState(character.MoveState);
            }
        }
    }
}

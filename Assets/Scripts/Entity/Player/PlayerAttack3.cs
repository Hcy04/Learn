using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack3 : PlayerPrimaryAttackState
{
    public PlayerAttack3(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        if (triggerCalled) 
        {
            stateMachine.ChangeState(player.idleState);
            player.attackTypeAhead = false;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}

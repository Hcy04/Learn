using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerAttack2 : PlayerPrimaryAttackState
{
    public PlayerAttack2(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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
            if (player.attackTypeAhead)
            {  
                stateMachine.ChangeState(player.attack3);
                player.attackTypeAhead = false;
            }
            else stateMachine.ChangeState(player.idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}

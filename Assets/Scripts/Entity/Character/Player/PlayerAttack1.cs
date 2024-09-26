using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack1 : PlayerPrimaryAttackState
{
    public PlayerAttack1(Player _player, string _animName) : base(_player, _animName)
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
                player.stateMachine.ChangeState(player.attack2);
                player.attackTypeAhead = false;
            }
            else player.stateMachine.ChangeState(player.idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}

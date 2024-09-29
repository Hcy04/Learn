using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerAttack2 : PlayerPrimaryAttackState
{
    public PlayerAttack2(Player _player, string _animName) : base(_player, _animName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.stats.damage.strength = .5f;
    }

    public override void Update()
    {
        base.Update();

        if (triggerCalled)
        {
            if (player.attackTypeAhead)
            {  
                player.stateMachine.ChangeState(player.attack3);
                player.attackTypeAhead = false;
            }
            else player.stateMachine.ChangeState(player.idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();

        player.comboCounter = 2;
    }
}

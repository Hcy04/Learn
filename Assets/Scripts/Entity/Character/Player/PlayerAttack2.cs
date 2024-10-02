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

        player.stats.damage.percentage = .5f;
    }

    public override void Update()
    {
        base.Update();

        if (triggerCalled)
        {
            if (player.dashTypeAhead) player.DoDash();
            else if (player.attackTypeAhead) player.stateMachine.ChangeState(player.attack3);
            else player.stateMachine.ChangeState(player.idleState);

            player.attackTypeAhead = false;
            player.dashTypeAhead = false;
        }
    }

    public override void Exit()
    {
        base.Exit();

        player.comboCounter = 2;
    }
}

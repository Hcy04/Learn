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

        player.stats.damage.percentage = 1;
    }

    public override void Update()
    {
        base.Update();

        if (triggerCalled)
        {
            if (player.dashTypeAhead) player.DoDash();
            else if (player.attackTypeAhead) player.stateMachine.ChangeState(player.attack2);
            else player.stateMachine.ChangeState(player.idleState);

            player.attackTypeAhead = false;
            player.dashTypeAhead = false;
        }
    }

    public override void Exit()
    {
        base.Exit();

        player.comboCounter = 1;
    }
}

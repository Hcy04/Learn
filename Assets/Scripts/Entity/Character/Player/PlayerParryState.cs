using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParryState : PlayerState
{
    public PlayerParryState(Player _player, string _animName) : base(_player, _animName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = player.counterAttackDuration;
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0) player.stateMachine.ChangeState(player.idleState);
    }

    public override void Exit()
    {
        base.Exit();
    }
}

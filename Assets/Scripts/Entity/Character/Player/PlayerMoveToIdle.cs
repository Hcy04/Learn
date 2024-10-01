using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveToIdle : PlayerGroundedState
{
    public PlayerMoveToIdle(Player _player, string _animName) : base(_player, _animName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        player.SetVelocity(player.facingDir * player.moveSpeed / 2, player.rb.velocity.y);

        if (triggerCalled) player.stateMachine.ChangeState(player.idleState);
    }

    public override void Exit()
    {
        base.Exit();
    }
}

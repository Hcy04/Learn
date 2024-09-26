using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerState
{
    public PlayerWallJumpState(Player _player, string _animName) : base(_player, _animName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = player.wallJumpDuration;
        player.SetVelocity(5 * -player.facingDir, player.jumpForce);
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0) player.stateMachine.ChangeState(player.airState);
    }

    public override void Exit()
    {
        base.Exit();
    }
}

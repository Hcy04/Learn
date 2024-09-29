using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class PlayerAirState : PlayerInAirState
{
    public PlayerAirState(Player _player, string _animName) : base(_player, _animName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        if (player.IsGroundDetected())
        {
            if (xInput == 0) player.stateMachine.ChangeState(player.idleState);
            else player.stateMachine.ChangeState(player.moveState);
        }
        else if (player.IsWallDetected() && xInput == player.facingDir) player.stateMachine.ChangeState(player.wallSlide);
        else if (xInput != 0) player.SetVelocity(xInput * player.moveSpeed * 0.8f, player.rb.velocity.y);
    }

    public override void Exit()
    {
        base.Exit();
    }
}

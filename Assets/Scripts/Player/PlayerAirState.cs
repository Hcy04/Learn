using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class PlayerAirState : PlayerState
{
    public PlayerAirState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        if (player.IsGroundDetected()) stateMachine.ChangeState(player.idleState);
        if (player.IsWallDetected() && xInput == player.facingDir) stateMachine.ChangeState(player.wallSlide);
        if (xInput != 0) player.SetVelocity(xInput * player.moveSpeed * 0.8f, rb.velocity.y);
    }

    public override void Exit()
    {
        base.Exit();
    }
}

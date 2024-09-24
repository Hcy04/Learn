using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class PlayerWallSlideState : PlayerState
{
    public PlayerWallSlideState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
        //无效化WallSlide时触发的ChangeState(player.dashState)
        if (stateMachine.currentState == player.dashState) stateMachine.ChangeState(player.wallSlide);

        if (!player.IsWallDetected() || xInput != player.facingDir) stateMachine.ChangeState(player.airState);
        if (player.IsGroundDetected()) stateMachine.ChangeState(player.idleState);

        if (Input.GetKeyDown(KeyCode.Space)) stateMachine.ChangeState(player.wallJump);
        else 
        {
            if (yInput < 0) rb.velocity = new Vector2(0, rb.velocity.y);
            else rb.velocity = new Vector2(0, rb.velocity.y * 0.5f);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}

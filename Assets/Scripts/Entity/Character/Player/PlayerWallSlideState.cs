using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class PlayerWallSlideState : PlayerState
{
    public PlayerWallSlideState(Player _player, string _animName) : base(_player, _animName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        if (!player.IsWallDetected() || xInput != player.facingDir) player.stateMachine.ChangeState(player.airState);
        if (player.IsGroundDetected()) player.stateMachine.ChangeState(player.idleState);

        if (Input.GetKeyDown(KeyCode.Space)) player.stateMachine.ChangeState(player.wallJump);
        else 
        {
            if (yInput < 0) player.rb.velocity = new Vector2(0, player.rb.velocity.y);
            else player.rb.velocity = new Vector2(0, player.rb.velocity.y * 0.5f);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}

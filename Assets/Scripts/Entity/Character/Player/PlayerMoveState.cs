using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player _player, string _animName) : base(_player, _animName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = 1.5f;
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer >= 0 && xInput == -player.facingDir) stateTimer = 1.5f;//转向重跑
        
        if ((xInput == 0 || player.IsWallDetected() || xInput == -player.facingDir) && stateTimer < 0)
            player.stateMachine.ChangeState(player.moveToIdle);
        else
        {
            player.SetVelocity(xInput * player.moveSpeed, player.rb.velocity.y);
            if (xInput == 0 || player.IsWallDetected()) player.stateMachine.ChangeState(player.idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}

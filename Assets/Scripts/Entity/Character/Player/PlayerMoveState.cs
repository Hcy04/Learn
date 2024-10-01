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
        
        if (xInput == 0 || player.IsWallDetected() || (xInput == -player.facingDir && stateTimer < 0))//停止 有障碍物 转向
        {
            if (stateTimer < 0) player.stateMachine.ChangeState(player.moveToIdle);//单方向移动超1.5s 急停
            else if (xInput == 0 || player.IsWallDetected()) player.stateMachine.ChangeState(player.idleState);//转向不经过Idle
        }
        else player.SetVelocity(xInput * player.moveSpeed, player.rb.velocity.y);
    }

    public override void Exit()
    {
        base.Exit();
    }
}

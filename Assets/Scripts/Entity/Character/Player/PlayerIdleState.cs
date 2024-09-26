using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(Player _player, string _animName) : base(_player, _animName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
        player.SetVelocity(0, 0);
    }

    public override void Update()
    {
        base.Update();
        
        if (xInput != 0 && !player.IsWallDetected() || (player.IsWallDetected() &&(xInput == -player.facingDir)))
            player.stateMachine.ChangeState(player.moveState);
        else if (!Input.GetKeyDown(KeyCode.Space)) player.SetVelocity(0, 0);
    }

    public override void Exit()
    {
        base.Exit();
    }
}

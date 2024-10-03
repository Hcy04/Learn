using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryAttackState : PlayerState
{
    public PlayerPrimaryAttackState(Player _player, string _animName) : base(_player, _animName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = .05f;
        player.attackTypeAhead = false;
    }

    public override void Update()
    {
        base.Update();

        float attackDir = 1;
        if (xInput == -player.facingDir) attackDir = -1;

        if (stateTimer > 0) player.rb.velocity = new Vector2(player.rb.velocity.x, 0);
        else player.SetVelocity(player.attackMoveSpeed * player.facingDir * attackDir, 0);

        if (Input.GetKeyDown(KeyCode.Mouse0)) player.attackTypeAhead = true;
        if (Input.GetKeyDown(KeyCode.LeftShift)) player.dashTypeAhead = true;

        if (!player.IsGroundDetected()) player.stateMachine.ChangeState(player.jumpToAir2);
    }

    public override void Exit()
    {
        base.Exit();
        
        player.lastTimeAttacked = Time.time;
        
    }
}

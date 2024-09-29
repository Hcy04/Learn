using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerInAirState
{
    public PlayerJumpState(Player _player, string _animName) : base(_player, _animName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.rb.velocity = new Vector2(player.rb.velocity.x, player.jumpForce);
    }

    public override void Update()
    {
        base.Update();

        if (player.rb.velocity.y < player.rb.gravityScale / 2f) player.stateMachine.ChangeState(player.jumpToAir1);
    }

    public override void Exit()
    {
        base.Exit();
    }
}

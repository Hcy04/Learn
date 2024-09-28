using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpToAirState2 : PlayerState
{
    public PlayerJumpToAirState2(Player _player, string _animName) : base(_player, _animName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        if (player.rb.velocity.y < -player.rb.gravityScale / 2f) player.stateMachine.ChangeState(player.airState);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
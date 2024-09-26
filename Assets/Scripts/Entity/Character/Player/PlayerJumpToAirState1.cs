using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpToAirState1 : PlayerState
{
    public PlayerJumpToAirState1(Player _player, string _animName) : base(_player, _animName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        if (player.rb.velocity.y < 0) player.stateMachine.ChangeState(player.jumpToAir2);
    }

    public override void Exit()
    {
        base.Exit();
    }
}

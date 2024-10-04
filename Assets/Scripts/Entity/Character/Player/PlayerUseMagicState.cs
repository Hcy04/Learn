using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUseMagicState : PlayerState
{
    public PlayerUseMagicState(Player _player, string _animName) : base(_player, _animName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = .5f;
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0) player.stateMachine.ChangeState(player.idleState);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
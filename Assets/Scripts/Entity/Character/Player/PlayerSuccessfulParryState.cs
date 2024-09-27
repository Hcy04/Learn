using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSuccessfulParryState : PlayerState
{
    public PlayerSuccessfulParryState(Player _player, string _animName) : base(_player, _animName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        if (triggerCalled)
        {
            player.stateMachine.ChangeState(player.idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}

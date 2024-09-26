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

        if (Input.GetKeyDown(KeyCode.Mouse0)) player.attackTypeAhead = true;

        if (triggerCalled)
        {
            if (player.attackTypeAhead)
            {
                player.stateMachine.ChangeState(player.attack3);
                player.attackTypeAhead = false;
            }
            else player.stateMachine.ChangeState(player.idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}

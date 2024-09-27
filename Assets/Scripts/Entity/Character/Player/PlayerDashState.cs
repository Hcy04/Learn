using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(Player _player, string _animName) : base(_player, _animName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = player.dashDuration;
        player.skill.clone.CreatClone(player.transform);
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Mouse0)) player.attackTypeAhead = true;

        if (stateTimer < 0)
        {
            if (player.attackTypeAhead)
            {
                if (player.comboCounter == 2) player.stateMachine.ChangeState(player.attack3);
                else if (player.comboCounter == 1) player.stateMachine.ChangeState(player.attack2);
                else if (player.comboCounter == 0) player.stateMachine.ChangeState(player.attack1);
            }
            else player.stateMachine.ChangeState(player.idleState);
        }
        else if (!player.IsGroundDetected() && player.IsWallDetected()) player.stateMachine.ChangeState(player.wallSlide);
        else player.SetVelocity(player.dashSpeed * player.dashDir, 0);
    }

    public override void Exit()
    {
        base.Exit();

        player.SetVelocity(0, 0);
    }
}

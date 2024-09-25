using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = player.dashDuration;
        SkillManager.instance.clone.CreatClone(player.transform);
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Mouse0)) player.attackTypeAhead = true;

        if (stateTimer < 0)
        {
            if (player.attackTypeAhead)
            {
                if (player.comboCounter == 2) stateMachine.ChangeState(player.attack3);
                else if (player.comboCounter == 1) stateMachine.ChangeState(player.attack2);
                else if (player.comboCounter == 0) stateMachine.ChangeState(player.attack1);
            }
            else stateMachine.ChangeState(player.idleState);
        }
        else if (!player.IsGroundDetected() && player.IsWallDetected()) stateMachine.ChangeState(player.wallSlide);
        else player.SetVelocity(player.dashSpeed * player.dashDir, 0);
    }

    public override void Exit()
    {
        base.Exit();

        player.ZeroVelocity();
    }
}

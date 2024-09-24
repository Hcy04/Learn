using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        if (Time.time - player.lastTimeAttacked > player.comboWindow) player.comboCounter = 0;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (player.comboCounter == 2) stateMachine.ChangeState(player.attack3);
            else if (player.comboCounter == 1) stateMachine.ChangeState(player.attack2);
            else if (player.comboCounter == 0) stateMachine.ChangeState(player.attack1);
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1)) stateMachine.ChangeState(player.counterAttack);
        else if (Input.GetKeyDown(KeyCode.Space) && player.IsGroundDetected()) stateMachine.ChangeState(player.jumpState);
        else if (!player.IsGroundDetected()) stateMachine.ChangeState(player.airState);
        else if (Input.GetKeyDown(KeyCode.F)) stateMachine.ChangeState(player.aimSword);
    }

    public override void Exit()
    {
        base.Exit();
    }
}

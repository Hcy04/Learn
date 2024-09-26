using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(Player _player, string _animName) : base(_player, _animName)
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
            if (player.comboCounter == 2) player.stateMachine.ChangeState(player.attack3);
            else if (player.comboCounter == 1) player.stateMachine.ChangeState(player.attack2);
            else if (player.comboCounter == 0) player.stateMachine.ChangeState(player.attack1);
        }
        else if (Input.GetKeyDown(KeyCode.Q)) player.stateMachine.ChangeState(player.counterAttack);
        else if (Input.GetKeyDown(KeyCode.Space) && player.IsGroundDetected()) player.stateMachine.ChangeState(player.jumpState);
        else if (!player.IsGroundDetected()) player.stateMachine.ChangeState(player.airState);
        else if (Input.GetKeyDown(KeyCode.Mouse1)) player.stateMachine.ChangeState(player.aimSword);
    }

    public override void Exit()
    {
        base.Exit();
    }
}

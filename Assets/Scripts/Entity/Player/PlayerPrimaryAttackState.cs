using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryAttackState : PlayerState
{
    public PlayerPrimaryAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = .05f;
        player.attackTypeAhead = false;
    }

    public override void Update()
    {
        base.Update();

        float attackDir = 1;
        if (xInput == -player.facingDir) attackDir = -1;

        if (stateTimer > 0) rb.velocity = new Vector2(rb.velocity.x, 0);
        else player.SetVelocity(player.attackMoveSpeed * player.facingDir * attackDir, 0);

        if (Input.GetKeyDown(KeyCode.Mouse0)) player.attackTypeAhead = true;
    }

    public override void Exit()
    {
        base.Exit();

        player.comboCounter = (player.comboCounter + 1) % 3;
        player.lastTimeAttacked = Time.time;
    }
}

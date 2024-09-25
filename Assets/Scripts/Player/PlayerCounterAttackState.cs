using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCounterAttackState : PlayerState
{
    public PlayerCounterAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = player.counterAttackDuration;
    }

    public override void Update()
    {
        base.Update();

        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                if (hit.GetComponent<Enemy>().TryStunned())
                {
                    stateTimer = 10;
                    player.anim.SetBool("SuccessfulCounterAttack", true);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse0)) player.attackTypeAhead = true;

        if (stateTimer < 0) stateMachine.ChangeState(player.idleState);
        else if (triggerCalled)
        {
            player.anim.SetBool("SuccessfulCounterAttack", false);
            if (player.attackTypeAhead)
            {
                stateMachine.ChangeState(player.attack3);
                player.attackTypeAhead = false;
            }
            else stateMachine.ChangeState(player.idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}

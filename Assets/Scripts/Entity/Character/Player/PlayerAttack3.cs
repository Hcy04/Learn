using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack3 : PlayerPrimaryAttackState
{
    public PlayerAttack3(Player _player, string _animName) : base(_player, _animName)
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
            player.attackTypeAhead = false;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}

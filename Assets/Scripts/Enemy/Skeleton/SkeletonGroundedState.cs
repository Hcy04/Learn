using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonGroundedState : SkeletonState
{
    protected Transform player;

    public SkeletonGroundedState(Enemy_Skeleton _enemy_Skeleton, string _animName) : base(_enemy_Skeleton, _animName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player = PlayerManager.instance.player.transform;
    }

    public override void Update()
    {
        base.Update();

        if ((enemy_Skeleton.IsPlayerDetected() && !enemy_Skeleton.IsWallDetected() && enemy_Skeleton.IsGroundDetected())
            || Vector2.Distance(enemy_Skeleton.transform.position, player.position) < 2)
            enemy_Skeleton.stateMachine.ChangeState(enemy_Skeleton.battleState);
    }

    public override void Exit()
    {
        base.Exit();
    }
}

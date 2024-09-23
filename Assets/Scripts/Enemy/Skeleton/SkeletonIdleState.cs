using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonIdleState : SkeletonGroundedState
{
    public SkeletonIdleState(Enemy_Skeleton _enemy_Skeleton, EnemyStateMachine _stateMachine, string _animBoolName) : base(_enemy_Skeleton, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = enemy_Skeleton.idleTime;
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0) stateMachine.ChangeState(enemy_Skeleton.moveState);
        enemy_Skeleton.ZeroVelocity();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
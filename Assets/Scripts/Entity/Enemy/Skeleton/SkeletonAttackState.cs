using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAttackState : SkeletonState
{
    public SkeletonAttackState(Enemy_Skeleton _enemy_Skeleton, EnemyStateMachine _stateMachine, string _animBoolName) : base(_enemy_Skeleton, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        if (triggerCalled) stateMachine.ChangeState(enemy_Skeleton.battleState);
        else enemy_Skeleton.ZeroVelocity();
    }

    public override void Exit()
    {
        base.Exit();

        enemy_Skeleton.lastTimeAttacked = Time.time;
    }
}

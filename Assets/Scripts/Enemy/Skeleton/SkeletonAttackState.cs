using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAttackState : SkeletonState
{
    public SkeletonAttackState(Enemy_Skeleton _enemy_Skeleton, string _animName) : base(_enemy_Skeleton, _animName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        if (triggerCalled) enemy_Skeleton.stateMachine.ChangeState(enemy_Skeleton.battleState);
        else enemy_Skeleton.ZeroVelocity();
    }

    public override void Exit()
    {
        base.Exit();

        enemy_Skeleton.lastTimeAttacked = Time.time;
    }
}

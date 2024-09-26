using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAttackState : SkeletonState
{
    public SkeletonAttackState(Enemy_Skeleton _Skeleton, string _animName) : base(_Skeleton, _animName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        if (triggerCalled) Skeleton.stateMachine.ChangeState(Skeleton.battleState);
        else Skeleton.SetVelocity(0, 0);
    }

    public override void Exit()
    {
        base.Exit();

        Skeleton.lastTimeAttacked = Time.time;
    }
}

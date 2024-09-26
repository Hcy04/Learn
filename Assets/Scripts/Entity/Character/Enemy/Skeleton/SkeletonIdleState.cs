using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonIdleState : SkeletonGroundedState
{
    public SkeletonIdleState(Enemy_Skeleton _Skeleton, string _animName) : base(_Skeleton, _animName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = Skeleton.idleTime;
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
        {
            Skeleton.stateMachine.ChangeState(Skeleton.moveState);
        }
        Skeleton.SetVelocity(0, 0);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
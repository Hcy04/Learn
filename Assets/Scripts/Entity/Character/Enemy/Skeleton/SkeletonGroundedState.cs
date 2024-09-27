using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonGroundedState : SkeletonState
{
    public SkeletonGroundedState(Enemy_Skeleton _Skeleton, string _animName) : base(_Skeleton, _animName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        if ((Skeleton.IsPlayerDetected() && !Skeleton.IsWallDetected() && Skeleton.IsGroundDetected())
            || Vector2.Distance(Skeleton.transform.position, Skeleton.player.transform.position) < 2)
            Skeleton.stateMachine.ChangeState(Skeleton.battleState);
    }

    public override void Exit()
    {
        base.Exit();
    }
}

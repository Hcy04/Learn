using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class SkeletonMoveState : SkeletonGroundedState
{
    public SkeletonMoveState(Enemy_Skeleton _Skeleton, string _animName) : base(_Skeleton, _animName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        if (Skeleton.IsWallDetected() || !Skeleton.IsGroundDetected()) Skeleton.Flip();
    }

    public override void Update()
    {
        base.Update();

        Skeleton.SetVelocity(Skeleton.moveSpeed * Skeleton.facingDir, Skeleton.rb.velocity.y);

        if (Skeleton.IsWallDetected() || !Skeleton.IsGroundDetected())
            Skeleton.stateMachine.ChangeState(Skeleton.idleState);
    }

    public override void Exit()
    {
        base.Exit();
    }
}

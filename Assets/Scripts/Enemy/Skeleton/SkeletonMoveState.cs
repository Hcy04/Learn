using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class SkeletonMoveState : SkeletonGroundedState
{
    public SkeletonMoveState(Enemy_Skeleton _enemy_Skeleton, string _animName) : base(_enemy_Skeleton, _animName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        if (enemy_Skeleton.IsWallDetected() || !enemy_Skeleton.IsGroundDetected()) enemy_Skeleton.Flip();
    }

    public override void Update()
    {
        base.Update();

        enemy_Skeleton.SetVelocity(enemy_Skeleton.moveSpeed * enemy_Skeleton.facingDir, rb.velocity.y);

        if (enemy_Skeleton.IsWallDetected() || !enemy_Skeleton.IsGroundDetected())
            enemy_Skeleton.stateMachine.ChangeState(enemy_Skeleton.idleState);
    }

    public override void Exit()
    {
        base.Exit();
    }
}

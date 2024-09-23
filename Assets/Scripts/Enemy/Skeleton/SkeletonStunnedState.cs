using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonStunnedState : SkeletonState
{
    public SkeletonStunnedState(Enemy_Skeleton _enemy_Skeleton, EnemyStateMachine _stateMachine, string _animBoolName) : base(_enemy_Skeleton, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        enemy_Skeleton.fx.InvokeRepeating("RedColorBlink", 0, .1f);

        stateTimer = enemy_Skeleton.stunDuration;
        rb.velocity = new Vector2(-enemy_Skeleton.facingDir * enemy_Skeleton.stunDirection.x, enemy_Skeleton.stunDirection.y);
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0) stateMachine.ChangeState(enemy_Skeleton.idleState);
    }

    public override void Exit()
    {
        base.Exit();

        enemy_Skeleton.fx.Invoke("CancelRedBlink", 0);
    }
}

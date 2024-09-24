using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using UnityEngine;

public class SkeletonBattleState : SkeletonState
{
    Transform player;
    private int moveDir;

    public SkeletonBattleState(Enemy_Skeleton _enemy_Skeleton, EnemyStateMachine _stateMachine, string _animBoolName) : base(_enemy_Skeleton, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player = PlayerManager.instance.player.transform;
        enemy_Skeleton.anim.speed = 2;
    }

    public override void Update()
    {
        base.Update();

        if (enemy_Skeleton.IsPlayerDetected())
        {
            stateTimer = enemy_Skeleton.battleTime;
            if (enemy_Skeleton.IsPlayerDetected().distance < enemy_Skeleton.attackDistance)
            {
                if (Time.time - enemy_Skeleton.lastTimeAttacked > enemy_Skeleton.attackCD)
                {
                    stateMachine.ChangeState(enemy_Skeleton.attackState);
                }
            }
        }
        
        if (!enemy_Skeleton.IsGroundDetected() || stateTimer < 0)
            stateMachine.ChangeState(enemy_Skeleton.idleState);

        if (player.position.x > enemy_Skeleton.transform.position.x) moveDir = 1;
        else if (player.position.x < enemy_Skeleton.transform.position.x) moveDir = -1;
        else moveDir = 0;

        enemy_Skeleton.SetVelocity(enemy_Skeleton.moveSpeed * moveDir * 1.5f, rb.velocity.y);
    }

    public override void Exit()
    {
        base.Exit();

        enemy_Skeleton.anim.speed = 1;
    }
}

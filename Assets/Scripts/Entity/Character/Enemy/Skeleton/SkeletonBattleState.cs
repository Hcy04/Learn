using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using UnityEngine;

public class SkeletonBattleState : SkeletonState
{
    Transform player;
    private int moveDir;

    public SkeletonBattleState(Enemy_Skeleton _Skeleton, string _animName) : base(_Skeleton, _animName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player = PlayerManager.instance.player.transform;
        Skeleton.anim.speed = 2;
    }

    public override void Update()
    {
        base.Update();

        if (Skeleton.IsPlayerDetected())
        {
            stateTimer = Skeleton.battleTime;
            if (Skeleton.IsPlayerDetected().distance < Skeleton.attackDistance)
            {
                if (Time.time - Skeleton.lastTimeAttacked > Skeleton.attackCD)
                {
                    Skeleton.stateMachine.ChangeState(Skeleton.attackState);
                }
            }
        }
        
        if (!Skeleton.IsGroundDetected() || stateTimer < 0)
            Skeleton.stateMachine.ChangeState(Skeleton.idleState);

        if (player.position.x > Skeleton.transform.position.x) moveDir = 1;
        else if (player.position.x < Skeleton.transform.position.x) moveDir = -1;
        else moveDir = 0;

        Skeleton.SetVelocity(Skeleton.moveSpeed * moveDir * 1.5f, Skeleton.rb.velocity.y);
    }

    public override void Exit()
    {
        base.Exit();

        Skeleton.anim.speed = 1;
    }
}

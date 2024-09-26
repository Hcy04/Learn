using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonStunnedState : SkeletonState
{
    public SkeletonStunnedState(Enemy_Skeleton _Skeleton, string _animName) : base(_Skeleton, _animName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        Skeleton.fx.InvokeRepeating("RedColorBlink", 0, .1f);

        stateTimer = Skeleton.stunDuration;
        Skeleton.rb.velocity = new Vector2(-Skeleton.facingDir * Skeleton.stunDirection.x, Skeleton.stunDirection.y);
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0) Skeleton.stateMachine.ChangeState(Skeleton.idleState);
    }

    public override void Exit()
    {
        base.Exit();

        Skeleton.fx.Invoke("CancelRedBlink", 0);
    }
}

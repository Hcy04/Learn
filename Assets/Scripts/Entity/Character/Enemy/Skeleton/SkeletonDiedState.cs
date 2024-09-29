using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkeletonDiedState : SkeletonState
{
    public SkeletonDiedState(Enemy_Skeleton _Skeleton, string _animName) : base(_Skeleton, _animName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        Skeleton.gameObject.layer = 14;
        stateTimer = 5;
    }

    public override void Update()
    {
        base.Update();
        
        if (stateTimer < 0) Skeleton.sr.color = new Color(1, 1, 1, Skeleton.sr.color.a - Time.deltaTime);
        if (Skeleton.sr.color.a <= 0) Skeleton.DestroySelf();
    }

    public override void Exit()
    {
        base.Exit();
    }
}

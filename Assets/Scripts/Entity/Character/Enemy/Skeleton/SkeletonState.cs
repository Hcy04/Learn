using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonState : EnemyState
{
    protected Enemy_Skeleton Skeleton;

    public SkeletonState(Enemy_Skeleton _Skeleton, string _animName) : base(_Skeleton, _animName)
    {
        this.Skeleton = _Skeleton;
    }
}

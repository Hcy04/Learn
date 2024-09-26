using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonState : EnemyState
{
    protected Enemy_Skeleton enemy_Skeleton;

    public SkeletonState(Enemy_Skeleton _enemy_Skeleton, string _animName) : base(_enemy_Skeleton, _animName)
    {
        this.enemy_Skeleton = _enemy_Skeleton;
    }
}

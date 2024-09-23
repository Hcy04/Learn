using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonState : EnemyState
{
    protected Enemy_Skeleton enemy_Skeleton;

    public SkeletonState(Enemy_Skeleton _enemy_Skeleton, EnemyStateMachine _stateMachine, string _animBoolName) : base(_enemy_Skeleton, _stateMachine, _animBoolName)
    {
        this.enemy_Skeleton = _enemy_Skeleton;
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy_Skeleton : Enemy
{
    #region States
    public SkeletonIdleState idleState { get; private set; }
    public SkeletonMoveState moveState { get; private set; }
    public SkeletonBattleState battleState { get; private set; }
    public SkeletonAttackState attackState { get; private set; }
    public SkeletonStunnedState stunnedState { get; private set; }
    #endregion

    protected override void Awake()
    {
        base.Awake();

        idleState = new SkeletonIdleState(this, "Idle");
        moveState = new SkeletonMoveState(this, "Move");
        battleState = new SkeletonBattleState(this, "Move");
        attackState = new SkeletonAttackState(this, "Attack");
        stunnedState = new SkeletonStunnedState(this, "Stunned");
    }

    protected override void Start()
    {
        base.Start();

        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
    }

    public override bool TryStunned()
    {
        if (base.TryStunned())
        {
            stateMachine.ChangeState(stunnedState);
            return true;
        }
        return false;
    }
}

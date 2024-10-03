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

    public SkeletonDiedState diedState { get; private set; }
    #endregion

    public float idleTime = 1;

    protected override void Awake()
    {
        base.Awake();

        idleState = new SkeletonIdleState(this, "Idle");
        moveState = new SkeletonMoveState(this, "Move");
        battleState = new SkeletonBattleState(this, "Move");
        attackState = new SkeletonAttackState(this, "Attack");
        stunnedState = new SkeletonStunnedState(this, "Stunned");

        diedState = new SkeletonDiedState(this, "Died");
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

    #region Change State
    public override void IsStunned()
    {
        base.IsStunned();

        stateMachine.ChangeState(stunnedState);
    }

    public override void IsBattle()
    {
        base.IsBattle();

        stateMachine.ChangeState(battleState);
    }

    public override void IsDied()
    {
        base.IsDied();

        stateMachine.ChangeState(diedState);
    }
    #endregion
}

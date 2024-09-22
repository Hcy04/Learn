using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    #region States
    public EnemyStateMachine stateMachine { get; private set; }

    public EnemyIdleState idleState { get; private set; }
    #endregion

    protected override void Awake()
    {
        base.Awake();

        stateMachine = new EnemyStateMachine();

        idleState = new EnemyIdleState(this, stateMachine, "Idle");
    }

    protected override void Start()
    {
        base.Start();

        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();

        stateMachine.currentState.Update();
    }
}

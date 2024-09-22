using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyState
{
    protected Enemy enemy;
    protected EnemyStateMachine enemyStateMachine;
    private string animBoolName;

    protected float stateTimer;
    protected bool triggerCalled;

    public EnemyState(Enemy _enemy, EnemyStateMachine _enemyStateMachine, string _animBoolName)
    {
        this.enemy = _enemy;
        this.enemyStateMachine = _enemyStateMachine;
        this.animBoolName = _animBoolName;
    }

    public virtual  void Enter()
    {
        enemy.anim.SetBool(animBoolName, true);

        triggerCalled = false;
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
    }

    public virtual void Exit()
    {
        enemy.anim.SetBool(animBoolName, false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyState : State
{
    protected Enemy enemy;

    protected float stateTimer;
    protected bool triggerCalled;

    protected Rigidbody2D rb;

    public EnemyState(Enemy _enemy,  string _animName) : base(_enemy, _animName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        triggerCalled = false;
        rb = enemy.rb;
    }

    public override void Update()
    {
        base.Update();
        
        stateTimer -= Time.deltaTime;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }
}

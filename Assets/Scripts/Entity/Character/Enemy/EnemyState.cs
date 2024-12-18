using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyState : CharacterState
{
    protected Enemy enemy;

    public EnemyState(Enemy _enemy,  string _animName) : base(_enemy, _animName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        enemy.CheckFreeze();
        if (enemy.isFreeze) return;

        base.Update();

        if (enemy.dropFlag) enemy.GenerateDrop();
    }

    public override void Exit()
    {
        base.Exit();
    }
}

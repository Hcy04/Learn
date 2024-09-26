using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterState : State
{
    protected Character character;

    protected float stateTimer;
    protected bool triggerCalled;

    public CharacterState(Character _character, string _animName) : base(_character, _animName)
    {
        this.character = _character;
    }

    public override void Enter()
    {
        base.Enter();

        triggerCalled = false;
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

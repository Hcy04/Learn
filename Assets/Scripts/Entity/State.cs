using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class State
{
    protected Entity entity;
    protected string animName;

    public State(Entity _entity, string _animName)
    {
        this.entity = _entity;
        this.animName = _animName;
    }

    public virtual void Enter()
    {
        entity.anim.Play(animName);
    }

    public virtual void Update()
    {

    }

    public virtual void Exit()
    {
        
    }
}

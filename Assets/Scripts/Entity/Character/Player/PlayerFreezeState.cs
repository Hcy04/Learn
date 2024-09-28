using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFreezeState : PlayerState
{
    public PlayerFreezeState(Player _player, string _animName) : base(_player, _animName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.sr.enabled = false;
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Exit()
    {
        base.Exit();

        player.sr.enabled = true;
    }
}

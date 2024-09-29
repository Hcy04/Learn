using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDiedState : PlayerState
{
    public PlayerDiedState(Player _player, string _animName) : base(_player, _animName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.gameObject.layer = 14;
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Exit()
    {
        base.Exit();
    }
}

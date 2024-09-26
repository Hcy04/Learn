using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : CharacterState
{
    protected Player player;

    protected float xInput;
    protected float yInput;

    public PlayerState(Player _player, string _animName) : base(_player, _animName)
    {
        this.player = _player;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");

        player.CheckForDashInput();
    }

    public override void Exit()
    {
        base.Exit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimSwordState : PlayerState
{
    public PlayerAimSwordState(Player _player, string _animName) : base(_player, _animName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.skill.sword.DotsActive(true);
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetKeyUp(KeyCode.Mouse1)) player.stateMachine.ChangeState(player.throwSword);
        else
        {
            player.SetVelocity(0, 0);
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (mousePosition.x < player.transform.position.x && player.facingDir == 1) player.Flip();
            else if (mousePosition.x > player.transform.position.x && player.facingDir == -1) player.Flip();
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}

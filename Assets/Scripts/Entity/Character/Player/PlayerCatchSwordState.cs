using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerCatchSwordState : PlayerState
{
    float swordX;

    public PlayerCatchSwordState(Player _player, string _animName) : base(_player, _animName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        swordX = player.sword.transform.position.x;
        if (swordX < player.transform.position.x && player.facingDir == 1) player.Flip();
        else if (swordX > player.transform.position.x && player.facingDir == -1) player.Flip();

        float speedDir = 1;
        if (swordX - player.transform.position.x > 0) speedDir = -1;
        player.rb.velocity += new Vector2(speedDir * player.moveSpeed, player.rb.velocity.y);
    }

    public override void Update()
    {
        base.Update();

        if (triggerCalled)
        {
            player.stateMachine.ChangeState(player.idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}

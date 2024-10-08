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

        stateTimer = 6.5f;
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0) UI_Manager.instance.restartButton.SetActive(true);
        if (stateTimer < 1.5f) UI_Manager.instance.darkScreen.GetComponent<Animator>().Play("fadeIn");
    }

    public override void Exit()
    {
        base.Exit();
    }
}

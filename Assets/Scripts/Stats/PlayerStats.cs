using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    Player player;

    protected override void Start()
    {
        base.Start();

        player = GetComponent<Player>();
    }

    public override void TakeDamage(Transform _damageFrom, float _Damage, float _fireDamage, float _iceDamage, float _lightningDamage)
    {
        if (!player.CheckForParry(_damageFrom) && player.stateMachine.currentState != player.successfulParry && player.stateMachine.currentState != player.dashState)
            base.TakeDamage(_damageFrom, _Damage, _fireDamage, _iceDamage, _lightningDamage);
    }
}

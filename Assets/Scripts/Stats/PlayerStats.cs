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

    public override void TakeDamage(Transform damageFrom, float _damage)
    {
        player.DamageFX(damageFrom);

        if (!player.CheckForParry(damageFrom))
        {
            currentHealth -= _damage;
            if (currentHealth <= 0) Die();
        }
    }
}

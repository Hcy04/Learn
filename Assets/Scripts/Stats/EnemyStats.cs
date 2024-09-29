using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    Enemy enemy;

    protected override void Start()
    {
        base.Start();

        enemy = GetComponent<Enemy>();
    }

    public override void TakeDamage(Transform _damageFrom, float _Damage, float _fireDamage, float _iceDamage, float _lightningDamage)
    {
        base.TakeDamage(_damageFrom, _Damage, _fireDamage, _iceDamage, _lightningDamage);

        if (currentHealth > 0) enemy.IsBattle();
    }
}

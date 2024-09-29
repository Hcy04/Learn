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

    public override void TakeDamage(Transform damageFrom, float _damage)
    {
        base.TakeDamage(damageFrom, _damage);

        enemy.IsBattle();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    private Enemy enemy;

    [Header("Level Details")]
    [SerializeField] private int level = 1;

    [Range(0f, 1f)]
    [SerializeField] private float percentageModifier = .1f;

    protected override void Start()
    {
        Modify(maxHealth);
        Modify(damage);
        Modify(armor);

        base.Start();

        enemy = GetComponent<Enemy>();
    }

    private void Modify(Stat _stat)
    {
        float modifier = Mathf.Pow(1 + percentageModifier, level - 1);
        _stat.baseValue *= modifier;
    }

    public override void TakeDamage(Transform _damageFrom, float _Damage, float _fireDamage, float _iceDamage, float _lightningDamage)
    {
        base.TakeDamage(_damageFrom, _Damage, _fireDamage, _iceDamage, _lightningDamage);

        if (currentHealth > 0) enemy.IsBattle();
    }
}

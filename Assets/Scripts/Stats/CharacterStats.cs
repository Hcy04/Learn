using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    Character character;

    #region Info
    [Header("Ability Stats")]
    public Stat vitality;
    public Stat strength;
    public Stat agility;
    public Stat intelligence;
    
    [Header("Major Stats")]
    public Stat maxHealth;
    public Stat damage;
    public Stat armor;

    public Stat critChance;
    public Stat critDamage;

    [Header("Elemental Stats")]
    public Stat fireResistance;
    public Stat iceResistance;
    public Stat lightningResistance;

    public Stat fireDamage;
    public Stat iceDamage;
    public Stat lightningDamage;

    [Header("Elemental Effect")]
    public float ignited;//燃烧次数，向上取整
    public float chilled;//减速的比例
    public float shocked;//触电的伤害

    protected float lastIgnitedDamage;//上次伤害时间
    private float chilledTimer = 3;//减速持续时间
    protected float shockedTimer = 2;//延迟时间

    [Header("Current Stats")]
    public float currentHealth;
    public bool Died;
    #endregion

    public System.Action onHealthChanged;

    protected virtual void Start()
    {
        character = GetComponent<Character>();

        currentHealth = maxHealth.GetValue();
    }

    protected virtual void Update()
    {
        if (ignited > 0 && Time.time - lastIgnitedDamage > 1)
        {
            currentHealth -= maxHealth.GetValue() * 0.01f;
            onHealthChanged();

            lastIgnitedDamage = Time.time;
            ignited -= 1;
        }
        if (chilled > 0)
        {Debug.Log(character.gameObject.name + "Chilled" + chilled);
            chilledTimer -= Time.deltaTime;
            if (chilledTimer < 0)
            {
                chilled = 0;
                chilledTimer = 3;
            }
        }
        if (shocked > 0)
        {
            shockedTimer -= Time.deltaTime;
            if (shockedTimer < 0)
            {
                currentHealth -= shocked * 2;
                onHealthChanged();

                shocked = 0;
                shockedTimer = 2;
            }
        }

        if (currentHealth <= 0 && !Died) Die();
    }

    public virtual void DoDamage(CharacterStats _targetStats)
    {
        float finalDamage = damage.GetValue();
        if (Random.Range(0, 1000) < critChance.GetValue() * 1000) finalDamage *= 1 + critDamage.GetValue();

        _targetStats.TakeDamage(transform, finalDamage, fireDamage.GetValue(), iceDamage.GetValue(), lightningDamage.GetValue());
    }

    public virtual void TakeDamage(Transform _damageFrom, float _Damage, float _fireDamage, float _iceDamage, float _lightningDamage)
    {
        character.DamageFX(_damageFrom);//_damageFrom用于计算击退方向

        float finalDamage = _Damage - armor.GetValue();
        if (finalDamage < 1) finalDamage = 1;
        currentHealth -= finalDamage;
        //元素伤害为百分比减伤
        currentHealth -= _fireDamage * (1 - fireResistance.GetValue());
        currentHealth -= _iceDamage * (1 - iceResistance.GetValue());
        currentHealth -= _lightningDamage * (1 - lightningResistance.GetValue());
        //燃烧，抗性影响持续时间
        if (_fireDamage > 0 && (Random.Range(0, 1000) < (1 - fireResistance.GetValue()) * 1000)) ignited = 10 * (1 - fireResistance.GetValue());
        //冰冻，抗性影响减速比例
        if (_iceDamage > 0 && (Random.Range(0, 1000) < (1 - iceResistance.GetValue()) * 1000)) chilled = 0.5f * (1 - iceResistance.GetValue());
        //触电，抗性影响伤害大小
        if (_lightningDamage > 0 && (Random.Range(0, 1000) < (1 - lightningResistance.GetValue()) * 1000)) shocked = _lightningDamage * (1 - lightningResistance.GetValue());

        onHealthChanged();
    }

    protected virtual void Die()
    {
        currentHealth = 0;
        Died = true;
        character.IsDied();
    }
}

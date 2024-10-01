using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    Character character;

    public ParticleSystem ignitedParticle;
    public ParticleSystem chilledParticle;
    public ParticleSystem shockedParticle;

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
    [SerializeField] protected float ignited;//燃烧次数，向上取整
    [SerializeField] protected float lastIgnitedDamage;//上次伤害时间

    [SerializeField] protected float chilled;//减速的比例
    [SerializeField] protected float chilledTimer;//减速持续时间

    [SerializeField] protected float shocked;//累计的真伤
    [SerializeField] protected float shockedTimer;//伤害积累时间

    [Header("Current Stats")]
    public float currentHealth;
    public bool Died;
    #endregion

    public System.Action onHealthChanged;

    protected virtual void Start()
    {
        character = GetComponent<Character>();

        ignitedParticle.Stop();
        chilledParticle.Stop();
        shockedParticle.Stop();

        currentHealth = maxHealth.GetValue();
    }

    protected virtual void Update()
    {
        if (Died) return;

        if (ignited > 0 && Time.time - lastIgnitedDamage > 1)
        {
            currentHealth -= maxHealth.GetValue() * 0.01f;
            onHealthChanged();

            lastIgnitedDamage = Time.time;
            ignited -= 1;

            if (ignited <= 0) ignitedParticle.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }
        if (chilled > 0)
        {
            chilledTimer -= Time.deltaTime;
            if (chilledTimer < 0)
            {
                character.SlowOver();

                chilled = 0;
                chilledParticle.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            }
        }
        if (shocked > 0)
        {
            shockedTimer -= Time.deltaTime;
            if (shockedTimer < 0)
            {
                currentHealth -= shocked;
                onHealthChanged();
                Spawner.instance.CreatThunderStrike(transform.position, transform);

                shocked = 0;
                shockedParticle.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            }
        }

        if (currentHealth <= 0) Die();
    }

    public virtual void DoDamage(CharacterStats _targetStats)
    {
        float finalDamage = damage.GetValue();
        if (Random.Range(0, 1000) < critChance.GetValue() * 1000) finalDamage *= 1 + critDamage.GetValue();

        _targetStats.TakeDamage(transform, finalDamage, fireDamage.GetValue(), iceDamage.GetValue(), lightningDamage.GetValue());
    }

    public virtual void TakeDamage(Transform _damageFrom, float _damage, float _fireDamage, float _iceDamage, float _lightningDamage)
    {
        character.DamageFX(_damageFrom);//_damageFrom用于计算击退方向

        float finalDamage = _damage - armor.GetValue();
        if (finalDamage < 1) finalDamage = 1;
        currentHealth -= finalDamage;
        //元素伤害为百分比减伤
        currentHealth -= _fireDamage * (1 - fireResistance.GetValue());
        currentHealth -= _iceDamage * (1 - iceResistance.GetValue());
        currentHealth -= _lightningDamage * (1 - lightningResistance.GetValue());
        //燃烧，抗性影响持续时间
        if (_fireDamage > 0 && (Random.Range(0, 1000) < (1 - fireResistance.GetValue()) * 1000))
        {
            ignitedParticle.Play();
            ignited = 10 * (1 - fireResistance.GetValue());
        }
        //冰冻，抗性影响减速比例和时间
        if (_iceDamage > 0 && (Random.Range(0, 1000) < (1 - iceResistance.GetValue()) * 1000))
        {
            chilledParticle.Play();
            chilled = 0.5f * (1 - iceResistance.GetValue() * .6f);//最多减少到0.2
            chilledTimer = 5 * (1 - iceResistance.GetValue() * .6f);//最多减少到2

            character.SlowBy(chilled);
        }
        //触电，抗性影响真伤积累时间
        if (_lightningDamage > 0 && (Random.Range(0, 1000) < (1 - lightningResistance.GetValue()) * 1000))
        {
            shockedParticle.Play();
            shocked += (_lightningDamage + _damage) * .5f;
            if(shockedTimer <= 0) shockedTimer = 10 * (1 - lightningResistance.GetValue() * .8f);//最多减少到两秒
        }

        onHealthChanged();
    }

    protected virtual void Die()
    {
        ignitedParticle.Stop();
        chilledParticle.Stop();
        shockedParticle.Stop();

        currentHealth = 0;
        Died = true;
        character.IsDied();
    }
}

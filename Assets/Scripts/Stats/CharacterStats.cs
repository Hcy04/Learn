using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuffState
{
    Start,
    Duration,
    End
}

public class CharacterStats : MonoBehaviour
{
    public Character character;
    [SerializeField] private List<Buff> buffList;

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
    public Stat fireDamage;
    public Stat iceDamage;
    public Stat lightningDamage;

    public Stat fireResistance;
    public Stat iceResistance;
    public Stat lightningResistance;

    [Header("Elemental Effect")]
    [SerializeField] protected Ignited ignited;
    [SerializeField] protected Chilled chilled;
    [SerializeField] protected Shocked shocked;

    [Header("Current Stats")]
    public float currentHealth;
    public bool Died;
    #endregion

    public System.Action onHealthChanged;

    protected virtual void Start()
    {
        character = GetComponent<Character>();
        buffList = new List<Buff>();

        ignitedParticle.Stop();
        chilledParticle.Stop();
        shockedParticle.Stop();

        currentHealth = maxHealth.GetValue();
    }

    protected virtual void Update()
    {
        if (Died) return;

        for (int i = buffList.Count - 1; i >= 0; i--)
        {
            buffList[i].time -= Time.deltaTime;

            if (buffList[i].time < 0)
            {
                buffList[i].Effect(this, BuffState.End);
                buffList.Remove(buffList[i]);
            }
            else buffList[i].Effect(this, BuffState.Duration);
        }
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

        float damage = 0;
        if (_damage != 0)
        {
            damage = _damage - armor.GetValue();
            if (damage < 1) damage = 1;
        }

        float fireDamage = _fireDamage * (1 - fireResistance.GetValue());
        float iceDamage = _iceDamage * (1 - iceResistance.GetValue());
        float lightningDamage = _lightningDamage * (1 - lightningResistance.GetValue());

        ChangeHealth(-(damage + fireDamage + iceDamage + lightningDamage));

        if (Died) return;
        //燃烧，抗性影响持续时间
        if (_fireDamage > 0 && (Random.Range(0, 1000) < (1 - fireResistance.GetValue()) * 1000))
        {
            ignited.Setup(10 * (1 - fireResistance.GetValue()));
            AddBuff(ignited);
        }

        //冰冻，抗性影响减速强度和时间,强度0.2-0.5，时间2-5
        if (_iceDamage > 0 && (Random.Range(0, 1000) < (1 - iceResistance.GetValue()) * 1000))
        {
            chilled.Setup(5 * (1 - iceResistance.GetValue() * .6f), 0.5f * (1 - iceResistance.GetValue() * .6f));
            AddBuff(chilled);
        }

        //触电，抗性影响真伤积累时间,时间2-10
        if (_lightningDamage > 0 && (Random.Range(0, 1000) < (1 - lightningResistance.GetValue()) * 1000))
        {
            Shocked target = (Shocked)buffList.Find(Buff => Buff.buffName == shocked.buffName);
            if (target == null)
            {
                shocked.SetUp(10 * (1 - lightningResistance.GetValue() * .8f), _damage + _lightningDamage);
                AddBuff(shocked);
            }
            else target.AddDamege(_damage + _lightningDamage);
        }
    }

    public virtual void ChangeHealth(float _value)
    {
        currentHealth += _value;
        if (currentHealth > maxHealth.GetValue()) currentHealth = maxHealth.GetValue();
        else if (currentHealth <= 0) Die();
        onHealthChanged();
    }

    protected virtual void Die()
    {
        ignitedParticle.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        chilledParticle.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        shockedParticle.Stop(true, ParticleSystemStopBehavior.StopEmitting);

        currentHealth = 0;
        Died = true;
        character.IsDied();
    }

    public void AddBuff(Buff _buff)
    {
        Buff target = buffList.Find(Buff => Buff.buffName == _buff.buffName);

        if (target != null)
        {
            if (target.time < _buff.time) target.time = _buff.time;
        }
        else
        {
            Buff buff;
            if (_buff.buffType == BuffType.ChangeStats) buff = Factory.CopyInstance((ChangeStat)_buff);
            else if (_buff.buffType == BuffType.InstantHealth) buff = Factory.CopyInstance((InstantHealth)_buff);
            else if (_buff.buffType == BuffType.Ignited) buff = Factory.CopyInstance((Ignited)_buff);
            else if (_buff.buffType == BuffType.Chilled) buff = Factory.CopyInstance((Chilled)_buff);
            else if (_buff.buffType == BuffType.Shocked) buff = Factory.CopyInstance((Shocked)_buff);
            else
            {
                Debug.LogError("Not Found BuffType");
                return;
            }

            buffList.Add(buff);
            buff.Effect(this, BuffState.Start);
        }
    }
}

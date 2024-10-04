using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Buff", menuName = "Data/Buff/Elemental/Ignited")]

public class Ignited : Buff
{
    [Header("Ignited")]
    [SerializeField] private float damagePercentage = 0.01f;
    [SerializeField] private float damageInterval = 1;
    [SerializeField] private float lastDamageTime = 0;

    public void Setup(float _time)
    {
        time = _time;
    }

    protected override void StartEffect(CharacterStats stats)
    {
        stats.ignitedParticle.Play();
    }

    protected override void DurationEffect(CharacterStats stats)
    {
        if (Time.time - lastDamageTime >= damageInterval)
        {
            stats.ChangeHealth(-stats.maxHealth.GetValue() * damagePercentage);
            lastDamageTime = Time.time;
        }
    }

    protected override void EndEffect(CharacterStats stats)
    {
        stats.ignitedParticle.Stop(true, ParticleSystemStopBehavior.StopEmitting);
    }
}

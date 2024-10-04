using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Buff", menuName = "Data/Buff/Elemental/Shocked")]

public class Shocked : Buff
{
    [Header("Shocked")]
    [SerializeField] private float strength = .5f;
    [SerializeField] private float finalDamage;

    public void SetUp(float _time, float _damage)
    {
        time = _time;
        finalDamage = _damage;
    }

    public void AddDamege(float _damage)
    {
        finalDamage += _damage;
    }

    protected override void StartEffect(CharacterStats stats)
    {
        stats.shockedParticle.Play();
    }

    protected override void EndEffect(CharacterStats stats)
    {
        stats.ChangeHealth(-finalDamage * strength);

        stats.shockedParticle.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        Spawner.instance.CreatThunderStrike(stats.transform.position, stats.transform);
    }
}

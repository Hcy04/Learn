using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Buff", menuName = "Data/Buff/Elemental/Chilled")]

public class Chilled : Buff
{
    [Header("Chilled")]
    [SerializeField] private float strenth;

    public void Setup(float _time, float _strenth)
    {
        time = _time;
        strenth = _strenth;
    }

    protected override void StartEffect(CharacterStats stats)
    {
        stats.chilledParticle.Play();

        stats.character.SlowBy(strenth);
    }

    protected override void EndEffect(CharacterStats stats)
    {
        stats.chilledParticle.Stop(true, ParticleSystemStopBehavior.StopEmitting);

        stats.character.SlowOver();
    }
}

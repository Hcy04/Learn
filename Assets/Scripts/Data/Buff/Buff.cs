using UnityEngine;

public enum BuffType
{
    ChangeStats,
    InstantHealth,

    Ignited,
    Chilled,
    Shocked
}

[System.Serializable]

public class Buff : ScriptableObject
{
    [Header("Buff")]
    public BuffType buffType;
    public string buffName;
    public float time;

    public void Effect(CharacterStats stats, BuffState buffState)
    {
        if (buffState == BuffState.Start) StartEffect(stats);
        else if (buffState == BuffState.Duration) DurationEffect(stats);
        else if (buffState == BuffState.End) EndEffect(stats);
    }

    protected virtual void StartEffect(CharacterStats stats)
    {

    }

    protected virtual void DurationEffect(CharacterStats stats)
    {

    }

    protected virtual void EndEffect(CharacterStats stats)
    {

    }
}

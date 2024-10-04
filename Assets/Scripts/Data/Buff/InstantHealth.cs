using UnityEngine;

[CreateAssetMenu(fileName = "New Buff", menuName = "Data/Buff/Instant Health")]

public class InstantHealth : Buff
{
    [Header("InstantHealth")]
    public float value;

    protected override void StartEffect(CharacterStats stats)
    {
        stats.ChangeHealth(value);
    }
}

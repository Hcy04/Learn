using UnityEngine;

public enum StatType
{
    vitality,
    strength,
    agility,
    intelligence,

    maxHealth,
    damage,
    armor,

    critChance,
    critDamage,

    fireResistance,
    iceResistance,
    lightningResistance,

    fireDamage,
    iceDamage,
    lightningDamage
}

[CreateAssetMenu(fileName = "New Buff", menuName = "Data/Buff/Change Stat")]

public class ChangeStat : Buff
{
    [Header("ChangeStat")]
    public StatType statType;
    public float value;

    protected override void StartEffect(CharacterStats stats)
    {
        if (statType == StatType.vitality) stats.vitality.AddModifier(value);
        else if (statType == StatType.strength) stats.strength.AddModifier(value);
        else if (statType == StatType.agility) stats.agility.AddModifier(value);
        else if (statType == StatType.intelligence) stats.intelligence.AddModifier(value);

        else if (statType == StatType.maxHealth) stats.maxHealth.AddModifier(value);
        else if (statType == StatType.damage) stats.damage.AddModifier(value);
        else if (statType == StatType.armor) stats.armor.AddModifier(value);

        else if (statType == StatType.critChance) stats.critChance.AddModifier(value);
        else if (statType == StatType.critDamage) stats.critDamage.AddModifier(value);

        else if (statType == StatType.fireResistance) stats.fireResistance.AddModifier(value);
        else if (statType == StatType.iceResistance) stats.iceResistance.AddModifier(value);
        else if (statType == StatType.lightningResistance) stats.lightningResistance.AddModifier(value);

        else if (statType == StatType.fireDamage) stats.fireDamage.AddModifier(value);
        else if (statType == StatType.iceDamage) stats.iceDamage.AddModifier(value);
        else if (statType == StatType.lightningDamage) stats.lightningDamage.AddModifier(value);
    }

    protected override void EndEffect(CharacterStats stats)
    {
        if (statType == StatType.vitality) stats.vitality.RemoveModifier(value);
        else if (statType == StatType.strength) stats.strength.RemoveModifier(value);
        else if (statType == StatType.agility) stats.agility.RemoveModifier(value);
        else if (statType == StatType.intelligence) stats.intelligence.RemoveModifier(value);

        else if (statType == StatType.maxHealth) stats.maxHealth.RemoveModifier(value);
        else if (statType == StatType.damage) stats.damage.RemoveModifier(value);
        else if (statType == StatType.armor) stats.armor.RemoveModifier(value);

        else if (statType == StatType.critChance) stats.critChance.RemoveModifier(value);
        else if (statType == StatType.critDamage) stats.critDamage.RemoveModifier(value);

        else if (statType == StatType.fireResistance) stats.fireResistance.RemoveModifier(value);
        else if (statType == StatType.iceResistance) stats.iceResistance.RemoveModifier(value);
        else if (statType == StatType.lightningResistance) stats.lightningResistance.RemoveModifier(value);

        else if (statType == StatType.fireDamage) stats.fireDamage.RemoveModifier(value);
        else if (statType == StatType.iceDamage) stats.iceDamage.RemoveModifier(value);
        else if (statType == StatType.lightningDamage) stats.lightningDamage.RemoveModifier(value);
    }
}

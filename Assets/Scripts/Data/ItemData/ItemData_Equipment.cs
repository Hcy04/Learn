using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public enum EquipmentType
{
    Weapon,
    Armor,
    Amulet,
    Magic
}

[CreateAssetMenu(fileName = "New Item Data", menuName = "Data/Equipment")]

public class ItemData_Equipment : ItemData
{
    public EquipmentType equipmentType;
    public ItemEffect[] effect;

    [Header("Ability Modifiers")]
    public float vitality;
    public float strength;
    public float agility;
    public float intelligence;
    
    [Header("Major Modifiers")]
    public float maxHealth;
    public float damage;
    public float armor;

    public float critChance;
    public float critDamage;

    [Header("Elemental Modifiers")]
    public float fireDamage;
    public float iceDamage;
    public float lightningDamage;

    public float fireResistance;
    public float iceResistance;
    public float lightningResistance;

    [Header("Craft Requirements")]
    public List<InventoryItem> craftingMaterials;

    public void AddModifiers()
    {
        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();

        if (vitality != 0) playerStats.vitality.AddModifier(vitality);
        if (strength != 0) playerStats.strength.AddModifier(strength);
        if (agility != 0) playerStats.agility.AddModifier(agility);
        if (intelligence != 0) playerStats.intelligence.AddModifier(intelligence);

        if (maxHealth != 0)
        {
            playerStats.AddModifierToMaxHealth(maxHealth);
            playerStats.onHealthChanged();
        }
        if (damage != 0) playerStats.damage.AddModifier(damage);
        if (armor != 0) playerStats.armor.AddModifier(armor);

        if (critChance != 0) playerStats.critChance.AddModifier(critChance);
        if (critDamage != 0) playerStats.critDamage.AddModifier(critDamage);

        if (fireResistance != 0) playerStats.fireResistance.AddModifier(fireResistance);
        if (iceResistance != 0) playerStats.iceResistance.AddModifier(iceResistance);
        if (lightningResistance != 0) playerStats.lightningResistance.AddModifier(lightningResistance);

        if (fireDamage != 0) playerStats.fireDamage.AddModifier(fireDamage);
        if (iceDamage != 0) playerStats.iceDamage.AddModifier(iceDamage);
        if (lightningDamage != 0) playerStats.lightningDamage.AddModifier(lightningDamage);
    }

    public void RemoveModifiers()
    {
        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();

        if (vitality != 0) playerStats.vitality.RemoveModifier(vitality);
        if (strength != 0) playerStats.strength.RemoveModifier(strength);
        if (agility != 0) playerStats.agility.RemoveModifier(agility);
        if (intelligence != 0) playerStats.intelligence.RemoveModifier(intelligence);

        if (maxHealth != 0)
        {
            playerStats.RemoveModifierToMaxHealth(maxHealth);
            playerStats.onHealthChanged();
        }
        if (damage != 0) playerStats.damage.RemoveModifier(damage);
        if (armor != 0) playerStats.armor.RemoveModifier(armor);

        if (critChance != 0) playerStats.critChance.RemoveModifier(critChance);
        if (critDamage != 0) playerStats.critDamage.RemoveModifier(critDamage);

        if (fireResistance != 0) playerStats.fireResistance.RemoveModifier(fireResistance);
        if (iceResistance != 0) playerStats.iceResistance.RemoveModifier(iceResistance);
        if (lightningResistance != 0) playerStats.lightningResistance.RemoveModifier(lightningResistance);

        if (fireDamage != 0) playerStats.fireDamage.RemoveModifier(fireDamage);
        if (iceDamage != 0) playerStats.iceDamage.RemoveModifier(iceDamage);
        if (lightningDamage != 0) playerStats.lightningDamage.RemoveModifier(lightningDamage);
    }

    public override string GetDescription()
    {
        sb.Length = 0;

        AddItemDescription(vitality, Factory.Translation("vitality"));
        AddItemDescription(strength, Factory.Translation("strength"));
        AddItemDescription(agility, Factory.Translation("agility"));
        AddItemDescription(intelligence, Factory.Translation("intelligence"));

        AddItemDescription(maxHealth, Factory.Translation("maxHealth"));
        AddItemDescription(damage, Factory.Translation("damage"));
        AddItemDescription(armor, Factory.Translation("armor"));

        AddItemDescription(critChance, Factory.Translation("critChance"));
        AddItemDescription(critDamage, Factory.Translation("critDamage"));

        AddItemDescription(fireDamage, Factory.Translation("fireDamage"));
        AddItemDescription(iceDamage, Factory.Translation("iceDamage"));
        AddItemDescription(lightningDamage, Factory.Translation("lightningDamage"));

        AddItemDescription(fireResistance * 100, Factory.Translation("fireResistance"));
        AddItemDescription(iceResistance * 100, Factory.Translation("iceResistance"));
        AddItemDescription(lightningResistance * 100, Factory.Translation("lightningResistance"));

        return sb.ToString();
    }

    private void AddItemDescription(float _value, string _name)
    {
        if (_value != 0)
        {
            if (sb.Length > 0) sb.AppendLine();
            if (_value > 0) sb.Append(_name + ":" + _value);
        }
    }
}

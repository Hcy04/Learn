using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Effect", menuName = "Data/Item Effect/Heal Effect")]

public class HealEffect : ItemEffect
{
    public float healAmount;

    public override void ExecuteEffect()
    {
        PlayerManager.instance.player.stats.ChangeHealth(healAmount);
    }
}

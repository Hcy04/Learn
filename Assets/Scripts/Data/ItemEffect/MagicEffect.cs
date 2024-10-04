using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MagicType
{
    Ice_Fire,
    ColdBeam
}

[CreateAssetMenu(fileName = "New Item Effect", menuName = "Data/Item Effect/Magic")]

public class MagicEffect : ItemEffect
{
    [SerializeField] private MagicType magicType;

    public override void ExecuteEffect()
    {
        if (magicType == MagicType.Ice_Fire) Spawner.instance.CreatIce_Fire(PlayerManager.instance.player.transform.position);
        else if (magicType == MagicType.ColdBeam) Spawner.instance.CreatColdBeam(PlayerManager.instance.player.transform.position);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColdBeam : Magic
{
    protected override void Start()
    {
        base.Start();

        fireDamage = 0;
        iceDamage = player.stats.intelligence.GetValue();
        lightningDamage = player.stats.intelligence.GetValue();
    }

    protected override void Update()
    {
        base.Update();
        
        if (timer < 0 || isGrounded) DestroySelf();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice_Fire : Magic
{
    protected override void Start()
    {
        base.Start();

        fireDamage = player.stats.intelligence.GetValue();
        iceDamage = player.stats.intelligence.GetValue();
        lightningDamage = 0;
    }

    protected override void Update()
    {
        base.Update();
        
        if (timer < 0 || isGrounded) DestroySelf();
    }
}

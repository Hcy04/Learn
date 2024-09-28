using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : Projectile
{
    public float crystalTimer;
    [HideInInspector] public bool triggerCalled;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        if (crystalTimer >= 0) crystalTimer -= Time.deltaTime;
        else anim.SetTrigger("Destroy");

        if (triggerCalled) Destroy(this.gameObject);
    }

    public void SetUpCrystal(float _crystalDuration)
    {
        crystalTimer = _crystalDuration;
    }
}

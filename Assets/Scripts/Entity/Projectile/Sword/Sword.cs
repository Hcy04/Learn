using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Projectile
{
    private CircleCollider2D cd;

    protected override void Awake()
    {
        base.Awake();
        
        cd = GetComponent<CircleCollider2D>();
    }

    public void SetUpSword(Vector2 _dir, float _gravityScale)
    {
        rb.velocity = _dir;
        rb.gravityScale = _gravityScale;
    }
}

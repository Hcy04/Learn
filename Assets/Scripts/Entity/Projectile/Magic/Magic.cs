using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Magic : Projectile
{
    protected Player player;

    [SerializeField] protected SpriteRenderer sr;

    [SerializeField] protected float speed;
    [SerializeField] protected float duration;
    protected float timer;
    [SerializeField] protected float colorSpeed;

    protected float fireDamage;
    protected float iceDamage;
    protected float lightningDamage;

    protected bool isGrounded;

    protected override void Start()
    {
        base.Start();

        player = PlayerManager.instance.player;

        if (player.facingDir == -1) transform.Rotate(0, 180, 0);

        if (player.facingDir == 1) rb.velocity = new Vector3(speed, 0);
        else rb.velocity = new Vector3(-speed, 0);

        timer = duration;
    }

    protected override void Update()
    {
        base.Update();

        timer -= Time.deltaTime;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 12)
        {
            collision.GetComponent<EnemyStats>().TakeDamage(transform, 0, fireDamage, iceDamage, lightningDamage);
        }
        else if (collision.gameObject.layer == 10) isGrounded = true;
    }

    protected virtual void DestroySelf()
    {
        sr.color = new Color(1, 1, 1, sr.color.a - Time.deltaTime * colorSpeed);
        if (sr.color.a <= 0) Destroy(gameObject);
    }
}

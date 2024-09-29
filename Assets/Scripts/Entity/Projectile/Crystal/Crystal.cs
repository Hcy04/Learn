using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : Projectile
{
    private bool addDuration;
    private bool canExplode;
    private bool canMoving;

    private Collider2D cd;

    public float crystalTimer;
    public bool triggerCalled;
    public float attackCheckRadius;
    private float damage;

    private float moveSpeed;
    private Transform clocestTarget;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
        
        cd = GetComponent<Collider2D>();
        cd.enabled = false;
    }

    protected override void Update()
    {
        base.Update();

        if (crystalTimer >= 0) 
        {
            crystalTimer -= Time.deltaTime;

            if (canMoving)
            {
                FindTarget();
                if (clocestTarget != null)
                {
                    rb.velocity = (clocestTarget.position - transform.position).normalized * moveSpeed;
                    if (Vector2.Distance(transform.position, clocestTarget.position) < .5f) DestroyCrystal();
                }
            }
        }
        else
        {
            if (canExplode) DestroyCrystal();
            else Destroy(this.gameObject);
        }

        if (triggerCalled) Destroy(this.gameObject);
    }

    public void SetUpCrystal(float _damage, float _attackCheckRadius, float _moveSpeed, bool _addDuration, bool _canExplode, bool _canMoving)
    {
        damage = _damage;
        attackCheckRadius = _attackCheckRadius;
        moveSpeed = _moveSpeed;

        addDuration = _addDuration;
        canExplode = _canExplode;
        canMoving = _canMoving;

        if (!addDuration) crystalTimer = 2;
        else crystalTimer = 4;
    }

    public void DestroyCrystal()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        cd.enabled = true;
        anim.SetBool("Destroy", true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 12)
        {
            collision.GetComponent<EnemyStats>().TakeDamage(transform, damage, 0, 0, 0);
        }
    }

    private void FindTarget()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 15);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                if (clocestTarget == null ||
                    (Vector2.Distance(transform.position, clocestTarget.position) > Vector2.Distance(transform.position, hit.transform.position)))
                    clocestTarget = hit.GetComponent<Enemy>().transform;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : Projectile
{
    private Collider2D cd;

    public float crystalTimer;
    public bool triggerCalled;
    public float attackCheckRadius;

    private bool canMoving;
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
                    if (Vector2.Distance(transform.position, clocestTarget.position) < 1) DestroyCrystal();
                }
            }
        }
        else DestroyCrystal();

        if (triggerCalled) Destroy(this.gameObject);
    }

    public void SetUpCrystal(float _crystalDuration, float _attackCheckRadius, bool _canMoving, float _moveSpeed)
    {
        crystalTimer = _crystalDuration;
        attackCheckRadius = _attackCheckRadius;
        canMoving = _canMoving;
        moveSpeed = _moveSpeed;
    }

    public void DestroyCrystal()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        cd.enabled = true;
        anim.SetBool("Destroy", true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
        {
            collision.GetComponent<Enemy>().Damage(transform);
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

using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShortcutManagement;
using UnityEngine;

public class Clone : Projectile
{
    private SpriteRenderer sr;

    private float colorloosingSpeed;
    [HideInInspector] public bool triggerCalled;

    public Transform attackCheck;
    public float attackCheckRadius;

    private int attackDir = 1;
    [HideInInspector] public float moveSpeed;
    
    protected override void Awake()
    {
        base.Awake();

        sr = GetComponentInChildren<SpriteRenderer>();
    }

    protected override void Start()
    {
        base.Start();

        triggerCalled = false;
    }

    protected override void Update()
    {
        base.Update();

        rb.velocity = new Vector2(moveSpeed * attackDir, 0);

        if (triggerCalled)
        {
            sr.color = new Color(1, 1, 1, sr.color.a - (Time.deltaTime * colorloosingSpeed));

            if (sr.color.a <= 0) Destroy(this.gameObject);
        }
    }

    public void SetupClone(Transform _newTransform, float _colorloosingSpeed)
    {
        transform.position = _newTransform.position;
        colorloosingSpeed = _colorloosingSpeed;

        if (true) 
        {
            FaceClosestTarget();

            if (!PlayerManager.instance.player.IsGroundDetected()) anim.SetInteger("AttackNumber", 2);
            else 
            {
                anim.SetInteger("AttackNumber", PlayerManager.instance.player.comboCounter + 1);
                
                PlayerManager.instance.player.lastTimeAttacked = Time.time;
                PlayerManager.instance.player.comboCounter++;
            }
        }
    }

    private Transform clocestTarget;

    private void FaceClosestTarget()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 10);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                if (clocestTarget == null ||
                    (Vector2.Distance(transform.position, clocestTarget.position) > Vector2.Distance(transform.position, hit.transform.position)))
                    clocestTarget = hit.GetComponent<Enemy>().transform;
            }
        }
        
        if (clocestTarget != null && transform.position.x > clocestTarget.position.x)
        {
            attackDir *= -1;
            transform.Rotate(0, 180, 0);
        }
    }
}

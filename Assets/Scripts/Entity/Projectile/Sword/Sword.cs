using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class Sword : Projectile
{
    private CircleCollider2D cd;

    private bool isReturning;
    private float returnSpeed;

    [Header("Bouncing")]
    public bool isBouncing;
    public int amountOfBounce = 4;
    public Transform enemyTarget;

    Player player;

    protected override void Awake()
    {
        base.Awake();
        
        cd = GetComponent<CircleCollider2D>();
    }

    protected override void Start()
    {
        base.Start();
        
        player = PlayerManager.instance.player;
    }

    protected override void Update()
    {
        base.Update();

        if (rb.velocity != Vector2.zero) transform.right = rb.velocity;
        if (transform.parent && Input.GetKeyDown(KeyCode.Mouse1)) ReturnSword();

        if (isBouncing && enemyTarget != null)
        {
            rb.velocity = (enemyTarget.position - transform.position).normalized * returnSpeed;
        }
        else if (isReturning)
        {
            rb.velocity = (player.transform.position - transform.position).normalized * returnSpeed;
            if (Vector2.Distance(transform.position, player.transform.position) < 1)
            {
                player.stateMachine.ChangeState(player.catchSword);
                Destroy(player.sword);
            }
        }
    }

    public void SetUpSword(Vector2 _dir, float _gravityScale, float _returnSpeed)
    {
        rb.velocity = _dir;
        rb.gravityScale = _gravityScale;
        returnSpeed = _returnSpeed;

        anim.SetBool("Rotation", true);
    }

    private void StuckInto(Collider2D collision)
    {
        cd.enabled = false;
        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        anim.SetBool("Rotation", false);
        transform.parent = collision.transform;
    }

    public void ReturnSword()
    {
        isReturning = true;

        cd.enabled = true;
        rb.isKinematic = false;
        rb.constraints = RigidbodyConstraints2D.None;
        
        anim.SetBool("Rotation", true);
        transform.parent = null;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isReturning)
        {
            if (collision.GetComponent<Enemy>() != null) collision.GetComponent<Enemy>().Damage(transform);
        }
        else
        {
            if (collision.GetComponent<Enemy>() != null)
            {
                collision.GetComponent<Enemy>().Damage(transform);
                if (isBouncing) SwordBouncing();
                else StuckInto(collision);
            }
            else StuckInto(collision);
        }
    }

    private void SwordBouncing()
    {
        amountOfBounce--;
        if (amountOfBounce <= 0) 
        {
            isBouncing = false;
            return;
        }

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 10);
        List<(float distance, Transform transform)> distances = new List<(float, Transform)>();

        foreach (var collider in colliders)  
        {
            if (collider.GetComponent<Enemy>() != null)
            {
                float distance = Vector3.Distance(transform.position, collider.transform.position);  
                distances.Add((distance, collider.transform));
            }
        }

        distances.Sort((a, b) => a.distance.CompareTo(b.distance));
        //敌人距离过近无法弹射，Sword Collider 与多个 Enemy Collider 相交
        if (distances.Count <= 1 || Mathf.Abs(distances[0].distance - distances[1].distance) < 1) isBouncing = false;
        else enemyTarget = distances[1].transform;
    }
}

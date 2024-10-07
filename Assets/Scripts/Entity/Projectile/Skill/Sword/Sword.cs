using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class Sword : ProjectileSkill
{
    private CircleCollider2D cd;

    private bool canRotate = true;
    private bool isReturning;
    private float returnSpeed;

    private bool canDamegeReturn;

    [Header("Bouncing")]
    public bool isBouncing;
    public int amountOfBounce = 4;
    public Transform enemyTarget;

    [Header("Pierce")]
    public bool isPierce;
    public float addSpeed = 1.5f;

    [Header("Spin")]
    public bool isSpin;
    public int amountOfSpin = 5;

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

        if (canRotate) transform.right = rb.velocity;
        if ((transform.parent || rb.isKinematic) && Input.GetKeyDown(KeyCode.Mouse1) || amountOfSpin <= 0
            || Vector2.Distance(transform.position, player.transform.position) > 30)
            ReturnSword();

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
                UI_Manager.instance.inGame.UpdateSwordImage(true);
            }
        }
    }

    public void SetUpSword(Vector2 _dir, float _gravityScale, float _returnSpeed, bool _canDamageReturn, Sword_Skill.SwordSkillMode skillMode)
    {
        rb.velocity = _dir;
        rb.gravityScale = _gravityScale;
        returnSpeed = _returnSpeed;

        canDamegeReturn = _canDamageReturn;

        if (skillMode == Sword_Skill.SwordSkillMode.Bouncing) isBouncing = true;
        else if (skillMode == Sword_Skill.SwordSkillMode.Pierce) isPierce = true;
        else if (skillMode == Sword_Skill.SwordSkillMode.Spin) isSpin = true;

        if (!isPierce) anim.SetBool("Rotation", true);
    }

    private void StuckInto(Collider2D collision)
    {
        canRotate = false;

        cd.enabled = false;
        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        anim.SetBool("Rotation", false);
        transform.parent = collision.transform;
    }

    public void ReturnSword()
    {
        isReturning = true;
        canRotate = true;

        cd.enabled = true;
        rb.isKinematic = false;
        rb.constraints = RigidbodyConstraints2D.None;
        
        anim.SetBool("Rotation", true);
        transform.parent = null;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        float damage = player.stats.damage.baseValue;

        if (isReturning)
        {
            if (collision.gameObject.layer == 12 && canDamegeReturn)
                collision.GetComponent<EnemyStats>().TakeDamage(transform, damage / 2, 0, 0, 0);
        }
        else
        {
            if (collision.gameObject.layer == 12)
            {
                if (isBouncing) 
                {
                    damage *= 1;
                    SwordBouncing();
                }
                else if (isPierce)
                {
                    damage *= 3;
                    SwordPierce(collision);
                }
                else if (isSpin) 
                {
                    damage *= .5f;
                    SwordSpin();
                }
                else
                {
                    damage *= 2f;
                    StuckInto(collision);
                }

                collision.GetComponent<EnemyStats>().TakeDamage(transform, damage, 0, 0, 2);
            }
            else if (collision.gameObject.layer == 10) StuckInto(collision);
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

    private void SwordPierce(Collider2D collision)
    { 
        rb.velocity = new Vector2(rb.velocity.x * addSpeed, 0);
    }

    private void SwordSpin()
    {
        amountOfSpin--;

        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }
}

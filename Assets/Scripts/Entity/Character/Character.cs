using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Entity
{
    #region Components
    [HideInInspector] public Rigidbody2D rb;
    public EntityFX fx { get; private set;}
    #endregion

    [Header("Knockback Info")]
    [SerializeField] protected float knockbackDuration;
    [SerializeField] protected Vector2 knockbackDirection;
    protected bool isKnocked;

    [Header("Collision Info")]
    public Transform attackCheck;
    public float attackCheckRadius;
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected LayerMask whatIsGround;

    public int facingDir { get; private set; } = 1;
    protected bool facingRight = true;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();

        rb = GetComponent<Rigidbody2D>();
        fx = GetComponent<EntityFX>();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected Transform damageFromPosition;

    public virtual void Damage(Transform damageFrom)
    {
        damageFromPosition = damageFrom;

        fx.StartCoroutine("FlashFX");
        StartCoroutine("HitKnockback");
    }
    
    protected virtual IEnumerator HitKnockback()
    {
        float moveDir =  (transform.position.x - damageFromPosition.position.x)
            / Mathf.Abs(transform.position.x - damageFromPosition.position.x);

        isKnocked = true;
        rb.velocity = new Vector2(knockbackDirection.x * moveDir, knockbackDirection.y);
        yield return new WaitForSeconds(knockbackDuration);
        isKnocked = false;
    }

    #region Velocity
    public virtual void SetVelocity(float _xVelocity, float _yVelocity)
    {
        if (isKnocked) return;

        rb.velocity = new Vector2(_xVelocity, _yVelocity);
        FlipController(_xVelocity);
    }

    public virtual void FlipController(float _x)
    {
        if ((_x > 0 && !facingRight) || (_x < 0 && facingRight)) Flip();
    }

    public virtual void Flip()
    {
        facingDir *= -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }
    #endregion

    #region Collision
    public virtual  bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
    public virtual bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround);

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance * facingDir, wallCheck.position.y));
        Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);
    }
    #endregion
}

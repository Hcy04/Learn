using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Character : Entity
{
    #region Components
    [HideInInspector] public EntityFX fx { get; private set; }
    
    [HideInInspector] public Collider2D cd;
    [HideInInspector] public SpriteRenderer sr;
    #endregion

    #region Info
    [Header("Character")]
    
    [Header("Speed Info")]
    [SerializeField] protected float defaultMoveSpeed;
    public float moveSpeed;
    [SerializeField] protected float defaultJumpForce;
    public float jumpForce;
    [SerializeField] protected float defaultDashSpeed;
    public float dashSpeed;

    [Header("Knockback Info")]
    [SerializeField] protected float knockbackDuration;
    [SerializeField] protected float knockbackSpeed;
    [HideInInspector] public bool isKnocked;

    [Header("Collision Info")]
    public Transform attackCheck;
    public float attackCheckRadius;
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected Transform ceilingCheck;
    [SerializeField] protected float ceilingCheckDistance;
    [SerializeField] protected LayerMask whatIsGround;

    [HideInInspector] public int facingDir { get; private set; } = 1;
    protected bool facingRight = true;
    #endregion

    public System.Action onFlipped;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();

        fx = GetComponent<EntityFX>();
        
        cd = GetComponent<Collider2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
        
        moveSpeed = defaultMoveSpeed;
        jumpForce = defaultJumpForce;
        dashSpeed = defaultDashSpeed;
    }

    protected override void Update()
    {
        base.Update();
    }

    public virtual void SlowBy(float _slowPercentage)
    {
        anim.speed = 1 - _slowPercentage;
        moveSpeed = defaultMoveSpeed * (1 - _slowPercentage);
        jumpForce = defaultJumpForce * (1 - _slowPercentage);
        dashSpeed = defaultDashSpeed * (1 - _slowPercentage);
    }

    public virtual void SlowOver()
    {
        anim.speed = 1;
        moveSpeed = defaultMoveSpeed;
        jumpForce = defaultJumpForce;
        dashSpeed = defaultDashSpeed;
    }

    public virtual void IsDied()
    {
        rb.velocity = Vector2.zero;
    }

    #region Damage Effect
    public virtual void DamageFX(Transform damageFrom)
    {
        fx.StartCoroutine("FlashFX");
        StartCoroutine("HitKnockback");

        Vector2 knockbackDir = (Vector2) (transform.position - damageFrom.position).normalized * knockbackSpeed;
        rb.velocity = new Vector2(knockbackDir.x, knockbackDir.y * rb.gravityScale / 2);
    }
    
    protected virtual IEnumerator HitKnockback()
    {
        isKnocked = true;
        yield return new WaitForSeconds(knockbackDuration);
        isKnocked = false;
    }
    #endregion

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

        onFlipped?.Invoke();
    }
    #endregion

    #region Collision
    public virtual  bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
    public virtual bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround);
    public virtual bool IsCeilingDetected() => Physics2D.Raycast(ceilingCheck.position, Vector2.up, ceilingCheckDistance, whatIsGround);

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance * facingDir, wallCheck.position.y));
        Gizmos.DrawLine(ceilingCheck.position, new Vector3(ceilingCheck.position.x, ceilingCheckDistance + ceilingCheck.position.y));
        Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);
    }
    #endregion
}

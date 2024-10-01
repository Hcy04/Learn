using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Enemy : Character
{
    public StateMachine<EnemyState> stateMachine { get; private set; }

    public Player player;
    
    [SerializeField] protected Transform playerCheck;
    [SerializeField] protected float detectionDistance;
    [SerializeField] protected LayerMask whatIsPlayer;

    [Header("Stunned Info")]
    public float stunDuration;
    public Vector2 stunDirection;
    [SerializeField] protected Transform attackWarning;
    [SerializeField] protected float attackWarningTime;

    [Header("Attack Info")]
    public float attackDistance;
    public float attackCD;
    public float battleTime;
    [HideInInspector] public float lastTimeAttacked;

    protected bool isFreeze;

    protected override void Awake()
    {
        base.Awake();

        stateMachine = new StateMachine<EnemyState>();
    }

    protected override void Start()
    {
        base.Start();

        player = PlayerManager.instance.player;
    }

    protected override void Update()
    {
        base.Update();

        if (player.skill.freeze.isActive && !isFreeze) FreezeTime(true);
        else if (!player.skill.freeze.isActive && isFreeze) FreezeTime(false);

        if (!isFreeze) stateMachine.currentState.Update();
        else anim.speed = 0;
    }

    public virtual void AnimationFinishTrigger() => stateMachine.currentState.AnimationFinishTrigger();
    public virtual RaycastHit2D IsPlayerDetected() => Physics2D.Raycast(playerCheck.position, Vector2.right * facingDir, detectionDistance, whatIsPlayer);

    public virtual void FreezeTime(bool _timeFrozen)
    {
        if (_timeFrozen)
        {
            isFreeze = true;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else
        {
            isFreeze = false;
            rb.constraints = RigidbodyConstraints2D.None;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            anim.speed = 1;
        }
    }

    public virtual void AttackWarning()
    {
        StartCoroutine("Warning");
    }

    protected virtual IEnumerator Warning()
    {
        attackWarning.gameObject.SetActive(true);
        yield return new WaitForSeconds(attackWarningTime);
        attackWarning.gameObject.SetActive(false);
    }

    public virtual void IsStunned()
    {

    }

    public virtual void IsBattle()
    {
        
    }

    public virtual void DestroySelf()
    {

    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        //Gizmos.color = Color.yellow;
        //Gizmos.DrawLine(playerCheck.position, new Vector3(playerCheck.position.x + detectionDistance * facingDir, playerCheck.position.y));
        //Gizmos.color = Color.red;
        //Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + attackDistance * facingDir, transform.position.y));
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Enemy : Character
{
    public StateMachine<EnemyState> stateMachine { get; private set; }
    
    [SerializeField] protected Transform playerCheck;
    [SerializeField] protected float detectionDistance;
    [SerializeField] protected LayerMask whatIsPlayer;

    [Header("Stunned Info")]
    public float stunDuration;
    public Vector2 stunDirection;
    protected Transform attackWarning;
    [SerializeField] protected float attackWarningTime;

    [Header("Move Info")]
    public float moveSpeed; 
    public float idleTime;

    [Header("Attack Info")]
    public float attackDistance;
    public float attackCD;
    public float battleTime;
    [HideInInspector] public float lastTimeAttacked;

    protected override void Awake()
    {
        base.Awake();

        stateMachine = new StateMachine<EnemyState>();
    }

    protected override void Start()
    {
        base.Start();

        attackWarning = transform.Find("AttackWarning");
    }

    protected override void Update()
    {
        base.Update();

        stateMachine.currentState.Update();
    }

    public virtual void AnimationFinishTrigger() => stateMachine.currentState.AnimationFinishTrigger();
    public virtual RaycastHit2D IsPlayerDetected() => Physics2D.Raycast(playerCheck.position, Vector2.right * facingDir, detectionDistance, whatIsPlayer);

    public virtual void AttackWarning()
    {
        StartCoroutine("Warning");
    }

    private IEnumerator Warning()
    {
        attackWarning.gameObject.SetActive(true);
        yield return new WaitForSeconds(attackWarningTime);
        attackWarning.gameObject.SetActive(false);
    }

    public virtual void IsStunned()
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

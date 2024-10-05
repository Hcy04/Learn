using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Enemy : Character
{
    public new StateMachine<EnemyState> stateMachine { get; private set; }

    #region Component
    [HideInInspector] public Player player;
    [HideInInspector] public EnemyStats stats;
    #endregion
    
    #region Info
    [Header("Enemy")]

    [Header("Drop Info")]
    [SerializeField] private DropList dropList;
    private List<ItemData> drops;

    [Header("Player Check")]
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

    public bool isFreeze;
    public bool dropFlag;
    #endregion

    protected override void Awake()
    {
        base.Awake();

        stateMachine = new StateMachine<EnemyState>();
    }

    protected override void Start()
    {
        base.Start();

        player = PlayerManager.instance.player;
        stats = GetComponent<EnemyStats>();
    }

    protected override void Update()
    {
        base.Update();

        stateMachine.currentState.Update();
    }

    public override void AnimationFinishTrigger() => stateMachine.currentState.AnimationFinishTrigger();

    public virtual RaycastHit2D IsPlayerDetected() => Physics2D.Raycast(playerCheck.position, Vector2.right * facingDir, detectionDistance, whatIsPlayer);

    #region Drop
    public void GenerateDrop()
    {
        drops = new List<ItemData>();

        for (int i = 0; i < dropList.possibleDrop.Length; i++)
        {
            if (dropList.dropPercentage[i] * 1000 > Random.Range(0, 1000)) drops.Add(dropList.possibleDrop[i]);
        }

        foreach (ItemData item in drops) DropItem(item);

        dropFlag = false;
    }

    private void DropItem(ItemData _item)
    {
        GameObject newDrop = Spawner.instance.CreatItem(transform.position);

        newDrop.GetComponent<Item>().SetUpItem(_item, new Vector2(Random.Range(-5, 6), Random.Range(10, 15)));
    }
    #endregion

    #region Freeze
    public virtual void CheckFreeze()
    {
        if (player.skill.freeze.isActive && !isFreeze) FreezeTime(true);
        else if (!player.skill.freeze.isActive && isFreeze) FreezeTime(false);
    }

    protected virtual void FreezeTime(bool _timeFrozen)
    {
        if (_timeFrozen)
        {
            isFreeze = true;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            anim.speed = 0;
        }
        else
        {
            isFreeze = false;
            rb.constraints = RigidbodyConstraints2D.None;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            anim.speed = 1;
        }
    }
    #endregion

    #region Attack Warning
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
    #endregion

    #region Change State
    public virtual void IsStunned()
    {

    }

    public virtual void IsBattle()
    {
        
    }

    public override void IsDied()
    {
        base.IsDied();

        dropFlag = true;
    }
    #endregion

    public virtual void DestroySelf()
    {
        Destroy(this.gameObject);
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

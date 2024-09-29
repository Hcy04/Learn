using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Player : Character
{
    #region States
    public StateMachine<PlayerState> stateMachine { get; private set; }

    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }

    public PlayerJumpState jumpState { get; private set; }
    public PlayerJumpToAirState1 jumpToAir1 { get; private set; }
    public PlayerJumpToAirState2 jumpToAir2 { get; private set; }
    public PlayerAirState airState { get; private set; }

    public PlayerDashState dashState { get; private set; }
    public PlayerWallSlideState wallSlide { get; private set; }
    public PlayerWallJumpState wallJump { get; private set; }

    public PlayerAttack1 attack1 { get; private set; }
    public PlayerAttack2 attack2 { get; private set; }
    public PlayerAttack3 attack3 { get; private set; }

    public PlayerParryState parryState { get; private set; }
    public PlayerSuccessfulParryState successfulParry { get; private set; }

    public PlayerAimSwordState aimSword { get; private set; }
    public PlayerThrowSwordState throwSword { get; private set; }
    public PlayerCatchSwordState catchSword { get; private set; }

    public PlayerFreezeState freezeState { get; private set; }

    public PlayerHitState hitState { get; private set; }
    public PlayerDiedState diedState { get; private set; }
    #endregion

    [HideInInspector] public SkillManager skill;

    #region  Info
    public Transform ceilingCheck;
    public float ceilingCheckDistance;

    [Header("Move Info")]
    public float moveSpeed;
    public float jumpForce;

    [Header("Dash Info")]
    public float dashSpeed;
    public float dashDuration;
    public float dashDir { get; private set; }

    [Header("Attack Info")]
    public float comboWindow;
    public int comboCounter;
    [HideInInspector] public float lastTimeAttacked;

    [HideInInspector] public bool attackTypeAhead;
    [HideInInspector] public float attackMoveSpeed;

    public float counterAttackDuration = .2f;

    [Header("Sword Skill")]
    public GameObject sword;

    [Space]
    public float wallJumpDuration;
    #endregion
    
    protected override void Awake()
    {
        base.Awake();

        stateMachine = new StateMachine<PlayerState>();

        idleState = new PlayerIdleState(this, "Idle");
        moveState = new PlayerMoveState(this, "Move");

        jumpState = new PlayerJumpState(this, "Jump");
        jumpToAir1 = new PlayerJumpToAirState1(this, "JumpToAir1");
        jumpToAir2 = new PlayerJumpToAirState2(this, "JumpToAir2");
        airState = new PlayerAirState(this, "Air");

        dashState = new PlayerDashState(this, "Dash");
        wallSlide = new PlayerWallSlideState(this, "WallSlide");
        wallJump = new PlayerWallJumpState(this, "Jump");

        attack1 = new PlayerAttack1(this, "Attack1");
        attack2 = new PlayerAttack2(this, "Attack2");
        attack3 = new PlayerAttack3(this, "Attack3");

        parryState = new PlayerParryState(this, "Parry");
        successfulParry = new PlayerSuccessfulParryState(this, "SuccessfulParry");

        aimSword = new PlayerAimSwordState(this, "AimSword");
        throwSword = new PlayerThrowSwordState(this, "ThrowSword");
        catchSword = new PlayerCatchSwordState(this, "CatchSword");

        freezeState = new PlayerFreezeState(this, "Freeze");

        hitState = new PlayerHitState(this, "Hit");
        diedState = new PlayerDiedState(this, "Died");
    }

    protected override void Start()
    {
        base.Start();

        stateMachine.Initialize(idleState);

        skill = SkillManager.instance;
    }

    protected override void Update()
    {
        base.Start();

        stateMachine.currentState.Update();
        if (Input.GetKeyDown(KeyCode.F)) skill.crystal.CanUseSkill();
    }

    public virtual void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();
    public void SetAttackMoveSpeed(float speed) => attackMoveSpeed = speed;

    public bool CanDamage()
    {
        if (stateMachine.currentState == successfulParry || stateMachine.currentState == dashState || stateMachine.currentState == hitState) return false;
        else return true;
    }

    public override void IsDied()
    {
        base.IsDied();

        stateMachine.ChangeState(diedState);
    }

    public override void DamageFX(Transform damageFrom)
    {
        base.DamageFX(damageFrom);

        if ((damageFrom.position.x - transform.position.x < 0 && facingRight) || (damageFrom.position.x - transform.position.x > 0 && !facingRight)) Flip();
        stateMachine.ChangeState(hitState);
    }

    public void CheckForDashInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && SkillManager.instance.dash.CanUseSkill()) 
        {
            dashDir = Input.GetAxisRaw("Horizontal");
            if (dashDir == 0) dashDir = facingDir;

            stateMachine.ChangeState(dashState);
        }
    }

    public bool CheckForParry(Transform _damageFrom)
    {
        if (stateMachine.currentState == parryState && _damageFrom.GetComponent<Enemy>() != null)
        {
            stateMachine.ChangeState(successfulParry);
            _damageFrom.GetComponent<Enemy>().IsStunned();

            return true;
        }
        else return false;
    }

    public virtual bool IsCeilingDetected() => Physics2D.Raycast(ceilingCheck.position, Vector2.up, ceilingCheckDistance, whatIsGround);

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawLine(ceilingCheck.position, new Vector3(ceilingCheck.position.x, ceilingCheckDistance + ceilingCheck.position.y));
    }

}

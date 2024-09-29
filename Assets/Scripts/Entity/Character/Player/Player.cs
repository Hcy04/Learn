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
    #endregion

    [HideInInspector] public SkillManager skill;
    [HideInInspector] public SpriteRenderer sr;

    #region  Info  
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
    public float catchSpeed;

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
    }

    protected override void Start()
    {
        base.Start();

        stateMachine.Initialize(idleState);

        skill = SkillManager.instance;
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    protected override void Update()
    {
        base.Start();

        stateMachine.currentState.Update();
        if (Input.GetKeyDown(KeyCode.F)) skill.crystal.CanUseSkill();
    }

    public virtual void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();
    public void SetAttackMoveSpeed(float speed) => attackMoveSpeed = speed;

    public void CheckForDashInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && SkillManager.instance.dash.CanUseSkill()
            && stateMachine.currentState != wallSlide && stateMachine.currentState != successfulParry 
            && stateMachine.currentState != aimSword && stateMachine.currentState != throwSword
            && stateMachine.currentState != freezeState) 
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

}

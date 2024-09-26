using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class Player : Character
{
    #region States
    public StateMachine<PlayerState> stateMachine { get; private set; }

    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerWallSlideState wallSlide { get; private set; }
    public PlayerWallJumpState wallJump { get; private set; }

    public PlayerAttack1 attack1 { get; private set; }
    public PlayerAttack2 attack2 { get; private set; }
    public PlayerAttack3 attack3 { get; private set; }

    public PlayerCounterAttackState counterAttack { get; private set; }

    public PlayerAimSwordState aimSword { get; private set; }
    public PlayerCatchSwordState catchSword { get; private set; }
    #endregion

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

    public bool attackTypeAhead;
    [HideInInspector] public float attackMoveSpeed;

    public float counterAttackDuration = .2f;

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
        airState = new PlayerAirState(this, "Jump");
        dashState = new PlayerDashState(this, "Dash");
        wallSlide = new PlayerWallSlideState(this, "WallSlide");
        wallJump = new PlayerWallJumpState(this, "Jump");

        attack1 = new PlayerAttack1(this, "Attack1");
        attack2 = new PlayerAttack2(this, "Attack2");
        attack3 = new PlayerAttack3(this, "Attack3");

        counterAttack = new PlayerCounterAttackState(this, "CounterAttack");

        aimSword = new PlayerAimSwordState(this, "AimSword");
        catchSword = new PlayerCatchSwordState(this, "CatchSword");
    }

    protected override void Start()
    {
        base.Start();

        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Start();

        stateMachine.currentState.Update();
    }

    public virtual void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();
    public void SetAttackMoveSpeed(float speed) => attackMoveSpeed = speed;

    public void CheckForDashInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && SkillManager.instance.dash.CanUseSkill()
            && stateMachine.currentState != wallSlide && stateMachine.currentState != counterAttack) 
        {
            dashDir = Input.GetAxisRaw("Horizontal");
            if (dashDir == 0) dashDir = facingDir;

            stateMachine.ChangeState(dashState);
        }
    }

    public override void Damage(Transform damagePosition)
    {
        fx.StartCoroutine("FlashFX");
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Player : Entity
{
    #region States
    public PlayerStateMachine stateMachine { get; private set; }

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

        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        airState = new PlayerAirState(this, stateMachine, "Jump");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
        wallSlide = new PlayerWallSlideState(this, stateMachine, "WallSlide");
        wallJump = new PlayerWallJumpState(this, stateMachine, "Jump");

        attack1 = new PlayerAttack1(this, stateMachine, "Attack1");
        attack2 = new PlayerAttack2(this, stateMachine, "Attack2");
        attack3 = new PlayerAttack3(this, stateMachine, "Attack3");

        counterAttack = new PlayerCounterAttackState(this, stateMachine, "CounterAttack");

        aimSword = new PlayerAimSwordState(this, stateMachine, "AimSword");
        catchSword = new PlayerCatchSwordState(this, stateMachine, "CatchSword");
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

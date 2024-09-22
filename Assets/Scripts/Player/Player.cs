using System.Collections;
using System.Collections.Generic;
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
    #endregion

    #region  Info  
    [Header("Move Info")]
    public float moveSpeed;
    public float jumpForce;

    [Header("Dash Info")]
    [SerializeField] private float dashCD;
    private float dashUsageTimer;
    public float dashSpeed;
    public float dashDuration;
    public float dashDir { get; private set; }

    [Header("Attack Info")]
    public float comboWindow;
    public int comboCounter;
    public float lastTimeAttacked;
    public bool attackTypeAhead;
    public float attackMoveSpeed;

    [Space]
    public float wallJumpDuration;
    #endregion
    
    //public bool isBusy;

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

    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();
    public void SetAttackMoveSpeed(float speed) => attackMoveSpeed = speed;

    public void CheckForDashInput()
    {
        dashUsageTimer -= Time.deltaTime;
        
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashUsageTimer < 0) 
        {
            dashUsageTimer = dashCD;

            dashDir = Input.GetAxisRaw("Horizontal");
            if (dashDir == 0) dashDir = facingDir;

            stateMachine.ChangeState(dashState);
        }
    }

    /*public IEnumerable Busyfor(float _seconds)
    {
        isBusy = true;

        yield return new WaitForSeconds(_seconds);

        isBusy = false;
    }*/
}

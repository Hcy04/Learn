using UnityEngine;

public class Clone : Projectile
{
    private bool homingTarget;
    private bool addComboCounter;
    public bool canCreatNewClone;

    private SpriteRenderer sr;
    private Player player;

    private float colorloosingSpeed;
    [HideInInspector] public bool triggerCalled;

    public float damage;
    public Transform attackCheck;
    public float attackCheckRadius;
    public float moveSpeed;

    private int attackDir = 1;
    private Transform clocestTarget = null;
    
    protected override void Awake()
    {
        base.Awake();

        sr = GetComponentInChildren<SpriteRenderer>();
        player = PlayerManager.instance.player;
    }

    protected override void Start()
    {
        base.Start();

        triggerCalled = false;
    }

    protected override void Update()
    {
        base.Update();

        rb.velocity = new Vector2(moveSpeed * attackDir, 0);

        if (triggerCalled)
        {
            sr.color = new Color(1, 1, 1, sr.color.a - (Time.deltaTime * colorloosingSpeed));

            if (sr.color.a <= 0)
            {
                if (canCreatNewClone && clocestTarget != null && clocestTarget.GetComponent<Collider2D>().enabled)
                {
                    if (Random.Range(0, 100) < 35) SkillManager.instance.clone.CreatClone(transform);
                }

                Destroy(this.gameObject);
            } 
        }
    }

    public void SetupClone(float _colorloosingSpeed, bool _homingTarget, bool _addComboCounter, bool _canCreatNewClone)
    {
        colorloosingSpeed = _colorloosingSpeed;

        homingTarget = _homingTarget;
        addComboCounter = _addComboCounter;
        canCreatNewClone = _canCreatNewClone;

        FaceClosestTarget();
        if (clocestTarget == null) return;

        if (!player.IsGroundDetected()) anim.SetInteger("AttackNumber", 2);
        else 
        {
            int nextCombo = player.comboCounter % 3 + 1;
            anim.SetInteger("AttackNumber", nextCombo);

            damage = player.stats.damage.baseValue;
            if (nextCombo == 1) damage  *= 1;
            else if (nextCombo == 2) damage *= .5f;
            else if (nextCombo == 3) damage *= 1.5f;
            
            if (addComboCounter)
            {
                player.lastTimeAttacked = Time.time;
                player.comboCounter++;
            }
        }
    }

    private void FaceClosestTarget()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 5);

        foreach (var hit in colliders)
        {
            if (hit.gameObject.layer == 12)
            {
                if (clocestTarget == null ||
                    (Vector2.Distance(transform.position, clocestTarget.position) > Vector2.Distance(transform.position, hit.transform.position)))
                    clocestTarget = hit.transform;
            }
        }

        if (clocestTarget == null) 
        {
            Destroy(this.gameObject);
            return;
        }
        
        if (homingTarget)
        {
            int temp = Random.Range(0, 2);
            if (temp == 0) temp = -1;
            transform.position = new Vector2(clocestTarget.transform.position.x + temp * 1.5f, clocestTarget.transform.position.y);

            if (transform.position.x > clocestTarget.position.x)
            {
                attackDir *= -1;
                transform.Rotate(0, 180, 0);
            }
        }
        else 
        {
            if (player.facingDir == -1)
            {
                attackDir *= -1;
                transform.Rotate(0, 180, 0);
            }
        }
    }
}

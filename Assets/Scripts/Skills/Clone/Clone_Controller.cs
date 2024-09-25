using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShortcutManagement;
using UnityEngine;

public class Clone_Controller : MonoBehaviour
{
    private SpriteRenderer sr;
    public Animator anim;
    public Rigidbody2D rb;

    private float colorloosingSpeed;
    private float cloneTimer;

    public Transform attackCheck;
    public int attackDir;
    public float attackCheckRadius;
    
    private void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (cloneTimer > 0) cloneTimer -= Time.deltaTime;
        else
        {
            sr.color = new Color(1, 1, 1, sr.color.a - (Time.deltaTime * colorloosingSpeed));

            if (sr.color.a <= 0) Destroy(this.gameObject);
        }
    }

    public void SetupClone(Transform _newTransform, float _cloneDuration, float _colorloosingSpeed)
    {
        transform.position = _newTransform.position;
        cloneTimer = _cloneDuration;
        colorloosingSpeed = _colorloosingSpeed;

        if (true) 
        {
            FaceClosestTarget();

            if (!PlayerManager.instance.player.IsGroundDetected()) anim.SetInteger("AttackNumber", 2);
            else anim.SetInteger("AttackNumber", PlayerManager.instance.player.comboCounter + 1);
        }
    }

    private Transform clocestTarget;

    private void FaceClosestTarget()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 10);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                if (clocestTarget == null ||
                    (Vector2.Distance(transform.position, clocestTarget.position) > Vector2.Distance(transform.position, hit.transform.position)))
                    clocestTarget = hit.GetComponent<Enemy>().transform;
            }
        }
        
        if (clocestTarget != null && transform.position.x > clocestTarget.position.x) transform.Rotate(0, 180, 0);
    }
}

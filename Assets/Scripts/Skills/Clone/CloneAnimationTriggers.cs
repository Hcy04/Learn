using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneAnimationTriggers : MonoBehaviour
{
    private Clone_Controller clone => GetComponentInParent<Clone_Controller>();

    private void AnimationTrigger()
    {
        clone.anim.SetInteger("AttackNumber", 0);
    }

    private void HandleAttackMoveSpeed(float speed)
    {
        clone.rb.velocity = new Vector2(speed * clone.attackDir, 0);
    }

    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(clone.attackCheck.position, clone.attackCheckRadius);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null) hit.GetComponent<Enemy>().Damage(transform);
        }
    }
}

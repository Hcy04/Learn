using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneAnimationTriggers : MonoBehaviour
{
    private Clone clone => GetComponentInParent<Clone>();

    private void AnimationTrigger()
    {
    }

    private void HandleAttackMoveSpeed(float speed)
    {
        clone.moveSpeed = speed;
    }

    private void AttackTrigger()
    {
        clone.triggerCalled = true;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(clone.attackCheck.position, clone.attackCheckRadius);

        foreach (var hit in colliders)
        {
            if (hit.gameObject.layer == 12) hit.GetComponent<EnemyStats>().TakeDamage(clone.transform, clone.damage, 0, 0, 0);
        }
    }
}

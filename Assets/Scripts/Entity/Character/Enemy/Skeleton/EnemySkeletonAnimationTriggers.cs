using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkeletonAnimationTriggers : MonoBehaviour
{
    private Enemy_Skeleton skeleton => GetComponentInParent<Enemy_Skeleton>();

    private void AnimationTrigger()
    {
        skeleton.AnimationFinishTrigger();
    }

    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(skeleton.attackCheck.position, skeleton.attackCheckRadius);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Player>() != null) hit.GetComponent<Player>().Damage(skeleton.transform);
        }
    }

    private void AttackWarning()
    {
        skeleton.AttackWarning();
    }
}

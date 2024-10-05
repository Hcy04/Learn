using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class PlayerAnimationTriggers : MonoBehaviour
{
    private Player player => GetComponentInParent<Player>();

    private void AnimationTrigger()
    {
        player.AnimationFinishTrigger();
    }

    private void HandleAttackMoveSpeed(float speed)
    {
        player.SetAttackMoveSpeed(speed);
    }

    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);

        foreach (var hit in colliders)
        {
            if (hit.gameObject.layer == 12) player.stats.DoDamage(hit.GetComponent<EnemyStats>());
        }
    }

    private void ThrowSword()
    {
        player.skill.sword.CreateSword();
    }
}

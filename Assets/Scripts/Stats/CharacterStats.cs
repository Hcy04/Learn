using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    Character character;

    public Stat maxHealth;
    public Stat damage;

    [SerializeField] protected float currentHealth;

    protected virtual void Start()
    {
        character = GetComponent<Character>();

        currentHealth = maxHealth.GetValue();
    }

    public virtual void DoDamage(CharacterStats _targetStats)
    {
        _targetStats.TakeDamage(transform, damage.GetValue());
    }

    public virtual void TakeDamage(Transform damageFrom, float _damage)
    {
        character.DamageFX(damageFrom);

        currentHealth -= _damage;
        if (currentHealth <= 0) Die();
    }

    protected virtual void Die()
    {
        Debug.Log(transform.gameObject.name + " Die");
    }
}

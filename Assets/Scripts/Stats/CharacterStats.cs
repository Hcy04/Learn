using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;

    [Header("Damage")]
    public int damage;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int _damage)
    {
        currentHealth -= _damage;
        if (currentHealth <= 0) Die();
    }

    private void Die()
    {
        Debug.Log(transform.gameObject.name + " Die");
    }
}

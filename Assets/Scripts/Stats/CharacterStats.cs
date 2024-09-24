using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int damege;
    public int maxHP;

    [SerializeField] private int currentHP;

    void Start()
    {
        currentHP = maxHP;
    }

    public void TakeDamege(int _damage)
    {
        currentHP -= _damage;
    }
}

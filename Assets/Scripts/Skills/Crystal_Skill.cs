using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Crystal_Skill : Skill
{
    [Header("Skill Active")]
    [SerializeField] private bool canCreatCrystal;
    [SerializeField] private bool addDuration;
    [SerializeField] private bool canExplode;
    [SerializeField] private bool canMoving;

    [Header("Skill Info")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private GameObject crystalPrefab;
    [SerializeField] private float attackCheckRadius;
    private GameObject currentCrystal;

    Crystal crystalScript;

    public override bool CanUseSkill()
    {
        if (base.CanUseSkill() && canCreatCrystal)
        {
            if (currentCrystal == null)
            {
                currentCrystal = Instantiate(crystalPrefab, player.transform.position, Quaternion.identity);
                crystalScript = currentCrystal.GetComponent<Crystal>();
                crystalScript.SetUpCrystal(attackCheckRadius, moveSpeed, addDuration, canExplode, canMoving);
            }
            else if (crystalScript.crystalTimer >= 0)
            {
                if (!canMoving)
                {
                    Vector2 playerPos = player.transform.position;
                    player.transform.position = currentCrystal.transform.position;
                    currentCrystal.transform.position = playerPos;

                    if (canExplode) crystalScript.DestroyCrystal();
                    else Destroy(currentCrystal);
                }
            }
            return true;
        }
        else return false;
    }
}

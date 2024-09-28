using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Crystal_Skill : Skill
{
    [Header("Skill Info")]
    [SerializeField] private bool canMoving;
    [SerializeField] private float moveSpeed;
    [SerializeField] private GameObject crystalPrefab;
    [SerializeField] private float crystalDuration;
    [SerializeField] private float attackCheckRadius;
    private GameObject currentCrystal;

    Crystal crystalScript;

    public override void UseSkill()
    {
        base.UseSkill();

        if (currentCrystal == null)
        {
            currentCrystal = Instantiate(crystalPrefab, player.transform.position, Quaternion.identity);
            crystalScript = currentCrystal.GetComponent<Crystal>();
            crystalScript.SetUpCrystal(crystalDuration, attackCheckRadius, canMoving, moveSpeed);
        }
        else if (currentCrystal.GetComponent<Crystal>().crystalTimer >= 0)
        {
            if (!canMoving)
            {
                Vector2 playerPos = player.transform.position;
                player.transform.position = currentCrystal.transform.position;
                currentCrystal.transform.position = playerPos;

                crystalScript.DestroyCrystal();
            }
        }
    }
}

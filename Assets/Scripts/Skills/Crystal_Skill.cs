using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Crystal_Skill : Skill
{
    [Header("Skill Info")]
    [SerializeField] private GameObject crystalPrefab;
    private GameObject currentCrystal;
    [SerializeField] private float crystalDuration;

    public override void UseSkill()
    {
        base.UseSkill();

        if (currentCrystal == null)
        {
            currentCrystal = Instantiate(crystalPrefab, player.transform.position, Quaternion.identity);
            currentCrystal.GetComponent<Crystal>().SetUpCrystal(crystalDuration);
        }
        else if (currentCrystal.GetComponent<Crystal>().crystalTimer >= 0)
        {
            player.transform.position = currentCrystal.transform.position;
            Destroy(currentCrystal);
        }
    }
}

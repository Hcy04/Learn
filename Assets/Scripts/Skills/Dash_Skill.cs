using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash_Skill : Skill
{
    [Header("Skill Active")]
    [SerializeField] private bool canDash;

    public override bool CanUseSkill()
    {
        if (base.CanUseSkill() && canDash) return true;
        else return false;
    }
}

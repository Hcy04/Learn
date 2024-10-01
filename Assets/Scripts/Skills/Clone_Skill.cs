using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Clone_Skill : Skill
{
    [Header("Skill Active")]
    [SerializeField] private bool cloneDashEnter;
    [SerializeField] private bool cloneDashExit;
    [SerializeField] private bool cloneParry;
    [SerializeField] private bool homingTarget;
    [SerializeField] private bool addComboCounter;
    [SerializeField] private bool canCreatNewClone;//分身攻击动作结束有概率创建新的分身

    [Header("Skill Info")]
    [SerializeField] private float colorloosingSpeed;

    public void CloneDashEnter()
    {
        if (cloneDashEnter) CreatClone(player.transform);
    }

    public void CloneDashExit()
    {
        if (cloneDashExit) CreatClone(player.transform);
    }

    public void CloneParry()
    {
        player.comboCounter = 2;
        if (cloneParry) CreatClone(player.transform);
    }

    public void CreatClone(Transform _clonePosition)
    {
        GameObject newClone = spawner.CreatClone(_clonePosition.position);

        newClone.GetComponent<Clone>().SetupClone(colorloosingSpeed, homingTarget, addComboCounter, canCreatNewClone);
    }
}

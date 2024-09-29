using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freeze_Skill : Skill
{
    [Header("Skill Active")]
    [SerializeField] private bool canFreeze;
    [SerializeField] private bool addAmount;

    [Header("Skill Info")]
    [SerializeField] private int amountOfAttack;
    [HideInInspector] public bool isActive;
    private float lastAttack;

    private Vector2 playerDefaultPosition;
    private List<Transform> targetList;
    private int targetindex;

    protected override void Start()
    {
        base.Start();

        targetList = new List<Transform>();
    }

    protected override void Update()
    {
        base.Update();

        if (isActive)
        {
            if (targetList.Count <= 0)
            {
                CDTimer = 0;
                SkillEnd();
            }
            else if (Time.time - lastAttack > 0.4f)
            {
                amountOfAttack--;
                
                targetindex++;
                if (targetindex >= targetList.Count) targetindex = 0;

                player.comboCounter = Random.Range(0, 3);
                if (targetList[targetindex].GetComponent<Collider2D>().enabled) SkillManager.instance.clone.CreatClone(targetList[targetindex].transform);

                lastAttack = Time.time;
            }
            else if (amountOfAttack <= 0) SkillEnd();
        }
    }

    public override bool CanUseSkill()
    {
        if (base.CanUseSkill() && canFreeze) return true;
        else return false;
    }

    public void SkillStart()
    {
        isActive = true;
        player.stateMachine.ChangeState(player.freezeState);
        playerDefaultPosition = player.transform.position;

        if (!addAmount) amountOfAttack = 6;
        else amountOfAttack = 10;
        
        FindTarget();
    }

    private void SkillEnd()
    {
        isActive = false;
        player.stateMachine.ChangeState(player.idleState);
        player.transform.position = playerDefaultPosition;

        targetindex = 0;
    }

    private void FindTarget()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.transform.position, 15);

        foreach (var collider in colliders)  
        {
            if (collider.GetComponent<Enemy>() != null) targetList.Add(collider.transform);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freeze_Skill : Skill
{
    public bool isActive;
    [SerializeField] private int amoutOfAttack = 10;
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
                amoutOfAttack--;
                
                targetindex++;
                if (targetindex >= targetList.Count) targetindex = 0;

                player.comboCounter = Random.Range(0, 3);
                SkillManager.instance.clone.CreatClone(targetList[targetindex].transform);

                lastAttack = Time.time;
            }
            else if (amoutOfAttack <= 0) SkillEnd();
        }
    }

    public void SkillStart()
    {
        isActive = true;
        player.stateMachine.ChangeState(player.freezeState);
        playerDefaultPosition = player.transform.position;

        FindTarget();
    }

    private void SkillEnd()
    {
        isActive = false;
        player.stateMachine.ChangeState(player.idleState);
        player.transform.position = playerDefaultPosition;

        amoutOfAttack = 10;
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

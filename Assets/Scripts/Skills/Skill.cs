using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    [Header("CD Info")]
    public float CD;
    public float CDTimer;

    protected Player player;
    protected Spawner spawner;

    protected virtual void Start()
    {
        player = PlayerManager.instance.player;
        spawner = Spawner.instance;
    }

    protected virtual void Update()
    {
        CDTimer -= Time.deltaTime;
    }

    public virtual bool CanUseSkill()
    {
        if (CDTimer < 0) 
        {
            CDTimer = CD;
            return true;
        }

        return false;
    }
}

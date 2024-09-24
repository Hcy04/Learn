using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Clone_Skill : Skill
{
    [SerializeField] private GameObject clonePrefab;
    [SerializeField] private float cloneDuration;
    [SerializeField] private float colorloosingSpeed;

    public void CreatClone(Transform _clonePosition)
    {
        GameObject newClone = Instantiate(clonePrefab);

        newClone.GetComponent<Clone_Controller>().SetupClone(_clonePosition, cloneDuration, colorloosingSpeed);
    }
}

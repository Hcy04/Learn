using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Data", menuName = "Data/Drop List")]

public class DropList : ScriptableObject
{
    public ItemData[] possibleDrop;
    [Range(0f, 1f)]
    public float[] dropPercentage;
}

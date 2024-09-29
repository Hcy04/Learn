using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Stat
{
    public float baseValue;
    public List<float> modifiers;
    
    public float strength = 1;

    public float GetValue()
    {
        float finalValue = baseValue;

        foreach (float modifier in modifiers) finalValue += modifier;
        finalValue *= strength;

        return finalValue;
    }
}

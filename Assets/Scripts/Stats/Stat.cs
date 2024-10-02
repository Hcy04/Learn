using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Stat
{
    public float baseValue;
    public List<float> modifiers;
    
    public float percentage = 1;

    public float GetValue()
    {
        float finalValue = baseValue;

        foreach (float modifier in modifiers) finalValue += modifier;
        finalValue *= percentage;

        return finalValue;
    }

    public void SetDefaultValue(float _value)
    {
        baseValue = _value;
    }

    public void AddModifier(float _modifier)
    {
        modifiers.Add(_modifier);
    }

    public void RemoveModifier(float _modifier)
    {
        modifiers.Remove(_modifier);
    }
    
}

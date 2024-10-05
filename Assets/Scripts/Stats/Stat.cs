using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Stat
{
    public float baseValue;
    public List<float> modifiers;
    
    public float percentage = 1;

    public float GetBaseValue()
    {
        float finalValue = baseValue;

        foreach (float modifier in modifiers) finalValue += modifier;

        return finalValue;
    }

    public float GetValue()
    {
        float finalValue = GetBaseValue();

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

        UI_Manager.instance.UpdateValue();
    }

    public void RemoveModifier(float _modifier)
    {
        modifiers.Remove(_modifier);

        UI_Manager.instance.UpdateValue();
    }
    
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public static class Factory
{
    public static T CopyInstance<T>(T source) where T : ScriptableObject  
    { 
        T instance = ScriptableObject.CreateInstance<T>();
        Type type = typeof(T);

        foreach (FieldInfo field in type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))  
        {
            object value = field.GetValue(source);  
            field.SetValue(instance, value);  
        }

        return instance;  
    }  
}

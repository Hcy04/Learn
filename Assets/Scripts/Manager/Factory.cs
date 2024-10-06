using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System.Collections.ObjectModel;

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

    private static readonly ReadOnlyDictionary<string, string> TranslationDictionary = new ReadOnlyDictionary<string, string>(new Dictionary<string, string>  
    {  
        {"vitality", "生命"},
        {"strength", "力量"},
        {"agility", "敏捷"},
        {"intelligence", "智力"},
        {"maxHealth", "最大生命"},
        {"damage", "攻击力"},
        {"armor", "防御力"},
        {"critChance", "暴击率"},
        {"critDamage", "暴击伤害"},
        {"fireDamage", "火伤"},
        {"iceDamage", "冰伤"},
        {"lightningDamage", "雷伤"},
        {"fireResistance", "火抗"},
        {"iceResistance", "冰抗"},
        {"lightningResistance", "雷抗"},

        {"Weapon", "武器"},
        {"Armor", "护甲"},
        {"Amulet", "护符"},
        {"Magic", "法术书"},

        {"Potion", "药水"},
        {"Material", "材料"}
    });

    private static readonly ReadOnlyDictionary<string, string> DescriptionDictionary = new ReadOnlyDictionary<string, string>(new Dictionary<string, string>  
    {  
        {"生命", "提升最大生命值"},
        {"力量", "提高攻击力"},
        {"敏捷", "敏捷"},
        {"智力", "提高魔法伤害和抗性"},

        {"最大生命值", "最大生命值"},
        {"攻击力", "攻击力"},
        {"防御力", "防御力"},
        {"暴击率", "触发暴击伤害的概率"},
        {"暴击伤害", "暴击时的伤害加成"},

        {"元素伤害", "无视防御 概率添加debuff"},
        {"元素抗性", "百分比抵抗对应伤害"},

        {"原木", "常见的材料 用途广泛"},
        {"金锭", "金制成的锭"},
        {"动物皮", "未知动物的皮"},
        {"原石", "普通的石头"},
        {"火水晶", "温热的水晶"},
        {"冰晶", "寒冷的水晶"},

        {"伤害提升药水", ""},
        {"治疗药水", ""}
    });

    public static string Translation(string str)
    {
        return TranslationDictionary[str];
    }

    public static string GetDescription(string str)
    {
        return DescriptionDictionary[str];
    }
}

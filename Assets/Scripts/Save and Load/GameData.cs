using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class GameData
{
    public int currency;

    public SerializableDictionary<string, int> inventory;
    public SerializableDictionary<string, int> equipment;

    public SerializableDictionary<string, int> skillTree;

    public GameData()
    {
        currency = 0;

        inventory = new SerializableDictionary<string, int>();
        equipment = new SerializableDictionary<string, int>();

        skillTree = new SerializableDictionary<string, int>();
    }
}

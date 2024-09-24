using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    public List<ItemData> inventory = new List<ItemData>();

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void AddItem(ItemData _item)
    {
        inventory.Add(_item);
    }
}

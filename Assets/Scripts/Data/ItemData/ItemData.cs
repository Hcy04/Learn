using UnityEngine;

public enum ItemType
{
    Material,
    Equipment
}

public class ItemData : ScriptableObject
{
    public ItemType itemType;
    
    public string itemName;
    public Sprite icon;
}

using System.Text;
using UnityEngine;

public enum ItemType
{
    Material,
    Equipment,
    Potion
}

public class ItemData : ScriptableObject
{
    public ItemType itemType;
    
    public string itemName;
    public Sprite icon;

    protected StringBuilder sb = new StringBuilder();

    public virtual string GetDescription()
    {
        return "";
    }
}

using System.Text;
using UnityEditor;
using UnityEngine;

public enum ItemType
{
    Material,
    Equipment,
    Potion
}

public class ItemData : ScriptableObject
{
    public string itemId;

    public ItemType itemType;
    
    public string itemName;
    public Sprite icon;

    protected StringBuilder sb = new StringBuilder();

    private void OnValidate()
    {
        #if UNITY_EDITOR
        string path = AssetDatabase.GetAssetPath(this);
        itemId = AssetDatabase.AssetPathToGUID(path);
        #endif
    }

    public virtual string GetDescription()
    {
        return "";
    }
}

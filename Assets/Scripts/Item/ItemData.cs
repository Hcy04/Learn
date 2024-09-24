using UnityEngine;

[CreateAssetMenu(fileName = "New Item Data", menuName = "Data/Item")]

public class ItemData : ScriptableObject
{
    public string ItemName;
    public Sprite icon;
}

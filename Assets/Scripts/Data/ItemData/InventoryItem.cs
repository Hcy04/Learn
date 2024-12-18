using System;

[Serializable]

public class InventoryItem
{
    public ItemData data;
    public int stackSize;

    public InventoryItem(ItemData _newItemData)
    {
        data = _newItemData;
        stackSize = 1;
    }

    public void AddStack() => stackSize++;
    public void RemoveStack() => stackSize--;
}

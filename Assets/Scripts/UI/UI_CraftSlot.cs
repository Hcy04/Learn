using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_CraftSlot : UI_ItemSlot
{
    private void OnEnable()
    {
        UpdateSlot(item);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (item == null || item.stackSize == 0) return;

        ItemData_Equipment craftInfo = (ItemData_Equipment)item.data;

        Inventory.instance.CanCraft(craftInfo);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_EquipmentSlot : UI_ItemSlot
{
    public EquipmentType slotType;

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (item == null || item.stackSize == 0) return;

        Inventory.instance.ManageEquipment((ItemData_Equipment)item.data, false, true);
    }
}

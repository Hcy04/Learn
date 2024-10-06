using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_CraftList : MonoBehaviour
{
    [SerializeField] private Transform craftSlotParent;

    [SerializeField] private List<ItemData_Equipment> weaponList;
    [SerializeField] private List<ItemData_Equipment> armorList;
    [SerializeField] private List<ItemData_Equipment> amuletList;
    [SerializeField] private List<ItemData_Equipment> magicList;
    [SerializeField] private List<UI_CraftSlot> craftSlots;

    void Start()
    {
        SetupCraftList(0);
    }

    public void SetupCraftList(int type)
    {
        List<ItemData_Equipment> craftList;
        if (type == 0) craftList = weaponList;
        else if (type == 1) craftList = armorList;
        else if (type == 2) craftList = amuletList;
        else if (type == 3) craftList = magicList;
        else craftList = null;

        for (int i = 0; i < craftSlots.Count; i++) Destroy(craftSlots[i].gameObject);

        craftSlots = new List<UI_CraftSlot>();

        for (int i = 0; i < craftList.Count; i++) 
        {
            GameObject newSlot = Spawner.instance.CreatCraftSlot(craftSlotParent);
            newSlot.GetComponent<UI_CraftSlot>().SetupCraftSlot(craftList[i]);

            craftSlots.Add(newSlot.GetComponent<UI_CraftSlot>());
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_CraftPanel : MonoBehaviour
{
    [SerializeField] private GameObject itemInfo;
    [SerializeField] private GameObject materials;

    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI statDescription;

    [SerializeField] private Transform materialsParent;

    private ItemData_Equipment equipmentToCraft;
    [SerializeField] private TextMeshProUGUI canCraft;

    public void SetUpPanel(ItemData_Equipment equipment)
    {
        equipmentToCraft = equipment;

        UI_CraftMaterialSlot[] slots = materialsParent.GetComponentsInChildren<UI_CraftMaterialSlot>();
        for (int i = 0; i < slots.Length; i++) Destroy(slots[i].gameObject);

        if (itemInfo.activeSelf == false) itemInfo.SetActive(true);
        if (materials.activeSelf == false) materials.SetActive(true);

        itemImage.sprite =  equipment.icon;
        itemName.text = equipment.itemName;
        statDescription.text = equipment.GetDescription();


        foreach(InventoryItem item in equipment.craftingMaterials)
        {
            GameObject newSlot = Spawner.instance.CreatCraftMaterialSlot(materialsParent);
            newSlot.GetComponent<UI_CraftMaterialSlot>().SetUpSlot((ItemData_Material)item.data, item.stackSize);
        }

        if(Inventory.instance.CanCraft(equipmentToCraft)) canCraft.text = "制作";
        else canCraft.text = "材料不足";
    }

    public void Crafting()
    {
        if (equipmentToCraft != null)
        {
            if(Inventory.instance.CanCraft(equipmentToCraft)) Inventory.instance.DoCraft(equipmentToCraft);
        }
    }
}

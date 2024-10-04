using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_ItemSlot : MonoBehaviour , IPointerDownHandler
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemText;

    public InventoryItem item;

    public void UpdateSlot(InventoryItem _newItem)
    {
        item = _newItem;

        itemImage.color = Color.white;

        if (item != null) 
        {
            itemImage.sprite = item.data.icon;
            if (item.stackSize > 1) itemText.text = item.stackSize.ToString();
            else itemText.text = "";
        }
    }

    public void CleanUpSlot()
    {
        item = null;

        itemImage.sprite = null;
        itemImage.color = Color.clear;
        itemText.text = "";
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if (item == null || item.stackSize == 0) return;

        if (Input.GetKey(KeyCode.LeftShift)) Inventory.instance.ManageItem(item.data, false);
        else if (Input.GetKey(KeyCode.LeftControl)) Inventory.instance.ManageItem(item, false);
        else if (item.data.itemType == ItemType.Equipment) Inventory.instance.ManageEquipment((ItemData_Equipment)item.data, true, true);
        else if (item.data.itemType == ItemType.Potion)
        {
            ItemData_Potion potion = (ItemData_Potion)item.data;
            
            PlayerManager.instance.player.stats.AddBuff(potion.buff);
            Inventory.instance.ManageItem(potion, false);
        }
    }
}

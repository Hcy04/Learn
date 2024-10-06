using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_CraftSlot : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemName;

    private ItemData_Equipment item;

    public void SetupCraftSlot(ItemData_Equipment _equipment)
    {
        item = _equipment;
        UpdateSlot();
    }

    public void UpdateSlot()
    {
        itemImage.sprite = item.icon;
        itemName.text = item.itemName;
    }

    public void ShowInfoInPanel()
    {
        UI_Manager.instance.craftPanel.SetUpPanel(item);
    }
}

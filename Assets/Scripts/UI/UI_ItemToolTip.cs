using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_ItemToolTip : MonoBehaviour
{
    [SerializeField] private RectTransform self;

    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemTypeText;
    [SerializeField] private TextMeshProUGUI itemDescription;

    public void ShowToolTip(ItemData item)
    {
        transform.position = self.anchoredPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        itemNameText.text = item.itemName;

        if (item.itemType == ItemType.Equipment)
        {
            ItemData_Equipment equipment = (ItemData_Equipment)item;
            
            itemTypeText.text = Factory.Translation(equipment.equipmentType.ToString());
            itemDescription.text = equipment.GetDescription();
        }
        else 
        {
            itemTypeText.text = Factory.Translation(item.itemType.ToString());
            itemDescription.text = Factory.GetDescription(item.itemName);
        }

        gameObject.SetActive(true);
    }

    public void HideToolTip() => gameObject.SetActive(false);
}

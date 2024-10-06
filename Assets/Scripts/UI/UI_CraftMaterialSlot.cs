using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_CraftMaterialSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI amount;

    [SerializeField] private ItemData_Material item;

    public void SetUpSlot(ItemData_Material _material, int _amount)
    {
        item = _material;

        itemImage.sprite = _material.icon;
        amount.text = _amount.ToString();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        UI_Manager.instance.toolTip.ShowToolTip(item);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UI_Manager.instance.toolTip.HideToolTip();
    }
}

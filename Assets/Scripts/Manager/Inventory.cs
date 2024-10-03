using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    public List<InventoryItem> equipment;
    public List<InventoryItem> inventory;
    public List<InventoryItem> materials;

    [Header("Inventory UI")]
    [SerializeField] private Transform equipmentSlotParent;
    [SerializeField] private Transform inventorySlotParent;
    [SerializeField] private Transform materialsSlotParent;
    
    private UI_EquipmentSlot[] equipmentSlot;
    private UI_ItemSlot[] inventorySlot;
    private UI_ItemSlot[] materialsSlot;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(instance.gameObject);
    }

    private void Start()
    {
        equipment = new List<InventoryItem>();
        inventory = new List<InventoryItem>();
        materials = new List<InventoryItem>();

        equipmentSlot = equipmentSlotParent.GetComponentsInChildren<UI_EquipmentSlot>();
        inventorySlot = inventorySlotParent.GetComponentsInChildren<UI_ItemSlot>();
        materialsSlot = materialsSlotParent.GetComponentsInChildren<UI_ItemSlot>();
        
    }

    public void ManageEquipment(ItemData_Equipment _equipment, bool _isAdd)
    {
        UI_EquipmentSlot targetSlot = equipmentSlot.First(UI_EquipmentSlot => UI_EquipmentSlot.slotType == _equipment.equipmentType);
        
        if (_isAdd)
        {
            if (targetSlot.item != null && targetSlot.item.stackSize != 0)
            {
                ManageEquipment((ItemData_Equipment)targetSlot.item.data, false);
                ManageEquipment(_equipment, true);
            }
            else
            {
                ManageItem(_equipment, false);

                equipment.Add(new InventoryItem(_equipment));
                _equipment.AddModifiers();
                targetSlot.UpdateSlot(equipment.Last());
            }
        }
        else
        {
            equipment.Remove(targetSlot.item);
            _equipment.RemoveModifiers();
            targetSlot.CleanUpSlot();

            ManageItem(_equipment, true);
        }
    }

    public void ManageItem(ItemData _item, bool _isAdd)
    {
        if (_item.itemType == ItemType.Equipment) ManageInventory((ItemData_Equipment)_item, _isAdd);
        else if (_item.itemType == ItemType.Material) ManageMaterial((ItemData_Material)_item, _isAdd);

        ReloadUI();
    }

    private void ManageInventory(ItemData_Equipment _equipment, bool _isAdd)
    {
        InventoryItem target = inventory.Find(InventoryItem => InventoryItem.data.itemName == _equipment.itemName);

        if (_isAdd)
        {
            if (target != null) target.stackSize++;
            else inventory.Add(new InventoryItem(_equipment));
        }
        else
        {
            if (target != null)
            {
                if (target.stackSize == 1) inventory.Remove(target);
                else target.stackSize--;
            }
            else Debug.LogError("Not found target item");
        }
    }

    private void ManageMaterial(ItemData_Material _material, bool _isAdd)
    {
        InventoryItem target = materials.Find(InventoryItem => InventoryItem.data.itemName == _material.itemName);

        if (_isAdd)
        {
            if (target != null) target.stackSize++;
            else materials.Add(new InventoryItem(_material));
        }
        else
        {
            if (target != null)
            {
                if (target.stackSize == 1) materials.Remove(target);
                else target.stackSize--;
            }
            else Debug.LogError("Not found target item");
        }
    }

    private void ReloadUI()
    {
        for (int i = 0; i < inventorySlot.Length; i++) inventorySlot[i].CleanUpSlot();
        for (int i = 0; i < materialsSlot.Length; i++) materialsSlot[i].CleanUpSlot();

        for (int i = 0; i < inventory.Count; i++) inventorySlot[i].UpdateSlot(inventory[i]);
        for (int i = 0; i < materials.Count; i++) materialsSlot[i].UpdateSlot(materials[i]);
    }

    public bool CanCraft(ItemData_Equipment _equipment)
    {
        for (int i = 0; i < _equipment.craftingMaterials.Count; i++)
        {
            InventoryItem targetMaterial = materials.Find(InventoryItem => InventoryItem.data.itemName == _equipment.craftingMaterials[i].data.itemName);

            if (targetMaterial == null || targetMaterial.stackSize < _equipment.craftingMaterials[i].stackSize) return false;
        }

        for (int i = 0; i < _equipment.craftingMaterials.Count; i++)
        {
            for (int j = 0; j < _equipment.craftingMaterials[i].stackSize; j++) ManageItem(_equipment.craftingMaterials[i].data, false);
        }

        ManageItem(_equipment, true);

        return true;
    }
}

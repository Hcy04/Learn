using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    public List<ItemData_Equipment> startingEquipment;
    public List<InventoryItem> startingItem;

    public List<InventoryItem> equipment;
    public List<InventoryItem> inventory;
    public List<InventoryItem> materials;
    public List<InventoryItem> potions;

    [Header("Inventory UI")]
    [SerializeField] private Transform equipmentSlotParent;
    [SerializeField] private Transform inventorySlotParent;
    [SerializeField] private Transform materialsSlotParent;
    [SerializeField] private Transform potionsSlotParent;
    
    private UI_EquipmentSlot[] equipmentSlot;
    private UI_ItemSlot[] inventorySlot;
    private UI_ItemSlot[] materialsSlot;
    private UI_ItemSlot[] potionsSlot;

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
        potions = new List<InventoryItem>();

        equipmentSlot = equipmentSlotParent.GetComponentsInChildren<UI_EquipmentSlot>();
        inventorySlot = inventorySlotParent.GetComponentsInChildren<UI_ItemSlot>();
        materialsSlot = materialsSlotParent.GetComponentsInChildren<UI_ItemSlot>();
        potionsSlot = potionsSlotParent.GetComponentsInChildren<UI_ItemSlot>();
        
        for (int i = 0; i < startingEquipment.Count; i++) ManageEquipment(startingEquipment[i], true, false);
        for (int i = 0; i < startingItem.Count; i++) ManageItem(startingItem[i], true);
    }

    public void ManageEquipment(ItemData_Equipment _equipment, bool _isAdd, bool _isPlayer)
    {
        UI_EquipmentSlot targetSlot = equipmentSlot.First(UI_EquipmentSlot => UI_EquipmentSlot.slotType == _equipment.equipmentType);
        
        if (_isAdd)
        {
            if (targetSlot.item != null && targetSlot.item.stackSize != 0 && _isPlayer)
            {
                ManageEquipment((ItemData_Equipment)targetSlot.item.data, false, true);
                ManageEquipment(_equipment, true, true);
            }
            else
            {
                if (_isPlayer) ManageItem(_equipment, false);

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

            if (_isPlayer)  ManageItem(_equipment, true);
        }
    }

    public void ManageItem(ItemData _item, bool _isAdd)
    {
        if (_item.itemType == ItemType.Equipment) ManageInventory((ItemData_Equipment)_item, _isAdd);
        else if (_item.itemType == ItemType.Material) ManageMaterial((ItemData_Material)_item, _isAdd);
        else if (_item.itemType == ItemType.Potion) ManagePotion((ItemData_Potion)_item, _isAdd);

        ReloadUI();
    }

    public void ManageItem(InventoryItem _items, bool _isAdd)
    {
        if (_items.data.itemType == ItemType.Equipment) ManageInventory(_items, _isAdd);
        else if (_items.data.itemType == ItemType.Material) ManageMaterial(_items, _isAdd);
        else if (_items.data.itemType == ItemType.Potion) ManagePotion(_items, _isAdd);

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

    private void ManageInventory(InventoryItem _equipments, bool _isAdd)
    {
        InventoryItem target = inventory.Find(InventoryItem => InventoryItem.data.itemName == _equipments.data.itemName);

        if (_isAdd)
        {
            if (target != null) target.stackSize += _equipments.stackSize;
            else inventory.Add(_equipments);
        }
        else
        {
            if (target != null)
            {
                if (target.stackSize < _equipments.stackSize) Debug.LogError("Number greater than the actual value");
                if (target.stackSize == _equipments.stackSize) inventory.Remove(target);
                else target.stackSize -= _equipments.stackSize;
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

    private void ManageMaterial(InventoryItem _materials, bool _isAdd)
    {
        InventoryItem target = materials.Find(InventoryItem => InventoryItem.data.itemName == _materials.data.itemName);

        if (_isAdd)
        {
            if (target != null) target.stackSize += _materials.stackSize;
            else materials.Add(_materials);
        }
        else
        {
            if (target != null)
            {
                if (target.stackSize < _materials.stackSize) Debug.LogError("Number greater than the actual value");
                if (target.stackSize == _materials.stackSize) materials.Remove(target);
                else target.stackSize -= _materials.stackSize;
            }
            else Debug.LogError("Not found target item");
        }
    }

    private void ManagePotion(ItemData_Potion _potion, bool _isAdd)
    {
        InventoryItem target = potions.Find(InventoryItem => InventoryItem.data.itemName == _potion.itemName);

        if (_isAdd)
        {
            if (target != null) target.stackSize++;
            else potions.Add(new InventoryItem(_potion));
        }
        else
        {
            if (target != null)
            {
                if (target.stackSize == 1) potions.Remove(target);
                else target.stackSize--;
            }
            else Debug.LogError("Not found target item");
        }
    }

    private void ManagePotion(InventoryItem _potions, bool _isAdd)
    {
        InventoryItem target = potions.Find(InventoryItem => InventoryItem.data.itemName == _potions.data.itemName);

        if (_isAdd)
        {
            if (target != null) target.stackSize += _potions.stackSize;
            else potions.Add(_potions);
        }
        else
        {
            if (target != null)
            {
                if (target.stackSize < _potions.stackSize) Debug.LogError("Number greater than the actual value");
                if (target.stackSize == _potions.stackSize) potions.Remove(target);
                else target.stackSize -= _potions.stackSize;
            }
            else Debug.LogError("Not found target item");
        }
    }

    private void ReloadUI()
    {
        for (int i = 0; i < inventorySlot.Length; i++) inventorySlot[i].CleanUpSlot();
        for (int i = 0; i < materialsSlot.Length; i++) materialsSlot[i].CleanUpSlot();
        for (int i = 0; i < potionsSlot.Length; i++) potionsSlot[i].CleanUpSlot();

        for (int i = 0; i < inventory.Count; i++) inventorySlot[i].UpdateSlot(inventory[i]);
        for (int i = 0; i < materials.Count; i++) materialsSlot[i].UpdateSlot(materials[i]);
        for (int i = 0; i < potions.Count; i++) potionsSlot[i].UpdateSlot(potions[i]);
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

    public void DoEquipmentEffect(EquipmentType _equipmentType)
    {
        UI_EquipmentSlot targetSlot = equipmentSlot.First(UI_EquipmentSlot => UI_EquipmentSlot.slotType == _equipmentType);

        if (targetSlot.item != null && targetSlot.item.stackSize != 0)
        {   
            ItemData_Equipment equipment = (ItemData_Equipment)targetSlot.item.data;
            if (equipment.effect != null)
            {
                foreach (ItemEffect effect in equipment.effect) effect.ExecuteEffect();
            }
        }
    }

    public void UsePotion(ItemData_Potion potion)
    {
        if (PlayerManager.instance.player.potionCD <= 0)
        {
            PlayerManager.instance.player.stats.AddBuff(potion.buff);
            instance.ManageItem(potion, false);
            
            PlayerManager.instance.player.potionCD = potion.potionCD;
        }
    }
}

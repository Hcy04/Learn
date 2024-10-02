using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    public List<InventoryItem> equipment;
    public Dictionary<ItemData_Equipment, InventoryItem> equipmentDictionary;

    public List<InventoryItem> inventory;
    public Dictionary<ItemData, InventoryItem> inventoryDictionary;//物品映射到对应的背包空间

    public List<InventoryItem> stash;
    public Dictionary<ItemData, InventoryItem> stashDictionary;

    [Header("Inventory UI")]
    [SerializeField] private Transform equipmentSlotParent;
    private UI_EquipmentSlot[] equipmentItemSlot;

    [SerializeField] private Transform inventorySlotParent;
    private UI_ItemSlot[] inventoryItemSlot;

    [SerializeField] private Transform stashSlotParent;
    private UI_ItemSlot[] stashItemSlot;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(instance.gameObject);
    }

    private void Start()
    {
        equipment = new List<InventoryItem>();
        equipmentDictionary = new Dictionary<ItemData_Equipment, InventoryItem>();

        inventory = new List<InventoryItem>();
        inventoryDictionary = new Dictionary<ItemData, InventoryItem>();
        
        stash = new List<InventoryItem>();
        stashDictionary = new Dictionary<ItemData, InventoryItem>();

        equipmentItemSlot = equipmentSlotParent.GetComponentsInChildren<UI_EquipmentSlot>();
        inventoryItemSlot = inventorySlotParent.GetComponentsInChildren<UI_ItemSlot>();
        stashItemSlot = stashSlotParent.GetComponentsInChildren<UI_ItemSlot>();
    }

    public void EquipItem(ItemData _item)
    {
        ItemData_Equipment newEquipment = _item as ItemData_Equipment;
        InventoryItem newItem = new InventoryItem(newEquipment);
        //如果对应槽位已经有物品 添加一份到inventory 然后在eqiupment中删除
        foreach (KeyValuePair<ItemData_Equipment, InventoryItem> item in equipmentDictionary)
        {
            if (item.Key.equipment == newEquipment.equipment)
            {
                AddToInventory(item.Value.data);

                equipment.Remove(item.Value);
                equipmentDictionary.Remove(item.Key);

                break;
            }
        }
        //添加装备到equipment 在inventory中删除对应物品
        equipment.Add(newItem);
        equipmentDictionary.Add(newEquipment, newItem);
        RemoveItem(_item);
        //每个装备遍历所有槽位 在对应的UISlot中加载
        foreach (KeyValuePair<ItemData_Equipment, InventoryItem> item in equipmentDictionary)
        {
            for (int i = 0; i < equipmentItemSlot.Length; i++)
            {
                if (item.Key.equipment == equipmentItemSlot[i].slotType)
                {
                    equipmentItemSlot[i].UpdateSlot(item.Value);
                    break;
                }
            }
        }
    }

    #region Add Item
    public void AddItem(ItemData _item)
    {
        if (_item.itemType == ItemType.Equipment) AddToInventory(_item);
        else if (_item.itemType == ItemType.Material) AddToStash(_item);
    }

    private void AddToInventory(ItemData _item)
    {
        if (inventoryDictionary.TryGetValue(_item, out InventoryItem value)) value.AddStack();
        else//如果背包中没有该物品 创建新的背包物品格
        {
            InventoryItem newItem = new InventoryItem(_item);
            inventory.Add(newItem);
            inventoryDictionary.Add(_item, newItem);
        }

        LoadSlots(inventoryItemSlot, inventory);
    }

    private void AddToStash(ItemData _item)
    {
        if (stashDictionary.TryGetValue(_item, out InventoryItem value)) value.AddStack();
        else
        {
            InventoryItem newItem = new InventoryItem(_item);
            stash.Add(newItem);
            stashDictionary.Add(_item, newItem);
        }

        LoadSlots(stashItemSlot, stash);
    }
    #endregion

    #region Remove Item
    public void RemoveItem(ItemData _item)
    {
        if (inventoryDictionary.TryGetValue(_item, out InventoryItem value)) RemoveInInventory(_item, value);
        else if (stashDictionary.TryGetValue(_item, out InventoryItem stashValue)) RemoveInStash(_item, stashValue);
    }

    private void RemoveInInventory(ItemData _item, InventoryItem value)
    {
        if (value.stackSize <= 1)
        {
            inventory.Remove(value);
            inventoryDictionary.Remove(_item);
        }
        else value.RemoveStack();

        LoadSlots(inventoryItemSlot, inventory);
    }

    private void RemoveInStash(ItemData _item, InventoryItem stashValue)
    {
        if (stashValue.stackSize <= 1)
        {
            stash.Remove(stashValue);
            stashDictionary.Remove(_item);
        }
        else stashValue.RemoveStack();

        LoadSlots(stashItemSlot, stash);
    }
    #endregion

    private void LoadSlots<T>(T[] targetSlots, List<InventoryItem> targetSpace) where T :UI_ItemSlot
    {
        for (int i = 0; i < targetSlots.Length; i++) targetSlots[i].CleanUpSlot();
        for (int i = 0; i < targetSpace.Count; i++) targetSlots[i].UpdateSlot(targetSpace[i]);
    }
}

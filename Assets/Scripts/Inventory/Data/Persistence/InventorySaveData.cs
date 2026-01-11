using System;
using System.Collections.Generic;


public interface IInventorySaveData
{
    void SaveInventory(InventorySystem inventory);
    bool LoadInventory(InventorySystem inventory, ItemDatabase database);
}


[Serializable]
public class SlotSaveData
{
    public int slotIndex;
    public int itemId;     
    public int quantity;

    public SlotSaveData(int index, int id, int qty)
    {
        slotIndex = index;
        itemId = id;
        quantity = qty;
    }
}

[Serializable]
public class InventorySaveData : IInventorySaveData
{
    public List<SlotSaveData> slots = new List<SlotSaveData>();
    public string saveDate;
   

 
    public void SaveInventory(InventorySystem inventory)
    {
        slots.Clear();
        saveDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        for (int i = 0; i < inventory.SlotCount; i++)
        {
            InventorySlot slot = inventory.GetSlot(i);
            
            if (!slot.IsEmpty)
            {
                slots.Add(new SlotSaveData(
                    i,
                    slot.itemStack.itemDefinition.id,
                    slot.itemStack.quantity
                ));
            }
        }
    }
    
    public bool LoadInventory(InventorySystem inventory, ItemDatabase database)
    {
        if (inventory == null || database == null)
        {
            UnityEngine.Debug.LogError("[InventorySaveData] Cannot load: null references");
            return false;
        }

        
        inventory.Clear();
        
        foreach (var slotData in slots)
        {
            ItemDefinition item = database.GetItemById(slotData.itemId);
            
            if (item == null)
            {
                UnityEngine.Debug.LogWarning($"[InventorySaveData] Item ID {slotData.itemId} not found in database!");
                continue;
            }

            // Add to specific slot index
            InventorySlot slot = inventory.GetSlot(slotData.slotIndex);
            if (slot != null)
            {
                slot.AddItem(item, slotData.quantity);
            }
        }

        UnityEngine.Debug.Log($"[InventorySaveData] Loaded {slots.Count} items from save dated {saveDate}");
        return true;
    }
}
using UnityEngine;
using System;
using System.Collections.Generic;


public class InventorySystem
{
    private InventorySlot[] slots;
    public int SlotCount => slots.Length;

    
    public event Action<int> OnSlotChanged; 
    public event Action OnInventoryChanged;

    public InventorySystem(int slotCount)
    {
        slots = new InventorySlot[slotCount];
        for (int i = 0; i < slotCount; i++)
        {
            slots[i] = new InventorySlot(i);
        }
    }

   
    public InventorySlot GetSlot(int index)
    {
        if (index < 0 || index >= slots.Length)
        {
            Debug.LogWarning($"[Inventory] Invalid slot index: {index}");
            return null;
        }
        return slots[index];
    }

    
    public bool AddItem(ItemDefinition item, int amount)
    {
        if (item == null || amount <= 0) return false;

        int remaining = amount;

        
        for (int i = 0; i < slots.Length; i++)
        {
            if (!slots[i].IsEmpty && slots[i].itemStack.itemDefinition == item)
            {
                remaining = slots[i].AddItem(item, remaining);
                OnSlotChanged?.Invoke(i);
                
                if (remaining <= 0)
                {
                    OnInventoryChanged?.Invoke();
                    return true;
                }
            }
        }

        
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].IsEmpty)
            {
                remaining = slots[i].AddItem(item, remaining);
                OnSlotChanged?.Invoke(i);
                
                if (remaining <= 0)
                {
                    OnInventoryChanged?.Invoke();
                    return true;
                }
            }
        }

       
        if (remaining > 0)
        {
            Debug.Log($"[Inventory] Added {amount - remaining}/{amount} of {item.itemName}. Inventory full.");
            OnInventoryChanged?.Invoke();
        }

        return remaining < amount; 
    }

 
    public bool RemoveItem(ItemDefinition item, int amount)
    {
        int remaining = amount;

        for (int i = 0; i < slots.Length; i++)
        {
            if (!slots[i].IsEmpty && slots[i].itemStack.itemDefinition == item)
            {
                int inSlot = slots[i].itemStack.quantity;
                int toRemove = Mathf.Min(inSlot, remaining);
                
                slots[i].RemoveItem(toRemove);
                OnSlotChanged?.Invoke(i);
                
                remaining -= toRemove;
                
                if (remaining <= 0)
                {
                    OnInventoryChanged?.Invoke();
                    return true;
                }
            }
        }

        OnInventoryChanged?.Invoke();
        return remaining == 0;
    }

  
    public bool RemoveFromSlot(int slotIndex, int amount)
    {
        if (slotIndex < 0 || slotIndex >= slots.Length) return false;

        bool success = slots[slotIndex].RemoveItem(amount);
        if (success)
        {
            OnSlotChanged?.Invoke(slotIndex);
            OnInventoryChanged?.Invoke();
        }
        return success;
    }
    
    public void ClearSlot(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= slots.Length)
        {
            Debug.LogWarning($"[Inventory] Invalid slot index: {slotIndex}");
            return;
        }

        slots[slotIndex].Clear();
        OnSlotChanged?.Invoke(slotIndex);
        OnInventoryChanged?.Invoke();
    }

   
    public void SwapSlots(int slotA, int slotB)
    {
        if (slotA < 0 || slotA >= slots.Length || slotB < 0 || slotB >= slots.Length)
            return;

        ItemStack tempStack = slots[slotA].GetStackCopy();
        slots[slotA].SetStack(slots[slotB].GetStackCopy());
        slots[slotB].SetStack(tempStack);

        OnSlotChanged?.Invoke(slotA);
        OnSlotChanged?.Invoke(slotB);
        OnInventoryChanged?.Invoke();
    }

 
    public bool HasItem(ItemDefinition item, int amount = 1)
    {
        int count = 0;
        foreach (var slot in slots)
        {
            if (!slot.IsEmpty && slot.itemStack.itemDefinition == item)
            {
                count += slot.itemStack.quantity;
                if (count >= amount) return true;
            }
        }
        return false;
    }
    
    public int GetItemCount(ItemDefinition item)
    {
        int count = 0;
        foreach (var slot in slots)
        {
            if (!slot.IsEmpty && slot.itemStack.itemDefinition == item)
            {
                count += slot.itemStack.quantity;
            }
        }
        return count;
    }

    public void Clear()
    {
        foreach (var slot in slots)
        {
            slot.Clear();
        }
        OnInventoryChanged?.Invoke();
    }
    
    public List<InventorySlot> GetAllItems()
    {
        List<InventorySlot> items = new List<InventorySlot>();
        foreach (var slot in slots)
        {
            if (!slot.IsEmpty)
            {
                items.Add(slot);
            }
        }
        return items;
    }
}
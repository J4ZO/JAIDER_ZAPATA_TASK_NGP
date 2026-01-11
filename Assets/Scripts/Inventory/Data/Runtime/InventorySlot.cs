using System;

[Serializable]
public class InventorySlot
{
    public int slotIndex;
    public ItemStack itemStack;

    public InventorySlot(int index)
    {
        this.slotIndex = index;
        this.itemStack = null;
    }
    
    public bool IsEmpty => itemStack == null || itemStack.IsEmpty();

   
    public bool CanAcceptItem(ItemDefinition item, int amount)
    {
       
        if (IsEmpty) return true;

        
        return itemStack.itemDefinition == item && itemStack.CanAddMore(amount);
    }
    
    public int AddItem(ItemDefinition item, int amount)
    {
        
        if (IsEmpty)
        {
            int amountToAdd = Math.Min(amount, item.maxStackSize);
            itemStack = new ItemStack(item, amountToAdd);
            return amount - amountToAdd;
        }
        
        if (itemStack.itemDefinition == item)
        {
            return itemStack.Add(amount);
        }
        
        return amount;
    }
    
    public bool RemoveItem(int amount)
    {
        if (IsEmpty) return false;

        bool isEmpty = itemStack.Remove(amount);
        if (isEmpty)
        {
            itemStack = null;
        }
        return true;
    }
    
    public void Clear()
    {
        itemStack = null;
    }
    
    public void SetStack(ItemStack stack)
    {
        itemStack = stack;
    }
    
    public ItemStack GetStackCopy()
    {
        return itemStack?.Clone();
    }
}
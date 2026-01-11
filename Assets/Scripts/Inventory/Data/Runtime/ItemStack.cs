using System;

[Serializable]
public class ItemStack
{
    public ItemDefinition itemDefinition;
    public int quantity;

    public ItemStack(ItemDefinition definition, int quantity)
    {
        this.itemDefinition = definition;
        this.quantity = quantity;
    }
    
    public bool CanAddMore(int amount)
    {
        if (itemDefinition == null) return false;
        return quantity + amount <= itemDefinition.maxStackSize;
    }
    
    public int Add(int amount)
    {
        int spaceAvailable = itemDefinition.maxStackSize - quantity;
        int amountToAdd = Math.Min(amount, spaceAvailable);
    
        quantity += amountToAdd;
        return amount - amountToAdd; // Return overflow
    }
    
    public bool Remove(int amount)
    {
        quantity = Math.Max(0, quantity - amount);
        return quantity == 0;
    }

    
    public bool IsEmpty()
    {
        return itemDefinition == null || quantity <= 0;
    }

 
    public ItemStack Clone()
    {
        return new ItemStack(itemDefinition, quantity);
    }
}

using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[CreateAssetMenu(fileName = "ItemDatabase", menuName = "Inventory/Item Database")]
public class ItemDatabase : ScriptableObject
{
    [SerializeField] private List<ItemDefinition> items = new List<ItemDefinition>();
    
    private Dictionary<int, ItemDefinition> itemDictionary;
    
    public void Initialize()
    {
        itemDictionary = items.ToDictionary(item => item.id, item => item);
        
        
        if (itemDictionary.Count != items.Count)
        {
            Debug.LogError("[ItemDatabase] Duplicate item IDs detected! Fix in inspector.");
        }
    }
    
    public ItemDefinition GetItemById(int id)
    {
        if (itemDictionary == null)
            Initialize();

        if (itemDictionary.TryGetValue(id, out ItemDefinition item))
            return item;

        Debug.LogWarning($"[ItemDatabase] Item with ID {id} not found!");
        return null;
    }
    
    public List<ItemDefinition> GetItemsByType(ItemType type)
    {
        return items.Where(item => item.itemType == type).ToList();
    }

#if UNITY_EDITOR
   
    [ContextMenu("Refresh Item List")]
    private void RefreshItemList()
    {
        items = new List<ItemDefinition>(Resources.LoadAll<ItemDefinition>("Items"));
        UnityEditor.EditorUtility.SetDirty(this);
        Debug.Log($"[ItemDatabase] Loaded {items.Count} items");
    }
#endif
}
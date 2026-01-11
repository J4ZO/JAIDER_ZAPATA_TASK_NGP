using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item Definition")]
public class ItemDefinition : ScriptableObject
{
    [Header("Identity")]
    [Tooltip("Unique identifier for this item type")]
    public int id;
    
    [Tooltip("Display name shown in UI")]
    public string itemName;
    
    [Header("Visual")]
    public Sprite icon;
    
    [Header("Classification")]
    public ItemType itemType;
    
    [Header("Stack Settings")]
    [Tooltip("Maximum number that can stack in one slot")]
    public int maxStackSize = 1;
    
    [Header("Description")]
    [TextArea(3, 6)]
    public string description;
    
}

public enum ItemType
{
    None,
    Food,
    Flower,
}

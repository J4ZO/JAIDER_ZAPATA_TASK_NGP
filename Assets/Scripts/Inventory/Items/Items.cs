using System;
using UnityEngine;

public enum ItemType
{
    food,
    tool,
    coin
}
[CreateAssetMenu(fileName = "Items", menuName = "Scriptable Objects/Items")]
public class Items : ScriptableObject
{
    public int id;
    public string itemName;
    public Sprite itemSprite;
    public ItemType itemType;
    public int amount;
    public String itemDescription;
}

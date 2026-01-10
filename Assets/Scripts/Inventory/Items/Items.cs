using System;
using UnityEngine;



public class Items : MonoBehaviour
{
    public int id;
    public string itemName;
    public Sprite itemSprite;
    public ItemType itemType;
    public int amount;
    public String itemDescription;
}

public enum ItemType
{
    None,
    food,
    tool,
    coin
}


using System;
using UnityEngine;
using UnityEngine.Rendering;

public class ObjectInteract : MonoBehaviour, IInteractable
{
    [SerializeField] private Items item;
    [SerializeField] private InventorySlotUI inventorySlotUI;
    
    private SpriteRenderer spriteRenderer;
    private int id;
    private string itemNameObject;
    private ItemType itemTypeObject;
    private int amountObject;
    private String itemDescription;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        id = item.id;
        itemNameObject = item.itemName;
        spriteRenderer.sprite = item.itemSprite;
        itemTypeObject = item.itemType;
        amountObject = item.amount;
        itemDescription = item.itemDescription;
    }
    

    public void Interact()
    {
        Debug.Log("Interact");
        inventorySlotUI.ShowItem(id, itemNameObject, spriteRenderer.sprite, amountObject, itemDescription, itemTypeObject);
        Destroy(gameObject);
    }
    
}

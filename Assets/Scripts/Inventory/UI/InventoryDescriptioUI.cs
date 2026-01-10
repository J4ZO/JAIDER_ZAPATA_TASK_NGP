using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDescriptioUI : MonoBehaviour
{
    public Image inventoryImage;
    public TMP_Text titleText;
    public TMP_Text descriptionText;
    public ItemType type;
    public InventorItemUI inventorItemUI;
    [SerializeField] private Sprite defaultSprite;

    public void ShowInventory(Image image, string title, string description, ItemType typeObject,
        InventorItemUI inventoryItemUI)
    {
        inventoryImage.sprite = image.sprite;
        titleText.text = title;
        descriptionText.text = description;
        type = typeObject;
        inventorItemUI = inventoryItemUI;

        Color color = inventoryImage.color;
        color.a = Mathf.Clamp01(1);
        inventoryImage.color = color;
    }
    
    
}

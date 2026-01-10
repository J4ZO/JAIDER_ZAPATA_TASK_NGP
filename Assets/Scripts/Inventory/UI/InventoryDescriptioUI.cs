using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDescriptioUI : MonoBehaviour
{
    [SerializeField] private Image inventoryImage;
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text descriptionText;

    public void ShowInventory(Image image, string title, string description)
    {
        inventoryImage.sprite = image.sprite;
        titleText.text = title;
        descriptionText.text = description;
    }
}

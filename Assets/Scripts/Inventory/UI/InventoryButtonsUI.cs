using UnityEngine;

public class InventoryButtonsUI : MonoBehaviour
{
    [SerializeField] private InventoryDescriptioUI inventoryDescriptionUI;
    public void DeleteButton()
    {
        DeleteInfo();
        Color color = inventoryDescriptionUI.inventoryImage.color;
        color.a = Mathf.Clamp01(0);
        inventoryDescriptionUI.inventoryImage.color = color;
        // inventoryDescriptionUI.inventoryImage = null;
        
        inventoryDescriptionUI.titleText.text = "";
        inventoryDescriptionUI.descriptionText.text = "";
    }

    public void DeleteInfo()
    {
        inventoryDescriptionUI.inventorItemUI.DeleteData();
    }


    public void UseButton()
    {
        // if (inventoryDescriptionUI.type == ItemType.food)
        // {
        //     DeleteButton();
        //
        //     // Increase Health
        // }
        // else if (inventoryDescriptionUI.type == ItemType.tool)
        // {
        //     DeleteButton();
        //     // Move to main inventory
        // }
    }
}

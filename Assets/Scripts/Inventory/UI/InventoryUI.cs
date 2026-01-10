using System;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private InventorySlotUI inventorySlotUI;
    public bool isInventoryOpen = false;
    
    private void OnEnable()
    {
        inventorySlotUI.InitialializeSlotUI();
    }
    


    private void ToggleInventory()
    {
        isInventoryOpen = !isInventoryOpen;
        gameObject.SetActive(isInventoryOpen);
        
        Time.timeScale = isInventoryOpen ? 0f : 1f;
    }
    
    public void CloseInventory()
    {
        if (isInventoryOpen)
        {
            ToggleInventory();
        }
    }

    public void OpenInventory()
    {
        if (!isInventoryOpen)
        {
            ToggleInventory();
        }
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
public class InventoryUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private Transform slotsContainer;
    [SerializeField] private GameObject slotPrefab;

    [Header("Detail Panel")]
    [SerializeField] private ItemDetailPanel detailPanel;
    
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private HealthPlayer healthPlayer;
    
    private InventorySlotUI[] slotUIs;
    private InventorySystem inventorySystem;
    private int selectedSlotIndex = -1;
    public bool isInventoryOpen => inventoryPanel != null && inventoryPanel.activeSelf;
    public void Initialize(InventorySystem inventory)
    {
        inventorySystem = inventory;

       
        CreateSlotUIs(inventory.SlotCount);

        
        inventorySystem.OnSlotChanged += OnSlotChanged;
        inventorySystem.OnInventoryChanged += RefreshUI;
        
        if (detailPanel != null)
        {
            detailPanel.OnUseItem += HandleUseItem;
            detailPanel.OnDeleteItem += HandleDeleteItem;
        }
        
        RefreshUI();

        
        inventoryPanel.SetActive(false);
    }

    private void OnDestroy()
    {
        
        if (inventorySystem != null)
        {
            inventorySystem.OnSlotChanged -= OnSlotChanged;
            inventorySystem.OnInventoryChanged -= RefreshUI;
        }
        
        if (detailPanel != null)
        {
            detailPanel.OnUseItem -= HandleUseItem;
            detailPanel.OnDeleteItem -= HandleDeleteItem;
        }
    }
    
    private void CreateSlotUIs(int slotCount)
    {
        slotUIs = new InventorySlotUI[slotCount];

        for (int i = 0; i < slotCount; i++)
        {
            GameObject slotObj = Instantiate(slotPrefab, slotsContainer);
            InventorySlotUI slotUI = slotObj.GetComponent<InventorySlotUI>();
            
            slotUI.Initialize(i);
            slotUI.OnSlotClicked += OnSlotClicked;
            slotUI.OnSlotDragSwap += OnSlotDragSwap;

            slotUIs[i] = slotUI;
        }
    }
    
    private void OnSlotChanged(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= slotUIs.Length) return;

        InventorySlot slot = inventorySystem.GetSlot(slotIndex);
        slotUIs[slotIndex].UpdateSlot(slot);
    }

  
    private void RefreshUI()
    {
        for (int i = 0; i < slotUIs.Length; i++)
        {
            OnSlotChanged(i);
        }
    }
    
    private void OnSlotClicked(int slotIndex)
    {
        InventorySlot slot = inventorySystem.GetSlot(slotIndex);

        
        if (selectedSlotIndex == slotIndex)
        {
            DeselectSlot();
            return;
        }

        
        selectedSlotIndex = slotIndex;
        UpdateSlotHighlights();
        
        if (slot != null && !slot.IsEmpty && detailPanel != null)
        {
            detailPanel.ShowItemDetails(slot.itemStack.itemDefinition, slot.itemStack.quantity, selectedSlotIndex);
        }
        
    }
    
    private void DeselectSlot()
    {
        selectedSlotIndex = -1;
        UpdateSlotHighlights();
        
    }
    
    private void UpdateSlotHighlights()
    {
        // for (int i = 0; i < slotUIs.Length; i++)
        // {
        //     slotUIs[i].SetHighlight(i == selectedSlotIndex);
        // }
    }
    
    private void OnSlotDragSwap(int fromIndex, int toIndex)
    {
        inventorySystem.SwapSlots(fromIndex, toIndex);
        DeselectSlot(); 
    }
    
    public void ToggleInventory()
    {
        bool isOpen = !inventoryPanel.activeSelf;
        inventoryPanel.SetActive(isOpen);

        if (!isOpen)
        {
            DeselectSlot();
        }
    }
    
    public void OpenInventory()
    {
        inventoryPanel.SetActive(true);
        RefreshUI();
    }
    
    public void CloseInventory()
    {
        inventoryPanel.SetActive(false);
        DeselectSlot();
    }
    
    private void HandleUseItem(int slotIndex)
    {
        InventorySlot slot = inventorySystem.GetSlot(slotIndex);
        if (slot == null || slot.IsEmpty) return;

        ItemDefinition item = slot.itemStack.itemDefinition;
        
        Debug.Log($"[InventoryUI] Item USED: {item.itemName} (ID: {item.id})");

        switch (item.id)
        {
            case 3: 
                playerMovement.movementSpeedWalking *= 1.5f;
                playerMovement.runSpeed *= 1.5f;
                playerMovement.ReturnSpeed();
                Debug.Log("[Player] Carrot consumed: Speed increased 1.5x");
                break;

            case 4: 
                playerMovement.movementSpeedWalking *= 3f;
                playerMovement.runSpeed *= 3f;
                playerMovement.ReturnSpeed();
                Debug.Log("[Player] Mushroom consumed: Speed increased 3x");
                break;

            case 5: 
                healthPlayer.Health(30f); 
                Debug.Log("[Player] Potion consumed: +30 Health");
                break;

            default:
                Debug.Log("[InventoryUI] No effect applied for this item");
                break;
        }
        
        bool success = inventorySystem.RemoveFromSlot(slotIndex, 1);

        if (success)
        {
           
            if (slot.IsEmpty)
            {
              
                DeselectSlot();
            }
            else
            {
                
                if (detailPanel != null)
                {
                    detailPanel.ShowItemDetails(
                        slot.itemStack.itemDefinition,
                        slot.itemStack.quantity,
                        slotIndex
                    );
                }
            }
        }
        
        detailPanel.ClearDisplay();
    }
    
    private void HandleDeleteItem(int slotIndex)
    {
        InventorySlot slot = inventorySystem.GetSlot(slotIndex);
        if (slot == null || slot.IsEmpty) return;

        ItemDefinition item = slot.itemStack.itemDefinition;
        int quantity = slot.itemStack.quantity;
        
        Debug.Log($"[InventoryUI] Item DELETED: {item.itemName} x{quantity} (ID: {item.id})");

        
        slot.Clear();
        detailPanel.ClearDisplay();
        inventorySystem.ClearSlot(slotIndex);

        
        DeselectSlot();
    }
    
}
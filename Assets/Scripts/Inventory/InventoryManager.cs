using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    [Header("Configuration")]
    [SerializeField] private int inventorySlotCount = 16;

    [Header("Components")]
    [SerializeField] private InventoryUI inventoryUI;
    [SerializeField] private InventorySaveManager saveManager;

    private InventorySystem inventorySystem;

    public InventorySystem Inventory => inventorySystem;

    private void Awake()
    {
        
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        
        InitializeInventory();
    }

    private void Start()
    {
        
        if (saveManager != null)
        {
            saveManager.LoadInventory();
        }
    }

   
    private void InitializeInventory()
    {
       
        inventorySystem = new InventorySystem(inventorySlotCount);

       
        if (inventoryUI != null)
        {
            inventoryUI.Initialize(inventorySystem);
        }
        else
        {
            Debug.LogWarning("[InventoryManager] No InventoryUI assigned!");
        }

        
        if (saveManager != null)
        {
            saveManager.Initialize(inventorySystem);
        }
        else
        {
            Debug.LogWarning("[InventoryManager] No InventorySaveManager assigned!");
        }

        Debug.Log($"[InventoryManager] Initialized with {inventorySlotCount} slots");
    }
    
    
    public bool AddItem(ItemDefinition item, int quantity = 1)
    {
        return inventorySystem.AddItem(item, quantity);
    }
    
    public bool RemoveItem(ItemDefinition item, int quantity = 1)
    {
        return inventorySystem.RemoveItem(item, quantity);
    }
    
    public bool HasItem(ItemDefinition item, int quantity = 1)
    {
        return inventorySystem.HasItem(item, quantity);
    }
    
    public int GetItemCount(ItemDefinition item)
    {
        return inventorySystem.GetItemCount(item);
    }

 
    public void OpenInventory()
    {
        if (inventoryUI != null)
        {
            inventoryUI.OpenInventory();
        }
    }

 
    public void CloseInventory()
    {
        if (inventoryUI != null)
        {
            inventoryUI.CloseInventory();
        }
    }

  
    public void SaveInventory()
    {
        if (saveManager != null)
        {
            saveManager.SaveInventory();
        }
    }

  
    public void LoadInventory()
    {
        if (saveManager != null)
        {
            saveManager.LoadInventory();
        }
    }

   
    public void ClearInventory()
    {
        inventorySystem.Clear();
    }
}
using UnityEngine;
using UnityEngine;
using System.IO;


public class InventorySaveManager : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private ItemDatabase itemDatabase;

    [Header("Settings")]
    [SerializeField] private string saveFileName = "inventory_save.json";
    [SerializeField] private bool autoSaveOnQuit = true;

    private string SavePath => Path.Combine(Application.persistentDataPath, saveFileName);

    private InventorySystem inventorySystem;

    public void Initialize(InventorySystem inventory)
    {
        inventorySystem = inventory;
        
        if (itemDatabase != null)
        {
            itemDatabase.Initialize();
        }
        else
        {
            Debug.LogError("[InventorySaveManager] ItemDatabase not assigned!");
        }
    }
    
    public void SaveInventory()
    {
        if (inventorySystem == null)
        {
            Debug.LogError("[InventorySaveManager] Inventory system not initialized!");
            return;
        }

        try
        {
            InventorySaveData saveData = new InventorySaveData();
            saveData.SaveInventory(inventorySystem);

            string json = JsonUtility.ToJson(saveData, true);
            File.WriteAllText(SavePath, json);

            Debug.Log($"[InventorySaveManager] Inventory saved to: {SavePath}");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"[InventorySaveManager] Save failed: {e.Message}");
        }
    }
    
    public bool LoadInventory()
    {
        if (inventorySystem == null)
        {
            Debug.LogError("[InventorySaveManager] Inventory system not initialized!");
            return false;
        }

        if (!File.Exists(SavePath))
        {
            Debug.Log("[InventorySaveManager] No save file found. Starting with empty inventory.");
            return false;
        }

        try
        {
            string json = File.ReadAllText(SavePath);
            InventorySaveData saveData = JsonUtility.FromJson<InventorySaveData>(json);

            bool success = saveData.LoadInventory(inventorySystem, itemDatabase);
            
            if (success)
            {
                Debug.Log($"[InventorySaveManager] Inventory loaded from: {SavePath}");
            }
            
            return success;
        }
        catch (System.Exception e)
        {
            Debug.LogError($"[InventorySaveManager] Load failed: {e.Message}");
            return false;
        }
    }
    
    public void DeleteSave()
    {
        if (File.Exists(SavePath))
        {
            File.Delete(SavePath);
            Debug.Log("[InventorySaveManager] Save file deleted.");
        }
    }
    
    public bool SaveExists()
    {
        return File.Exists(SavePath);
    }

    private void OnApplicationQuit()
    {
        if (autoSaveOnQuit && inventorySystem != null)
        {
            SaveInventory();
        }
    }
    
}

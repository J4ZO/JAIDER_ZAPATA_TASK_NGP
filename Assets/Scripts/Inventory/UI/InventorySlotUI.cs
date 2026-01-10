using UnityEngine;
using System.Collections.Generic;
using Unity.Mathematics;

public class InventorySlotUI : MonoBehaviour
{
    [SerializeField] private GameObject inventoryPrefab;
    [SerializeField] private RectTransform inventorySlotContainer;
    
    public List<InventorItemUI> listInventorySlotUI = new List<InventorItemUI>();

    public void InitialializeSlotUI()
    {
        if (listInventorySlotUI == null) return;
        foreach (InventorItemUI itemUI in listInventorySlotUI)
        {
            bool hasSprite = itemUI.imageChild != null && 
                             itemUI.imageChild.sprite != null;
            itemUI.gameObject.SetActive(hasSprite);
        }
    }

    public void ShowItem(int id, string nameObject, Sprite sprite, int amount, string description )
    {
        if (listInventorySlotUI == null) return;
        foreach (InventorItemUI itemUI in listInventorySlotUI)
        {
            if (itemUI.imageChild == null || itemUI.imageChild.sprite == null)
            {
                Debug.Log($"Added at: {itemUI.gameObject.name}");
                itemUI.InsertData(id, nameObject, sprite, amount, description);
                return;
            }
            return;
        }
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

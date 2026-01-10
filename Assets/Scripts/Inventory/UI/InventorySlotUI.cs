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
        for (int i = 3; i < listInventorySlotUI.Count; i ++)
        {
            InventorItemUI itemUI = listInventorySlotUI[i];
            bool hasSprite = itemUI.imageChild != null && 
                             itemUI.imageChild.sprite != null;
            itemUI.gameObject.SetActive(hasSprite);
            if(!hasSprite)
            {
                Color color = itemUI.imageChild.color;
                color.a = Mathf.Clamp01(0);
                itemUI.imageChild.color = color;
            }
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
                Color color = itemUI.imageChild.color;
                color.a = Mathf.Clamp01(1);
                itemUI.imageChild.color = color;
                itemUI.InsertData(id, nameObject, sprite, amount, description);
                return;
            }
        }
    }
    
}

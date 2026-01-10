using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryDrop : MonoBehaviour, IDropHandler
{
    [SerializeField] private InventorItemUI itemUI;

    private void Awake()
    {
        if (itemUI == null)
            itemUI = GetComponent<InventorItemUI>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Drop");
        InventorItemUI draggedItem = InventoryDrag.CurrentDraggedItem;
        
        if (draggedItem == null) return;
       
        if (draggedItem != itemUI) return;
        Debug.Log("Drop item");
        
    }
    
    
}

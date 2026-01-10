using System;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class InventoryDrag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerClickHandler
{
    private RectTransform rectTransform;
    [SerializeField] Canvas canvas;
    public Transform originalParent;
    [SerializeField] private Transform dragLayer;
    [SerializeField] private InventorItemUI inventoryItemUI;
    [SerializeField] private InventoryDescriptioUI descriptionUI;
    private CanvasGroup canvasGroup;
    
    public static InventorItemUI CurrentDraggedItem { get; private set; }
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnDrag(PointerEventData eventData)
    { 
       rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnBeginDrag(PointerEventData eventData)
    { 
        CurrentDraggedItem = inventoryItemUI;
        
        if (inventoryItemUI == null || !inventoryItemUI.HasItem)
        {
            eventData.pointerDrag = null; 
            return;
        }
        
        originalParent = transform.parent;
        transform.SetParent(dragLayer, true);
        transform.SetAsLastSibling();
        
        canvasGroup.blocksRaycasts = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
    
        InventorItemUI targetSlot = null;
    
        foreach (RaycastResult result in results)
        {
            InventorItemUI slot = result.gameObject.GetComponentInParent<InventorItemUI>();
    
            if (slot != inventoryItemUI)
            {
                targetSlot = slot;
                break;
            }
        }
    
        if (targetSlot != null)
        {
            Debug.Log(targetSlot.name);
        }
        
        canvasGroup.blocksRaycasts = true;
        
        if (targetSlot != null)
        {
            InventoryDrop targetDrop = targetSlot.GetComponentInChildren<InventoryDrop>();
            
            if (targetDrop != null)
            {
                Transform targetOriginalParent = targetDrop.transform.parent;
                
                targetDrop.transform.SetParent(originalParent);
                targetDrop.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                
                transform.SetParent(targetOriginalParent);
                rectTransform.anchoredPosition = Vector2.zero;
            }
            else
            {
                
                transform.SetParent(targetSlot.transform);
                rectTransform.anchoredPosition = Vector2.zero;
            }
        }
        else
        {
            transform.SetParent(originalParent);
            rectTransform.anchoredPosition = Vector2.zero;
        }

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (inventoryItemUI != null && inventoryItemUI.HasItem)
        {
            Debug.Log($"Item seleccionado: {inventoryItemUI.name}");
            
            descriptionUI.ShowInventory(inventoryItemUI.imageChild, inventoryItemUI.nameObject, inventoryItemUI.descriptionObject);
          
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                Debug.Log("Click izquierdo - Seleccionar item");
            }
            else if (eventData.button == PointerEventData.InputButton.Right)
            {
                Debug.Log("Click derecho - Abrir menú");
            }
        }
    }
}

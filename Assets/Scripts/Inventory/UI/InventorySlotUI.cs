using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InventorySlotUI : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    [Header("UI References")]
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI quantityText;
    // [SerializeField] private GameObject highlightBorder;

    [Header("Settings")]
    [SerializeField] private Color emptyIconColor = new Color(1, 1, 1, 0);
    [SerializeField] private Color filledIconColor = new Color(1, 1, 1, 1);

    private int slotIndex;
    private InventorySlot slotData;

    // Events for communication with InventoryUI
    public System.Action<int> OnSlotClicked;
    public System.Action<int, int> OnSlotDragSwap; // fromIndex, toIndex

    public void Initialize(int index)
    {
        slotIndex = index;
        ClearSlot();
    }

   
    public void UpdateSlot(InventorySlot slot)
    {
        slotData = slot;

        if (slot == null || slot.IsEmpty)
        {
            ClearSlot();
            return;
        }

        
        itemIcon.sprite = slot.itemStack.itemDefinition.icon;
        itemIcon.color = filledIconColor;

        
        if (slot.itemStack.quantity > 1)
        {
            quantityText.text = slot.itemStack.quantity.ToString();
            quantityText.gameObject.SetActive(true);
        }
        else
        {
            quantityText.gameObject.SetActive(false);
        }
    }

   
    private void ClearSlot()
    {
        itemIcon.sprite = null;
        itemIcon.color = emptyIconColor;
        quantityText.gameObject.SetActive(false);
        slotData = null;
    }

    
    // public void SetHighlight(bool active)
    // {
    //     if (highlightBorder != null)
    //     {
    //         highlightBorder.SetActive(active);
    //     }
    // }

    // --- Input Handlers ---

    public void OnPointerClick(PointerEventData eventData)
    {
        OnSlotClicked?.Invoke(slotIndex);
    }

    private GameObject draggedIcon;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (slotData == null || slotData.IsEmpty) return;

       
        draggedIcon = new GameObject("DragIcon");
        draggedIcon.transform.SetParent(transform.root);
        
        Image dragImage = draggedIcon.AddComponent<Image>();
        dragImage.sprite = slotData.itemStack.itemDefinition.icon;
        dragImage.raycastTarget = false;

        RectTransform rt = draggedIcon.GetComponent<RectTransform>();
        // rt.sizeDelta = new Vector2(50, 50);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (draggedIcon != null)
        {
            draggedIcon.transform.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (draggedIcon != null)
        {
            Destroy(draggedIcon);
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        InventorySlotUI draggedSlot = eventData.pointerDrag?.GetComponent<InventorySlotUI>();
        
        if (draggedSlot != null && draggedSlot != this)
        {
            OnSlotDragSwap?.Invoke(draggedSlot.slotIndex, this.slotIndex);
        }
    }
}
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemDetailPanel : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private CanvasGroup panelCanvasGroup;
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemNameText;
    // [SerializeField] private TextMeshProUGUI itemTypeText;
    [SerializeField] private TextMeshProUGUI itemDescriptionText;
    [SerializeField] private TextMeshProUGUI quantityText;
    
    [Header("Action Buttons")]
    [SerializeField] private Button useButton;
    [SerializeField] private Button deleteButton;

    [Header("Visual Settings")]
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private Color emptyIconColor = new Color(1, 1, 1, 0);
    [SerializeField] private Color filledIconColor = new Color(1, 1, 1, 1);

    private ItemDefinition currentItem;
    // private int currentQuantity;
    private int currentSlotIndex = -1;
    
    public System.Action<int> OnUseItem;
    public System.Action<int> OnDeleteItem;
    

    private void Start()
    {
        if (useButton != null)
        {
            useButton.onClick.AddListener(OnUseButtonClicked);
        }

        if (deleteButton != null)
        {
            deleteButton.onClick.AddListener(OnDeleteButtonClicked);
        }

        ClearDisplay();
    }
    
    public void ShowItemDetails(ItemDefinition item, int quantity, int slotIndex = -1)
    {
       

        currentItem = item;
        // currentQuantity = quantity;
        currentSlotIndex = slotIndex;

        
        if (panelCanvasGroup != null)
        {
            panelCanvasGroup.alpha = 1f;
            panelCanvasGroup.interactable = true;
            panelCanvasGroup.blocksRaycasts = true;
        }
        else
        {
            gameObject.SetActive(true);
        }

        
        if (itemIcon != null)
        {
            itemIcon.sprite = item.icon != null ? item.icon : defaultSprite;
            itemIcon.color = filledIconColor;
        }

      
        if (itemNameText != null)
        {
            itemNameText.text = item.itemName;
        }

        // if (itemTypeText != null)
        // {
        //     itemTypeText.text = item.itemType.ToString();
        // }

        if (itemDescriptionText != null)
        {
            itemDescriptionText.text = item.description;
        }

        // if (quantityText != null)
        // {
        //     quantityText.text = $"Quantity: {quantity}";
        // }
        
        if (useButton != null)
        {
            useButton.interactable = true;
        }

        if (deleteButton != null)
        {
            deleteButton.interactable = true;
        }
    }
    
    public void ClearDisplay()
    {
        // Reset icon
        if (itemIcon != null)
        {
            itemIcon.sprite = defaultSprite;
            itemIcon.color = emptyIconColor;
        }

        // Clear texts
        if (itemNameText != null)
        {
            itemNameText.text = "No Item Selected";
        }
        

        if (itemDescriptionText != null)
        {
            itemDescriptionText.text = "Select an item to view details";
        }
        
        
        if (useButton != null)
        {
            useButton.interactable = false;
        }

        if (deleteButton != null)
        {
            deleteButton.interactable = false;
        }

        currentItem = null;
        currentSlotIndex = -1;
    }
    
    private void OnUseButtonClicked()
    {
        if (currentSlotIndex < 0 || currentItem == null) return;

        Debug.Log($"[ItemDetailPanel] USE button clicked for: {currentItem.itemName}");
        OnUseItem?.Invoke(currentSlotIndex);
    }
    
    private void OnDeleteButtonClicked()
    {
        Debug.Log(currentSlotIndex);
        if (currentSlotIndex < 0 || currentItem == null) return;

        Debug.Log($"[ItemDetailPanel] DELETE button clicked for: {currentItem.itemName}");
        OnDeleteItem?.Invoke(currentSlotIndex);
    }
    
}
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Collider2D))]
public class WorldItem : MonoBehaviour
{
    [Header("Item Configuration")]
    [SerializeField] private ItemDefinition itemDefinition;
    [SerializeField] private int quantity = 1;

    [Header("Interaction")]
    [SerializeField] private float interactionRange = 2f;
    [SerializeField] private InputActionReference pickUpAction;
    [SerializeField] private GameObject interactionPrompt;

    private Transform player;
    private bool isInRange;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (interactionPrompt != null)
        {
            interactionPrompt.SetActive(false);
        }
    }

    private void Update()
    {
        if (player == null) return;

        
        float distance = Vector3.Distance(transform.position, player.position);
        isInRange = distance <= interactionRange;
        
        if (interactionPrompt != null)
        {
            interactionPrompt.SetActive(isInRange);
        }

        
        if (isInRange && pickUpAction.action.WasPressedThisFrame())
        {
            TryPickup();
        }
    }

  
    private void TryPickup()
    {
        InventoryManager inventoryManager = InventoryManager.Instance;
        
        if (inventoryManager == null)
        {
            Debug.LogError("[WorldItem] No InventoryManager found in scene!");
            return;
        }
        
        bool success = inventoryManager.AddItem(itemDefinition, quantity);

        if (success)
        {
            Debug.Log($"[WorldItem] Picked up {quantity}x {itemDefinition.itemName}");
            
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("[WorldItem] Inventory full!");
            
        }
    }
    
    
    public void SetItem(ItemDefinition item, int qty = 1)
    {
        itemDefinition = item;
        quantity = qty;
        
    }
}


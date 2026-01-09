using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerInteractor playerInteract;
    [SerializeField] private InventoryUI inventoryUI;
    
    public InputActionReference moveAction;
    public InputActionReference pickUpAction;
    public InputActionReference openInventoryAction;
    
    void Update()
    {
        Vector3 moveDirection = moveAction.action.ReadValue<Vector2>().normalized;

        playerMovement.Move(moveDirection);

        if (pickUpAction.action.WasPressedThisFrame()) playerInteract.TryInteract();
        
        if(inventoryUI.isInventoryOpen && openInventoryAction.action.WasPressedThisFrame())
        {
            inventoryUI.CloseInventory();
        }else if(!inventoryUI.isInventoryOpen && openInventoryAction.action.WasPressedThisFrame())
        {
            inventoryUI.OpenInventory();
        }
        
    }
}

using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerInteractor playerInteract;
    [SerializeField] private InventoryUI inventoryUI;
    [SerializeField] private PlayerAnimation playerAnimation;
    [SerializeField] private HealthPlayer healthPlayer;
    [SerializeField] private DialogueUI dialogueUI;
    [SerializeField] private PauseUI pauseUI;
    
    public InputActionReference moveAction;
    public InputActionReference pickUpAction;
    public InputActionReference talkAction;
    public InputActionReference openInventoryAction;
    public InputActionReference runAction;
    public InputActionReference openPauseAction;
    
    private bool isDead = false;
    
    void Update()
    {
        if (isDead) return;
        
        // <----------------- Movement ------------------------>
        Vector3 moveDirection = moveAction.action.ReadValue<Vector2>().normalized;
        bool isRunning = runAction.action.IsPressed();
        
        float currentSpeed = isRunning ? playerMovement.runSpeed : playerMovement.movementSpeedWalking;
        
        playerMovement.Move(moveDirection);
        
        float speedValue = moveDirection.magnitude * currentSpeed;
        playerAnimation.MovementAnimation(speedValue);
        
        // Flip Player
        if (moveDirection.x > 0)
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
        }else if (moveDirection.x < 0)
        {
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }
        
        // // <----------------- Pick up ------------------------>
        // if (pickUpAction.action.WasPressedThisFrame()) playerInteract.TryInteract();
        // <----------------- NPC Dialogue Toggle ------------------------>
        if(dialogueUI.isDialogueOpen && talkAction.action.WasPressedThisFrame())
        {
            dialogueUI.CloseDialogue();
        }else if(!dialogueUI.isDialogueOpen && talkAction.action.WasPressedThisFrame())
        {
            playerInteract.TryNpc();
            dialogueUI.OpenDialogue();
        }
        
        
        // <----------------- Inventory Toggle ------------------------>
        if(inventoryUI.isInventoryOpen && openInventoryAction.action.WasPressedThisFrame())
        {
            inventoryUI.CloseInventory();
        }else if(!inventoryUI.isInventoryOpen && openInventoryAction.action.WasPressedThisFrame())
        {
            inventoryUI.OpenInventory();
        }
        
        // <----------------- Pause Toggle ------------------------>
        if(pauseUI.isPaused && openPauseAction.action.WasPressedThisFrame())
        {
            pauseUI.ClosePauseUI();
        }else if(!pauseUI.isPaused && openPauseAction.action.WasPressedThisFrame())
        {
            pauseUI.OpenPauseUI();
        }
        
    }
    
}

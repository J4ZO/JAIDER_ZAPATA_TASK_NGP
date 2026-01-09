using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerInteractor playerInteract;
    
    public InputActionReference moveAction;
    public InputActionReference pickUpAction;
    
    void Update()
    {
        Vector3 moveDirection = moveAction.action.ReadValue<Vector2>().normalized;

        playerMovement.Move(moveDirection);

        if (pickUpAction.action.WasPressedThisFrame()) playerInteract.TryInteract();
        
    }
}

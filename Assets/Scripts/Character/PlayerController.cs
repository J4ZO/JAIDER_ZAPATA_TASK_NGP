using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerInteractor playerInteract;
    
    public InputActionReference moveAction;
    public InputActionReference pickUpAction;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveDirection = moveAction.action.ReadValue<Vector2>().normalized;

        playerMovement.Move(moveDirection);

        if (pickUpAction.action.WasPressedThisFrame())
        {
            Debug.Log("Pick up");
            playerInteract.TryInteract();
        }
    }
}

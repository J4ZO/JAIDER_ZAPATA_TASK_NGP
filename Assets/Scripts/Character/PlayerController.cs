using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    
    [SerializeField] private PlayerMovement playerMovement;
    
    public InputActionReference moveAction;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveDirection = moveAction.action.ReadValue<Vector2>().normalized;
        
        playerMovement.Move(moveDirection);
    }
}

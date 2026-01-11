using UnityEngine;

public class MovementGoblin : MonoBehaviour
{
    public float movementSpeedWalking = 5f;
    public float runSpeed = 10f;

    private Rigidbody2D rb;
    private Vector2 movementDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    public void Move(Vector3 targetPosition)
    {
        movementDirection = new Vector2(targetPosition.x, targetPosition.y).normalized;
        
        UpdateFacing(movementDirection.x);
    }

    public void Stop()
    {
        movementDirection = Vector2.zero;
    }
    private void FixedUpdate()
    {
        if (rb != null)
        {
            rb.linearVelocity = movementDirection * movementSpeedWalking;
        }
    }
    
    private void UpdateFacing(float xDirection)
    {
        if (xDirection == 0) return;

        Vector3 scale = transform.localScale;
        scale.x = xDirection < 0 ? -1f : 1f;
        transform.localScale = scale;
    }
}
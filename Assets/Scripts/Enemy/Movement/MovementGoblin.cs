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

    public void Move(Vector3 direction)
    {
        movementDirection = new Vector2(direction.x, direction.y).normalized;
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
}
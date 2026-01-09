using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
   [SerializeField] private float movementSpeed = 5f;

    public void Move(Vector3 direction)
    {
        transform.position += direction * (Time.deltaTime * movementSpeed);
        Debug.Log("Moving");
    }
}

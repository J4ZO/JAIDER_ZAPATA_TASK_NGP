using UnityEngine;

public class ObjectInteract : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log("Interact");
        Destroy(gameObject);
    }
}

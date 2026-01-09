using System;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
   [SerializeField] private float radiusCollider;
   [SerializeField] private LayerMask layerMask;


   public void TryInteract()
   {
      Debug.Log("I Interact");
      Collider2D hitObject = Physics2D.OverlapCircle(transform.position, radiusCollider, layerMask);
      
      
      if (hitObject == null) return;
      
      Debug.Log("Pressed");
      IInteractable interactable = hitObject.GetComponent<IInteractable>();
      interactable?.Interact();
   }

   private void OnDrawGizmos()
   {
      Gizmos.color = Color.orange;
      Gizmos.DrawWireSphere(transform.position, radiusCollider);
   }
}

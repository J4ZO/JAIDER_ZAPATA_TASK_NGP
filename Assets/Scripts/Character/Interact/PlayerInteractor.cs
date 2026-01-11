using System;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
   [SerializeField] private float radiusCollider;
   [SerializeField] private LayerMask layerMask;


   public void TryInteract()
   {
      Collider2D hitObject = Physics2D.OverlapCircle(transform.position, radiusCollider, layerMask);

      if (hitObject == null) return;
      
      IInteractable interactable = hitObject.GetComponent<IInteractable>();
      interactable?.Interact();
     Debug.Log("Interacted" + hitObject.name);
   }

   public void TryNpc()
   {
      Collider2D hitObject = Physics2D.OverlapCircle(transform.position, radiusCollider, layerMask);

      if (hitObject == null) return;
      
      IINPC interactable = hitObject.GetComponent<IINPC>();
      interactable?.Show();
      Debug.Log("NPC" + hitObject.name);
   }

   private void OnDrawGizmos()
   {
      Gizmos.color = Color.orange;
      Gizmos.DrawWireSphere(transform.position, radiusCollider);
   }

   
   
}

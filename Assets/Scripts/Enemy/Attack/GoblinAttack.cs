using System;
using UnityEngine;

public class GoblinAttack : MonoBehaviour
{
    [SerializeField] private HealthPlayer healthPlayer;
    public bool isAttacking;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isAttacking = true;
            Debug.Log("Attacking");
            healthPlayer.TakeDamage(30);
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isAttacking = false;
        }
    }
    
}

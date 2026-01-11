using System;
using UnityEngine;

public class HealthPlayer : MonoBehaviour
{
    [SerializeField] private PlayerAnimation playerAnimation;
    [SerializeField] private float health;
    private bool isDead;
    
    public void TakeDamage(float damage)
    {
        if (isDead) return;
        health -= damage;
        playerAnimation.HurtingAnimation();
    }
    public void Health(float amount)
    {
        if (isDead) return;
        health += amount;
    }
    
    
    public void Die()
    {
        isDead = true;
        playerAnimation.DeathAnimation();
    }
}

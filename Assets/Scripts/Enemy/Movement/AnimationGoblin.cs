using System.Collections;
using UnityEngine;

public class AnimationGoblin : MonoBehaviour
{
    private Animator animator;
    
    private static readonly int IsWalking = Animator.StringToHash("isWalking");
    private static readonly int IsRunning = Animator.StringToHash("isRunning");
    private static readonly int Attack = Animator.StringToHash("isAttacking");
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void WalkingAnimation()
    {
        animator.SetBool(IsWalking, true);
        animator.SetBool(IsRunning, false);
    }
    public void RunningAnimation()
    {
        animator.SetBool(IsRunning, true);
        animator.SetBool(IsWalking, false);
    }
    
    public void AttackAnimation()
    {
        animator.SetTrigger(Attack);
            
    }

  
    public void Stop()
    {
        animator.SetBool(IsWalking, false);
        animator.SetBool(IsRunning, false);
    }
    
}

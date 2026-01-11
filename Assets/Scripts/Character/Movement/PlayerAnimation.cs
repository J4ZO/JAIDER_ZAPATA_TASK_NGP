using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    
    private static readonly int IsWalking = Animator.StringToHash("isWalking");
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int Hurt = Animator.StringToHash("Hurt");
    private static readonly int IsDead = Animator.StringToHash("IsDead");
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void MovementAnimation(float speed)
    {
        bool isMoving = speed > 0.1f;
        animator.SetBool(IsWalking, isMoving);
        animator.SetFloat(Speed, speed);
    }

    public void HurtingAnimation()
    {
        animator.SetTrigger(Hurt);
    }
    public void AttackAnimation()
    {
        animator.SetTrigger(Attack);
    }

    public void DeathAnimation()
    {
        animator.SetBool(IsDead,true);
    }
}

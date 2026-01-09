using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Collider attackCollider;

    public void Attack()
    {
        Debug.Log("Attack");
    }
}

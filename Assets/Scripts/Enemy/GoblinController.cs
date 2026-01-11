using System.Collections;
using UnityEngine;

public class GoblinController : MonoBehaviour
{
    [SerializeField] private MovementGoblin movementGoblin;
    [SerializeField] private PatrolGoblin patrolGoblin;
    [SerializeField] private GoblinVision goblinVision;
    [SerializeField] private GoblinAttack goblinAttack;
    
    private Transform playerTransform;
    
    private enum GoblinState
    {
        Patrolling,
        Chasing,
        Attacking
    }
    
    private GoblinState currentState = GoblinState.Patrolling;
    
    
    private void Update()
    {
        switch (currentState)
        {
            case GoblinState.Patrolling:
                UpdateVision();
                break;
                
            case GoblinState.Chasing:
                UpdateChasing();
                break;
                
            case GoblinState.Attacking:
                UpdateAttacking();
                break;
        }
    }

    private void UpdateVision()
    {
        if (goblinVision.detectedPlayer)
        {
            Debug.Log("Goblin Chasing");
            playerTransform = goblinVision.DetectedPlayer;
            patrolGoblin.StopPatrol();
            currentState = GoblinState.Chasing;
        }


        if (goblinAttack.isAttacking)
        {
            currentState = GoblinState.Attacking;
        }
    }

    private void UpdateChasing()
    {
        if (!goblinVision.detectedPlayer) StartCoroutine(WaitToReturnPatroll());
        
        Vector2 direction = playerTransform.position - transform.position;

        movementGoblin.Move(direction);
        
        if (goblinAttack.isAttacking)
        {
            currentState = GoblinState.Attacking;
        }
    }

    private IEnumerator WaitToReturnPatroll()
    {
        yield return new WaitForSeconds(4f);
        currentState = GoblinState.Patrolling;
        patrolGoblin.ResumePatrol();
    }

    public void UpdateAttacking()
    {
        movementGoblin.Stop();
        if (!goblinAttack.isAttacking) StartCoroutine(WaitToReturnChase());
    }
    
    private IEnumerator WaitToReturnChase()
    {
        yield return new WaitForSeconds(4f);
        Debug.Log("Chasing again");
        currentState = GoblinState.Chasing;
        patrolGoblin.ResumePatrol();
    }
    


}
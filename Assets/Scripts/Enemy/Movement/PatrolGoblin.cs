using System.Collections;
using UnityEngine;

public class PatrolGoblin : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float waitTimePatrol = 1f;
    [SerializeField] private float minimunDistance = 0.1f;
    
    [SerializeField] private MovementGoblin movementGoblin;
    
    private int currentWaypoint = 0;
    private bool isWaiting = false;
    
    private void Awake()
    {
        if (movementGoblin == null)
            movementGoblin = GetComponent<MovementGoblin>();
    }
    
    private void Update()
    {
        if (!isWaiting && waypoints.Length > 0)
        {
            Patrolling();
        }
    }

    private void Patrolling()
    {
        Vector2 targetPosition = waypoints[currentWaypoint].position;
        Vector2 currentPosition = transform.position;
        
        
        float distance = Vector2.Distance(currentPosition, targetPosition);
        
        if (distance > minimunDistance)
        {
           
            Vector3 direction = (targetPosition - currentPosition).normalized;
            
            
            movementGoblin.Move(direction);
        }
        else
        {
            movementGoblin.Move(Vector3.zero);
            StartCoroutine(WaitInWaypoint());
        }
    }

    private IEnumerator WaitInWaypoint()
    {
        isWaiting = true;
        
        yield return new WaitForSeconds(waitTimePatrol);
        
        
        currentWaypoint++;
        
        if (currentWaypoint >= waypoints.Length)
        {
            currentWaypoint = 0;
        }
        
        isWaiting = false;
    }
    
    
    public void StopPatrol()
    {
        isWaiting = true;
        movementGoblin.Move(Vector3.zero);
    }
    
   
    public void ResumePatrol()
    {
        currentWaypoint = 0;
        isWaiting = false;
    }
}
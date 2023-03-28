using UnityEngine;
using UnityEngine.AI;

public class NPCbehavior : MonoBehaviour
{
    private NavMeshAgent agent;
    private Vector3 targetPosition;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        SetRandomTargetPosition();
    }

    void Update()
    {
        if (agent.remainingDistance < 0.5f)
        {
            SetRandomTargetPosition();
        }
        OnAgentMove();
    }

    private void SetRandomTargetPosition()
    {
        Vector3 randomDirection = Random.insideUnitSphere * 10;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, 10, NavMesh.AllAreas);
        targetPosition = hit.position;
        agent.SetDestination(targetPosition);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(targetPosition, 0.3f);
    }




    private void OnAgentMove()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, agent.desiredVelocity.normalized, out hit, agent.radius * 2))
        {
            agent.SetDestination(hit.point + hit.normal * agent.radius * 2);
        }
    }
}

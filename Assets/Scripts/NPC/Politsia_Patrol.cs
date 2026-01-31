using System.Xml.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Politsia_Patrol : MonoBehaviour
{
    public Transform[] patrolPoints;
    private int patrolIndex;
    private NavMeshAgent agent;
    void Start()
    {
        patrolIndex = 0;
        agent = GetComponent<NavMeshAgent>();
        if (patrolPoints.Length > 0 && agent != null)
        {
            agent.SetDestination(patrolPoints[patrolIndex].transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (patrolPoints.Length == 0 || agent == null)
            return;
        if (!agent.pathPending && agent.remainingDistance < 1f)
        {
            patrolIndex = (patrolIndex + 1) % patrolPoints.Length;
            agent.SetDestination(patrolPoints[patrolIndex].transform.position);
        }
        DetectPlayer();
    }

    [Header("Vision")]
    public float viewRadius = 10f;
    [Range(0, 360)] public float viewAngle = 90f;
    public LayerMask playerLayer;
    public LayerMask obstacleLayer;

    void DetectPlayer()
    {
        // 1️⃣ Detectar Player dentro del radio
        Collider[] hits = Physics.OverlapSphere(transform.position, viewRadius, playerLayer);

        foreach (Collider hit in hits)
        {
            if (!hit.CompareTag("Player")) continue;

            Vector3 dirToPlayer = (hit.transform.position - transform.position).normalized;

            // 2️⃣ Comprobar ángulo de visión
            float angle = Vector3.Angle(transform.forward, dirToPlayer);

            if (angle < viewAngle / 2f)
            {
                // 3️⃣ Comprobar obstáculos
                float distanceToPlayer = Vector3.Distance(transform.position, hit.transform.position);

                if (!Physics.Raycast(transform.position,dirToPlayer,distanceToPlayer,obstacleLayer))
                {
                    // 🎯 Player detectado
                    Debug.Log("Player visto");
                }
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, viewRadius);

        Vector3 left = Quaternion.Euler(0, -viewAngle / 2, 0) * transform.forward;
        Vector3 right = Quaternion.Euler(0, viewAngle / 2, 0) * transform.forward;

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + left * viewRadius);
        Gizmos.DrawLine(transform.position, transform.position + right * viewRadius);
    }
}

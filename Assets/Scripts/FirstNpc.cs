using UnityEngine;
using UnityEngine.AI;

public class FirstNpc : MonoBehaviour
{
    public Transform goPoint;
    private NavMeshAgent agent;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = goPoint.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.enabled)
        {
            agent.destination = goPoint.position;
        }
    }
}

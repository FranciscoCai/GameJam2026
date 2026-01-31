using UnityEngine;

public class NPC_Patrol : StateMachineBehaviour
{
    private NPCDataFunctions npcData;
    private int patrolIndex;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        npcData = animator.GetComponent<NPCDataFunctions>();
        patrolIndex = 0;
        if (npcData.patrolPoints.Length > 0 && npcData.agent != null)
        {
            npcData.agent.SetDestination(npcData.patrolPoints[patrolIndex].transform.position);
        }
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (npcData.patrolPoints.Length == 0 || npcData.agent == null)
            return;
        if (!npcData.agent.pathPending && npcData.agent.remainingDistance < 1f)
        {
            patrolIndex = (patrolIndex + 1) % npcData.patrolPoints.Length;
            npcData.agent.SetDestination(npcData.patrolPoints[patrolIndex].transform.position);
        }
    }
}

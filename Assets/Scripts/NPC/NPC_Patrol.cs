using System.Collections.Generic;
using UnityEngine;

public class NPC_Patrol : StateMachineBehaviour
{
    private NPCDataFunctions npcData;
    private NPCInventory npcInventory;
    private int patrolIndex;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        npcData = animator.GetComponent<NPCDataFunctions>();
        npcInventory = animator.GetComponent<NPCInventory>();
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
        List<GameObject> detectedObjects = npcData.DetectObjectsInView();
        foreach (GameObject obj in detectedObjects)
        {
            if (!npcInventory.HasItem())
            {
                WorldItem worldItem = obj.GetComponent<WorldItem>();
                if (worldItem != null)
                {
                    npcData.agent.destination = worldItem.transform.position;
                    animator.SetTrigger("ToFindItem");
                    break;
                }
            }
        }
    }
}

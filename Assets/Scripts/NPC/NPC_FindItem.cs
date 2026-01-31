using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class NPC_FindItem : StateMachineBehaviour
{
    private NPCInventory npcInventory;
    private NPCDataFunctions npcData;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        npcInventory = animator.GetComponent<NPCInventory>();
        npcData = animator.GetComponent<NPCDataFunctions>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        List<GameObject> detectedObjects = npcData.DetectObjectsInView();
        bool seeItem = false;
        foreach (GameObject obj in detectedObjects)
        {
            WorldItem worldItem = obj.GetComponent<WorldItem>();
            if (worldItem != null)
            {
                seeItem = true;
                break;
            }
        }
        if (!seeItem)
        {
            animator.SetTrigger("ToPatrol");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}

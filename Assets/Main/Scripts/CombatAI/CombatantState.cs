 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CombatantState : StateMachineBehaviour 
{
    public static string DISTANCE_TO_OPONENT = "DistanceToOponent";
    public static string HAS_RIGHT_OF_WAY = "HasRightOfWay";
    public static string BEING_ATTACKED = "BeingAttacked";
    protected CombatantController combatant;
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        combatant = animator.gameObject.GetComponent<CombatantController>();
    }
    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetFloat(DISTANCE_TO_OPONENT, Vector3.Distance(combatant.Position(), combatant.Oponent.Position()));
    }    

    
    /*

    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }
    
    override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }
    override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }
     */
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingState : CombatantState
{
    public float AttackThreshold = 0.05f;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        
        combatant.AttackTarget = FindAttackTarget();
    }

    // Start is called before the first frame update
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);   
        if(combatant.AttackDistanceTraveled >= AttackThreshold){
        }
        //wait for a random delay
        //attempt attack
        //set right of way boolean

        /*
        if(AttacckHasFinished) //distance from hand to shoulder is >= arm length
            combatant.MoveHandTo(READY_POSITION);
        if(AttackHasLanded){
            combatant.Sore++;
            animator.SetSomethingToGoBackToReadyPositions()
        }
         */
    }

    void Wait(float seconds){
        float delayedTime = 0;
        while(delayedTime < seconds){
            delayedTime += Time.deltaTime;
        }
    }
    GameObject FindAttackTarget(){
        int targetIndex = Random.Range(0, 3);
        return combatant.Oponent.Targets[targetIndex];
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingState : CombatantState
{
    public float AttackThreshold = 0.05f;
    private Coroutine _attackRoutine;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        
        _attackRoutine = combatant.StartCoroutine(
                                    StartAttackingTarget(
                                        FindAttackTarget()));
    }

    // Start is called before the first frame update
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);   
        if(combatant.AttackDistanceTraveled >= AttackThreshold){
            //player has gained right of way
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

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        combatant.StopCoroutine(_attackRoutine);
    }

    GameObject FindAttackTarget(){
        int targetIndex = Random.Range(0, combatant.Oponent.Targets.Length);
        return combatant.Oponent.Targets[targetIndex];
    }
    
    IEnumerator StartAttackingTarget(GameObject target){
        combatant.AttackDistanceTraveled = 0.0f; //this can get out of hand
        float delay = Random.Range(0.5f, 2f);
        yield return new WaitForSeconds(delay);
        combatant.AttackTarget = target;
    }

    void MoveWeaponTowardsTarget(){
        
    }
}

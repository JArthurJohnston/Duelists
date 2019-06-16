using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingState : CombatantState
{
    /**
        The amount you have to move your weapon to gain right of way
     */
    public float AttackThreshold = 0.05f;
    private Coroutine _attackRoutine;
    private float _attackDistanceTraveled = 0.0f;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        
        _attackRoutine = combatant.StartCoroutine(
                                    StartAttackingTarget());
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);   
        animator.SetBool("IsAttacking", true);
        if(_attackDistanceTraveled >= AttackThreshold){
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
        animator.SetBool("IsAttacking", false);
    }

    Vector3 FindAttackTarget(){
        int targetIndex = Random.Range(0, combatant.Oponent.Targets.Length);
        return combatant.Oponent.Targets[targetIndex].transform.position;
    }
    
    IEnumerator StartAttackingTarget(){
        _attackDistanceTraveled = 0.0f;
        Debug.Log("Attacking");
        yield return new WaitForSeconds(Random.Range(0.5f, 2f));
        yield return MoveWeaponTowardsTarget(FindAttackTarget());
    }

    IEnumerator MoveWeaponTowardsTarget(Vector3 target){
        while(WeaponArmExtension() < combatant.ArmLength){
            float step = combatant.attackSpeed * Time.deltaTime;
            combatant.MoveWeaponTowards(target, step);
            _attackDistanceTraveled += step;
            yield return null;
        }
        Debug.Log("Arm Extended");
        yield return combatant.StartCoroutine(FinishedAttack());
    }

    IEnumerator MoveWeaponToReady(){
        Debug.Log("moving to ready");
        while(Vector3.Distance(WeaponPosition(), combatant.enGuardPosition.transform.position) > 0.001f){
            combatant.MoveWeaponTo(combatant.enGuardPosition.transform);
            yield return null;
        }
    }

    float WeaponArmExtension(){
        return Vector3.Distance(WeaponPosition(), combatant.WeaponShoulder.transform.position);
    }

    Vector3 WeaponPosition(){
        return combatant.WeaponHand.transform.position;
    }

    IEnumerator FinishedAttack(){
        /*
        Four options:
        1) move back to ready position
        2) continue attacking
        3) attack another target
        4) pull back for a defense
         */
         Debug.Log("attack finished");
         yield return combatant.StartCoroutine(MoveWeaponToReady());
    }
}

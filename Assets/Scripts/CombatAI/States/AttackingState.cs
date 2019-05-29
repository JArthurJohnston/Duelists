using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingState : CombatantState
{
    // Start is called before the first frame update
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        //wait for a random delay
        //attempt attack
        //set right of way boolean
    }
}

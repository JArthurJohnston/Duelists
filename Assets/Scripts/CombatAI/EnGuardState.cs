using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnGuardState : CombatantState
{
    
    /*
    - Follow the player
     */

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //eventually use a rotation slerp here
        combatant.FaceOponent();
    }
}

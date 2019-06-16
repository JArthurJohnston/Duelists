using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatantController : AbstractPlayer
{
    public string PlayerName;
    public GameObject WeaponShoulder;
    public GameObject WeaponHand;
    public float ArmLength = 2;
    public float attackSpeed = 15;
    public GameObject[] GuardPositions;
    private Animator _animator;
    public Transform enGuardPosition;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        // if(IsBeingAttacked()){
        //     _animator.SetBool(CombatantState.BEING_ATTACKED, true); //ideally this logic would be handled by the state machine
        // }
        // HandleAttackTarget();
    }
        /**
    Ideally this will return the index of the guard needed to deflect the current attack
     */
    public int FindGuardPosition(){
        Vector3 heading = AttackHeading();
        if(heading.x > 0 && heading.y > 0){
            return 3;
        } else if(heading.x > 0 && heading.y < 0){
            return 1; //fourth
        } else if(heading.x < 0 && heading.y > 0){
            return 2;
        } else if(heading.x < 0 && heading.y < 0){
            return 0; //sixth
        }
        return -1;
    }

    public void MoveWeaponTowards(Vector3 target, float step){
        WeaponHand.transform.rotation = 
            Quaternion.Lerp(WeaponHand.transform.rotation, Quaternion.LookRotation(target - WeaponHand.transform.position), step);
        WeaponHand.transform.position = 
            Vector3.MoveTowards(WeaponHand.transform.position, target, step);
    }

    public void MoveWeaponTo(Transform targetTransform){
        float step = attackSpeed * Time.deltaTime;

        var transform = WeaponHand.transform;

        transform.position = Vector3.Lerp(transform.position, targetTransform.position, step);
        transform.rotation = Quaternion.Lerp (transform.rotation, targetTransform.rotation, step);
    }

    public void FaceOponent(){
        transform.LookAt(Oponent.Position());
    }
}

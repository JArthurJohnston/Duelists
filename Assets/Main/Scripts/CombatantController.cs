using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatantController : AbstractPlayer
{
    public float AttackDistanceTraveled {get; set;}
    public GameObject AttackTarget {
        get{return _attackTarget;} 
        set {
            if(_attackTarget == null){
                StartCoroutine(StartAttackingTarget(value));
            }
        }
    }
    public string PlayerName;
    public GameObject WeaponShoulder;
    public GameObject WeaponHand;
    public float ArmLength = 2;
    public float attackSpeed = 15;
    public GameObject[] GuardPositions;
    private GameObject _attackTarget;
    private Animator _animator;
    public Transform enGuardPosition;

    void Start()
    {
        AttackDistanceTraveled = 0;
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(IsBeingAttacked()){
            _animator.SetBool(CombatantState.BEING_ATTACKED, true);
        }
        HandleAttackTarget();
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

    void HandleAttackTarget(){
        if(_attackTarget != null){
            float step = attackSpeed * Time.deltaTime;
            AttackDistanceTraveled += step;
            if(Vector3.Distance(WeaponHand.transform.position, WeaponShoulder.transform.position) < ArmLength){
                var relativePosition = _attackTarget.transform.position - WeaponHand.transform.position;
                var newRotation = Quaternion.LookRotation(relativePosition);
                WeaponHand.transform.rotation = 
                    Quaternion.Lerp(WeaponHand.transform.rotation, newRotation, step);
                WeaponHand.transform.position = 
                    Vector3.MoveTowards(WeaponHand.transform.position, _attackTarget.transform.position, step);
            }
        }
    }

    IEnumerator StartAttackingTarget(GameObject target){
        AttackDistanceTraveled = 0.0f;
        float delay = Random.Range(0.5f, 2f);
        yield return new WaitForSeconds(delay);
        _attackTarget = target;
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

    public bool HasRightOfWay(){
        return _animator.GetBool(CombatantState.HAS_RIGHT_OF_WAY);
    }

}

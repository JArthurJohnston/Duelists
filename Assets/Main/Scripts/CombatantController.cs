using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatantController : MonoBehaviour
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
    public GameObject[] Targets;
    public GameObject[] GuardPositions;
    public CombatantController Oponent;
    private GameObject _attackTarget;
    private Animator _animator;

    public static int GUARD_1 = 0;
    public static int GUARD_2 = 1;
    public static int GUARD_3 = 2;
    public static int GUARD_4 = 3;

    void Start()
    {
        AttackDistanceTraveled = 0;
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        HandleAttackTarget();
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

    public Vector3 Position(){
        return transform.position;
    }

    public void FaceOponent(){
        transform.LookAt(Oponent.Position());
    }

    public bool HasRightOfWay(){
        return _animator.GetBool(CombatantState.HAS_RIGHT_OF_WAY);
    }

    //this should be a check of the position and movement of the oponents weapon
    public void BeingAttacked(){
        _animator.SetBool(CombatantState.BEING_ATTACKED, true);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatantController : MonoBehaviour
{
    public string PlayerName;
    private GameObject _attackTarget;
    public float AttackDistanceTraveled {get; set;}
    public GameObject WeaponShoulder;
    public GameObject WeaponHand;
    public float ArmLength = 2;
    public GameObject AttackTarget {
        get{return _attackTarget;} 
        set {
            if(_attackTarget == null){
                StartCoroutine(StartAttackingTarget(value));
            }
        }
    }
    public float attackSpeed = 15;
    public bool IsDebug = false;
    private Vector3 LookTarget;
    public GameObject[] Targets;
    public CombatantController Oponent;
    // Start is called before the first frame update
    private Animator _animator;
    void Start()
    {
        AttackDistanceTraveled = 0;
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleAttackTarget();
    }

    void HandleAttackTarget(){
        if(_attackTarget != null){
            WeaponHand.transform.LookAt(_attackTarget.transform);
            float step = attackSpeed * Time.deltaTime;
            AttackDistanceTraveled += step;
            if(Vector3.Distance(WeaponHand.transform.position, WeaponShoulder.transform.position) < ArmLength){
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

    void HandleDebugInputs(){
        if(IsDebug){
            if(Input.GetKeyDown("1")){

            }
        }
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

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatantController : MonoBehaviour
{
    private GameObject _attackTarget;
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
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(_attackTarget != null){
            WeaponHand.transform.LookAt(_attackTarget.transform);
            float step = attackSpeed * Time.deltaTime;
            if(Vector3.Distance(WeaponHand.transform.position, WeaponShoulder.transform.position) < ArmLength){
                WeaponHand.transform.position = 
                    Vector3.MoveTowards(WeaponHand.transform.position, _attackTarget.transform.position, step);
            }
        }
    }

    IEnumerator StartAttackingTarget(GameObject target){
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

    

}

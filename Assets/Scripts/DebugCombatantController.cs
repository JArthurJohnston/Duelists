using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugCombatantController : MonoBehaviour
{
    private CombatantController _controller;
    private Transform _guardPosition; 

    void Start(){
        _controller = GetComponent<CombatantController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("1")){
            int guardIndex = Random.Range(0, 3);
            _guardPosition = _controller.GuardPositions[guardIndex].transform;
        }
        if(_guardPosition != null){
            _controller.MoveWeaponTo(_guardPosition);
        }
    }
}

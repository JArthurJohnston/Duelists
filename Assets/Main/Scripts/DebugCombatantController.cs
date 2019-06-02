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
            _guardPosition = _controller.GuardPositions[0].transform;
        } else if(Input.GetKeyDown("2")){
            _guardPosition = _controller.GuardPositions[1].transform;
        }else if(Input.GetKeyDown("3")){
            _guardPosition = _controller.GuardPositions[2].transform;
        }else if(Input.GetKeyDown("4")){
            _guardPosition = _controller.GuardPositions[3].transform;
        }else if(Input.GetKeyDown("5")){
            _guardPosition = _controller.GuardPositions[4].transform;
        }
        
        if(_guardPosition != null){
            _controller.MoveWeaponTo(_guardPosition);
        }
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugCombatantController : MonoBehaviour
{
    private CombatantController _controller;
    private Transform _guardPosition; 

    void Start(){
        _controller = GetComponent<CombatantController>();
    }

    public static void DrawVectors(Vector3 position){
        Debug.DrawLine(position, position + Vector3.right * 3, Color.red);
        Debug.DrawLine(position, position + Vector3.up * 3, Color.green);
        Debug.DrawLine(position, position + Vector3.forward * 3, Color.blue);
    }

    void Update()
    {
        Debug.DrawLine(transform.position, _controller.Oponent.weapon.Position(), Color.cyan);
        HandleUserInput();

        // if(_controller.IsBeingAttacked()){
        //     int guardIndex = _controller.FindGuardPosition();
        //     // Debug.Log("Being Attackedd! " + _controller.AttackHeading() +" " + guardIndex);
        //     if(guardIndex >= 0){
        //         _guardPosition = _controller.GuardPositions[guardIndex].transform;
        //     }
        // } else {
        //     // Debug.Log("Safe");
        // }

        // if(_guardPosition != null){
        //     _controller.MoveWeaponTo(_guardPosition);
        // }
    }

    void HandleUserInput(){
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
    }
}

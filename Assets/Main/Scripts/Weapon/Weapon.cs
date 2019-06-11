using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float movementThreshold = 0.02f;
    public BladeHitDetection hitDetector;
    public GameObject blade;
    public GameObject hilt;
    public GameObject point;
    private Vector3 _previousPosition;
    
    void FixedUpdate(){
        if(Vector3.Distance(blade.transform.position, _previousPosition) > movementThreshold){
            _previousPosition = blade.transform.position;
        }
    }

    public bool IsAttacking(){
        return hitDetector.IsAttacking();
    }

    public Vector3 Heading(){
        return _previousPosition - blade.transform.position;
    }

    public Vector3 Position(){
        return blade.transform.position;
    }
}

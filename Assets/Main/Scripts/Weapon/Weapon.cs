using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float movementThreshold = 0.02f; //TODO make sure this is set properly
    public BladeHitDetection hitDetector;
    public GameObject blade;
    public GameObject hilt;
    public GameObject point;
    private Vector3 _previousPosition;
    public GripBehavior grip;

    void Update(){
        Debug.DrawLine(blade.transform.position, blade.transform.position + Heading() * 12, Color.green);
    }
    
    void FixedUpdate(){
        if(Vector3.Distance(blade.transform.position, _previousPosition) > movementThreshold){
            _previousPosition = blade.transform.position;
        }
    }

    public bool IsAttacking(){
        return hitDetector.IsAttacking();
    }

    public Vector3 Heading(){
        return blade.transform.position - _previousPosition;
    }

    public float Speed(){
        return Heading().magnitude;
    }

    public Vector3 Position(){
        return blade.transform.position;
    }

    void OnCollisionEnter(Collision collision){
        grip.UnlockRotation();
    }

    void OnCollisionExit(Collision collision){
        grip.LockRotation();
    }
}

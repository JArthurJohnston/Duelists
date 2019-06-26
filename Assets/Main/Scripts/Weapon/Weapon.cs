using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float movementThreshold = 0.02f; //TODO make sure this is set properly
    public GameObject blade;
    public GameObject hilt;
    public GameObject point;
    private Vector3 _previousPosition;
    public GripBehavior grip;

    void Update(){
        Debug.DrawLine(blade.transform.position, blade.transform.position + Heading() * 12, Color.green);
    }
    
    void FixedUpdate(){
        //why am I doing this in fixed update again?
        if(DistanceTraveled() > movementThreshold){
            _previousPosition = blade.transform.position;
        }
    }

    public float DistanceTraveled(){
        return Vector3.Distance(blade.transform.position, _previousPosition);
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeHitDetection : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 _previousPosition;
    public GameObject marker;
    public float collisionDistance = 2;
    public float movementThreshold = 0.002f;

    void Start()
    {
        _previousPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate(){
        // if(Vector3.Distance(transform.position, _previousPosition) > movementThreshold){
            // Debug.Log(transform.position);
            // Debug.Log(_previousPosition);
            // Debug.DrawLine(transform.position, _previousPosition, Color.cyan);
            // var distance = Vector3.Distance(_previousPosition, transform.position);
            
            // var direction = GetMovementDirection();
            // var markerPosition = transform.position + direction * distance;
            // marker.transform.position = markerPosition;;

            var linePosition = transform.position + GetMovementDirection() * collisionDistance;
            Debug.DrawLine(transform.position, linePosition, Color.cyan);
            _previousPosition = transform.position;
        // }
    }

    Vector3 GetMovementDirection(){
        return   transform.position - _previousPosition;
        // var direction =  transform.position - _previousPosition;
        // var localDirection = transform.InverseTransformDirection(direction);
    }
}

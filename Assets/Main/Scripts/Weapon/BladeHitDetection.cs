using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeHitDetection : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 _previousPosition;
    public float collisionDistance = 2;
    public float detectionDistance = 10;
    public Transform hilt;
    public Transform point;
    public LayerMask layerMask;

    void Start()
    {
        _previousPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.DrawLine(transform.position, _previousPosition, Color.cyan);
    }

    void FixedUpdate(){
        if(transform.position != _previousPosition) {

            if(IsAttacking()){
                Debug.Log("Attacking " + GetMovementDirection().magnitude);
            }

            _previousPosition = transform.position;
        }
    }

    bool IsAttacking(){
        return CastTowardsSwing() == "Hittable";
    }

    string CastTowardsSwing(){
        RaycastHit hit;
        float bladeRadius = 0.05f;
        if(Physics.CapsuleCast(hilt.position, point.position, bladeRadius, GetMovementDirection(), out hit, collisionDistance, layerMask, QueryTriggerInteraction.Ignore)){
            var distance = Vector3.Distance(transform.position, hit.transform.position);
            if(distance < detectionDistance){
                return hit.collider.tag;
            // } else if (distance < collisionDistance) {
            //     return hit.collider.name;
            }
        }
        return "";
    }

    Vector3 GetMovementDirection(){
        return transform.position - _previousPosition;
    }
}

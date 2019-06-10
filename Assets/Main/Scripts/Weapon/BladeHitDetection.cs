using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeHitDetection : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 _previousPosition;
    public float collisionDistance = 2;
    public float detectionDistance = 10;
    public float minimumAttackingSpeed = 0.025f;
    public Transform hilt;
    public Transform point;
    public LayerMask layerMask;
    private Renderer _renderer;
    private Material _defaultColor;

    public Material attackColor;


    void Start()
    {
        _renderer = GetComponent<Renderer>();
        _defaultColor = _renderer.material;
        _previousPosition = transform.position;
    }

    void ChangeBladeColor(Color color){
        _renderer.material.SetColor("_Color", color);
    }

    void FixedUpdate(){
        if(transform.position != _previousPosition) {

            if(IsAttacking()){
                _renderer.material = attackColor;
            } else {
                _renderer.material = _defaultColor;
            }

            var linePosition = transform.position + GetMovementDirection() * collisionDistance;
            Debug.DrawLine(transform.position, linePosition, Color.cyan);
            _previousPosition = transform.position;
        }
    }

    public bool IsAttacking(){
        return GetCurrentWeaponTarget() == "Hittable";
    }

    /**
    If the weapon is moving fast enough, it will cast in the direction its moving, then
    return the tag of the object its moving towards
     */
    string GetCurrentWeaponTarget(){
        RaycastHit hit;
        Vector3 direction = GetMovementDirection();
        float speed = direction.magnitude;



        if(speed > minimumAttackingSpeed && GetSwingTarget(out hit)){
            var distance = Vector3.Distance(transform.position, hit.transform.position);
            if(distance < detectionDistance){
                return hit.collider.tag;
            // } else if (distance < collisionDistance) {
            //     return hit.collider.name;
            }
        }
        return "";
    }

    bool GetSwingTarget(out RaycastHit hit){
        float bladeRadius = 0.05f;
        return Physics.CapsuleCast(
            hilt.position, 
            point.position, 
            bladeRadius, GetMovementDirection(), out hit, collisionDistance, layerMask, QueryTriggerInteraction.Ignore);
    }

    public Vector3 GetMovementDirection(){
        return transform.position - _previousPosition;
    }
}

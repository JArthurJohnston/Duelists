using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeColliders : MonoBehaviour
{
    public float collisionCheckDistance = 1;
    private Vector3 _previousPosition;
    private Rigidbody _rigidBody;
    void Start()
    {
        _previousPosition = transform.position;
        _rigidBody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        var direction = GetDirection();
        var distance = 10f;

        Debug.Log("Attacking towards " +  transform.position + " from " + _previousPosition);
        // Debug.DrawRay(transform.position, direction * distance, Color.cyan);

        RaycastHit hit;
        if (_rigidBody.SweepTest(GetDirection(), out hit, Vector3.Distance(_previousPosition, transform.position) * collisionCheckDistance))
        {
            Debug.Log("About to hit: " + hit.collider.gameObject.name);
        }
        _previousPosition = transform.position;
    }

    public Vector3 GetVelocity(){
        return _previousPosition - transform.position;
    }

    public Vector3 GetDirection() {
        return GetVelocity().normalized;
    }
}

/*
 directions on finding angles
 float worldDegrees = Vector3.Angle(Vector3.forward, direction); // angle relative to world space
float localDegrees = Vector3.Angle(myobject.transform.forward, direction); // angle relative to last heading of myobject
 */
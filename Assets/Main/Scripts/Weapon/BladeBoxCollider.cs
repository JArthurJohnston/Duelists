using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeBoxCollider : MonoBehaviour
{
    public Transform hilt;
    public Transform tip;
    private Vector3 _center;
    private BoxCollider _collider;
    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<BoxCollider>();
        _center = _collider.center;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(_center);
    }

    void FixedUpdate(){

    }
}

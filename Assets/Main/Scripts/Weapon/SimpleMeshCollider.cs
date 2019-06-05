using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class SimpleMeshCollider : MonoBehaviour
{
    private const int CURRENT_HILT = 0;
    private const int CURRENT_TIP = 1;
    private const int PREVIOUS_HILT = 2;
    private const int PREVIOUS_TIP = 3;
    private Mesh _mesh;

    /**
        1: Current Tip  2: Previous Tip
        |             / |
        |        /      |
        |   /           |
        0: Current Hilt 3: Previous Hilt
     */
    private readonly int[] _triangles = new int[] {
        CURRENT_HILT, CURRENT_TIP, PREVIOUS_HILT,
        CURRENT_HILT, PREVIOUS_HILT, PREVIOUS_TIP
    };
    private MeshCollider _collider;

    public Transform hilt;
    public Transform point;
    public float movementThreshold = 0.02f;

    /**
    only made public for testing and debugging.
    The previous points are set programatically during the game
     */
    public Transform previousHilt;
    public Transform previousPoint;
    private float _defaultBladeWidth = 0.01f;
    
    void Start()
    {
        InitMesh();
        InitCollider();
    }

    Vector3[] DefaultVertices(){
        return new Vector3[] {
            new Vector3(hilt.position.x - _defaultBladeWidth, hilt.position.y, hilt.position.z),
            new Vector3(point.position.x - _defaultBladeWidth, point.position.y, point.position.z),
            new Vector3(hilt.position.x + _defaultBladeWidth, hilt.position.y, hilt.position.z),
            new Vector3(point.position.x + _defaultBladeWidth, point.position.y, point.position.z),
        };
    }

    void InitMesh(){
        _mesh = new Mesh();
        _mesh.vertices = DefaultVertices();
        _mesh.triangles = _triangles;
        _mesh.name = "Collider Mesh";
        GetComponent<MeshFilter>().mesh = _mesh;
    }

    void InitCollider(){
        _collider = gameObject.AddComponent<MeshCollider>();
        _collider.convex = true;
        _collider.sharedMesh = _mesh;
    }

    void FixedUpdate(){
        //if(HasMoved())//may be unnecessary, as I cant imagine a time when the blade WONT be moving during the game
        _mesh.vertices = GenerateVertices();
        _collider.sharedMesh = null;
        _collider.sharedMesh = _mesh;
    }

    bool HasMoved(){
        return Vector3.Distance(previousHilt.position, hilt.position) > movementThreshold &&
            Vector3.Distance(previousPoint.position, point.position) > movementThreshold;
    }

    Vector3[] GenerateVertices(){
        return new Vector3[] { //consider updating the _mesh.vertices values directly, instead of instantiating an arry during fixed update
            transform.InverseTransformPoint(hilt.position),
            transform.InverseTransformPoint(point.position),
            transform.InverseTransformPoint(previousPoint.position), 
            transform.InverseTransformPoint(previousHilt.position)
        };
    } 
}

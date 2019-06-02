using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeCollider : MonoBehaviour
{
    private static int PREVIOUS_HILT = 0;
    private static int PREVIOUS_TIP = 1;
    private static int CURRENT_TIP = 2;
    private static int CURRENT_HILT = 3;

    private static int[] TRIANGLES = new int[] {
        PREVIOUS_HILT,
        PREVIOUS_TIP,
        CURRENT_HILT,
        CURRENT_HILT,
        PREVIOUS_TIP,
        CURRENT_TIP
    };

    public float threshold = 0.005f;
    public GameObject hiltPoint;
    public GameObject tipPoint;
    private MeshCollider movingBladeCollider;
    private Mesh collisionMesh;

    private Vector3 previousPoint;
    private Vector3 previousHilt;
    private Vector3[] meshPoints;
    // Start is called before the first frame update
    void Start()
    {
        var hilt = hiltPoint.transform.position;
        var tip = tipPoint.transform.position;

        meshPoints = new Vector3[] {
            new Vector3(hilt.x - threshold, hilt.y, hilt.z),
            new Vector3(tip.x - threshold, tip.y, tip.z),
            new Vector3(tip.x + threshold, tip.y, tip.z),
            new Vector3(hilt.x + threshold, hilt.y, hilt.z),
        };

        collisionMesh = new Mesh();
        collisionMesh.vertices = meshPoints;
        collisionMesh.triangles = TRIANGLES;

        movingBladeCollider = gameObject.AddComponent<MeshCollider>();
        movingBladeCollider.convex = true;
        movingBladeCollider.cookingOptions = MeshColliderCookingOptions.EnableMeshCleaning | MeshColliderCookingOptions.WeldColocatedVertices;
        movingBladeCollider.sharedMesh = collisionMesh;
    }

    // Update is called once per frame
    void Update()
    {
        DrawDebugOutline();
    }

    void DrawDebugOutline(){
        Debug.DrawLine(meshPoints[PREVIOUS_HILT], meshPoints[PREVIOUS_TIP], Color.cyan);
        Debug.DrawLine(meshPoints[PREVIOUS_TIP], meshPoints[CURRENT_HILT], Color.cyan);
        Debug.DrawLine(meshPoints[CURRENT_HILT], meshPoints[CURRENT_HILT], Color.cyan);
        Debug.DrawLine(meshPoints[CURRENT_HILT], meshPoints[PREVIOUS_TIP], Color.cyan);
        Debug.DrawLine(meshPoints[PREVIOUS_TIP], meshPoints[CURRENT_TIP], Color.cyan);
    }

    void FixedUpdate(){
        if(BladeHasMoved()){
            UpdateCollider();
        }
    }

    bool IsMoving(){
        return GetComponent<Rigidbody>().velocity.magnitude > 0;
    }

    bool BladeHasMoved(){
        return Vector3.Distance(previousHilt, hiltPoint.transform.position) > threshold &&
            Vector3.Distance(previousPoint, tipPoint.transform.position) > threshold;
    }

    void UpdateCollider(){
        UpdateMesh();
        var currentMesh = movingBladeCollider.sharedMesh;
        movingBladeCollider.sharedMesh = null;
        movingBladeCollider.sharedMesh = collisionMesh;
    }

    void UpdateMesh(){
        UpdateMeshPoints();
        collisionMesh.Clear();
        collisionMesh.vertices = meshPoints;
        collisionMesh.triangles = TRIANGLES;
    }

    void UpdateMeshPoints(){
        if(VerticesAreValid()){
            meshPoints = new Vector3[] {
                previousHilt,
                previousPoint,
                hiltPoint.transform.position, 
                tipPoint.transform.position,
            };
            previousHilt = hiltPoint.transform.position;
            previousPoint = tipPoint.transform.position;
        }
    }

    bool VerticesAreValid(){
        return hiltPoint.transform.position != Vector3.zero && tipPoint.transform.position != Vector3.zero;
    }
}

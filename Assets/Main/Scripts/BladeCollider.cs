using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeCollider : MonoBehaviour
{
    private static int CURRENT_HILT = 0;
    private static int CURRENT_TIP = 1;
    private static int PREVIOUS_TIP = 2;
    private static int PREVIOUS_HILT = 3;

    private static int[] TRIANGLES = new int[] {
        CURRENT_HILT, CURRENT_TIP, PREVIOUS_TIP,
        CURRENT_HILT, PREVIOUS_TIP, PREVIOUS_HILT
    };

    public float threshold = 0.005f;
    public GameObject hiltPoint;
    public GameObject tipPoint;
    private MeshCollider movingBladeCollider;
    private Mesh collisionMesh;

    private Vector3 previousPoint;
    private Vector3 previousHilt;
    public Vector3[] meshPoints;
    // Start is called before the first frame update
    void Start()
    {
        var hilt = hiltPoint.transform.position;
        var tip = tipPoint.transform.position;

        meshPoints = new Vector3[] {
            hilt, tip, tip, hilt
        };

        collisionMesh = new Mesh();
        collisionMesh.vertices = meshPoints;
        collisionMesh.triangles = TRIANGLES;

        movingBladeCollider = gameObject.AddComponent<MeshCollider>();
        movingBladeCollider.convex = true;
        // movingBladeCollider.cookingOptions = MeshColliderCookingOptions.EnableMeshCleaning | MeshColliderCookingOptions.WeldColocatedVertices;
        movingBladeCollider.cookingOptions = MeshColliderCookingOptions.None;
        movingBladeCollider.sharedMesh = collisionMesh;
    }

    // Update is called once per frame
    void Update()
    {
        // DrawDebugOutline();
    }

    void DrawDebugOutline(){
        var wedgePoint = RaisedWedgePointFor(meshPoints[CURRENT_TIP], meshPoints[PREVIOUS_TIP]);
        Debug.DrawLine(meshPoints[CURRENT_TIP], wedgePoint, Color.magenta);
        Debug.DrawLine(meshPoints[PREVIOUS_TIP], wedgePoint, Color.magenta);

        Debug.DrawLine(meshPoints[CURRENT_HILT], meshPoints[CURRENT_TIP], Color.cyan);
        Debug.DrawLine(meshPoints[CURRENT_TIP], meshPoints[PREVIOUS_TIP], Color.cyan);
        Debug.DrawLine(meshPoints[CURRENT_HILT], meshPoints[PREVIOUS_HILT], Color.cyan);
        Debug.DrawLine(meshPoints[PREVIOUS_TIP], meshPoints[PREVIOUS_HILT], Color.cyan);
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
                hiltPoint.transform.position, 
                tipPoint.transform.position,
                previousPoint,
                previousHilt,
            };
            previousHilt = hiltPoint.transform.position;
            previousPoint = tipPoint.transform.position;
        }
    }

    bool VerticesAreValid(){
        return hiltPoint.transform.position != Vector3.zero && tipPoint.transform.position != Vector3.zero;
    }

    private static Vector3 angleDirection = new Vector3(0, 0.2f, 0);
    Vector3 RaisedWedgePointFor(Vector3 start, Vector3 end){
        Vector3 direction = (start - end).normalized;
         Vector3 midPoint = (start + end) / 2f;
         return end + Quaternion.AngleAxis(90.0f, angleDirection) * angleDirection;
    }

}

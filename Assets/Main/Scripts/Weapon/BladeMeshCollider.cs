using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeMeshCollider : MonoBehaviour
{
    private static int CURRENT_HILT = 0;
    private static int CURRENT_TIP = 1;
    private static int WEDGE_TIP = 2;
    private static int WEDGE_HILT = 3;
    private static int PREVIOUS_TIP = 4;
    private static int PREVIOUS_HILT = 5;

    private static int[] TRIANGLES = new int[] {
        CURRENT_HILT, CURRENT_TIP, WEDGE_TIP,
        WEDGE_TIP, WEDGE_HILT, CURRENT_HILT,
        CURRENT_HILT, CURRENT_TIP, PREVIOUS_HILT,
        CURRENT_TIP, PREVIOUS_TIP, PREVIOUS_HILT,
        PREVIOUS_HILT, WEDGE_TIP, PREVIOUS_TIP,
        WEDGE_HILT, PREVIOUS_TIP, PREVIOUS_HILT,
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
            hilt, tip, tip, hilt, tip, hilt
        };

        collisionMesh = new Mesh();
        collisionMesh.vertices = meshPoints;
        collisionMesh.triangles = TRIANGLES;

        movingBladeCollider = gameObject.AddComponent<MeshCollider>();
        movingBladeCollider.convex = true;
        // movingBladeCollider.cookingOptions = MeshColliderCookingOptions.EnableMeshCleaning;
        // movingBladeCollider.cookingOptions = MeshColliderCookingOptions.EnableMeshCleaning | MeshColliderCookingOptions.WeldColocatedVertices;
        // movingBladeCollider.cookingOptions = MeshColliderCookingOptions.None;
        movingBladeCollider.sharedMesh = collisionMesh;
        // movingBladeCollider.transform.localScale = new Vector3(1, 1, 1); //Dont Do This!!!
    }

    // Update is called once per frame
    void Update()
    {
        DrawDebugOutline();
    }

    void DrawDebugOutline() {
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
        movingBladeCollider.sharedMesh = null;
        movingBladeCollider.sharedMesh = collisionMesh;
    }

    void UpdateMesh(){
        UpdateMeshPoints();
        collisionMesh.Clear();
        collisionMesh.vertices = meshPoints;
        collisionMesh.triangles = TRIANGLES;
        collisionMesh.RecalculateBounds();
    }

    void UpdateMeshPoints(){
        if(VerticesAreValid()){
            var tipWedgePoint = RaisedWedgePointFor(tipPoint.transform.position, previousPoint);
            var hiltWedgePoint = RaisedWedgePointFor(hiltPoint.transform.position, previousHilt);
            meshPoints = new Vector3[] {
                hiltPoint.transform.position, 
                tipPoint.transform.position,
                tipWedgePoint,
                hiltWedgePoint,
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

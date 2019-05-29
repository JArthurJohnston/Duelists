using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontGoThroughThings : MonoBehaviour
{
       // Careful when setting this to true - it might cause double
       // events to be fired - but it won't pass through the trigger
       public bool sendTriggerMessage = false; 	
 
	public LayerMask layerMask = -1; //make sure we aren't in this layer 
	public float skinWidth = 0.1f; //probably doesn't need to be changed 
    public GameObject hiltPoint;
    public GameObject tipPoint;
    public float bladeRadius = 0.25f;

    private Vector3[] previousPoints;
	private float minimumExtent; 
	private float partialExtent; 
	private float sqrMinimumExtent; 
	private Vector3 previousPosition; 
	private Rigidbody myRigidbody;
	private Collider myCollider;
 
	//initialize values 
	void Start() 
	{ 
        previousPoints = new Vector3[] {
            hiltPoint.transform.position, 
            tipPoint.transform.position
        };
	   myRigidbody = GetComponent<Rigidbody>();
	   myCollider = GetComponent<Collider>();
	   previousPosition = myRigidbody.position; 
	   minimumExtent = Mathf.Min(Mathf.Min(myCollider.bounds.extents.x, myCollider.bounds.extents.y), myCollider.bounds.extents.z); 
	   partialExtent = minimumExtent * (1.0f - skinWidth); 
	   sqrMinimumExtent = minimumExtent * minimumExtent; 
	} 

    // void Update(){
        
    // }
 
	void FixedUpdate() 
	{ 
	   //have we moved more than our minimum extent? 
	   Vector3 movementThisStep = myRigidbody.position - previousPosition; 
	   float movementSqrMagnitude = movementThisStep.sqrMagnitude;
 
	   if (movementSqrMagnitude > sqrMinimumExtent) 
		{ 
	      float movementMagnitude = Mathf.Sqrt(movementSqrMagnitude);
	      RaycastHit hitInfo; 
 
	      //check for obstructions we might have missed 
        Debug.DrawRay(transform.position, movementThisStep, Color.green, 1f);
	    //   if (Physics.Raycast(previousPosition, movementThisStep, out hitInfo, movementMagnitude, layerMask.value))
	    //   if (BladeCast(movementThisStep, movementMagnitude, out hitInfo))
        if(myRigidbody.SweepTest(movementThisStep, out hitInfo, movementSqrMagnitude, QueryTriggerInteraction.Ignore))
              {
                  Debug.Log("Hit Something!!!");

                 if (!hitInfo.collider){
                     Debug.Log("No Collider");
                     return;
                 }
 
                 if (hitInfo.collider.isTrigger) 
                     hitInfo.collider.SendMessage("OnTriggerEnter", myCollider);
 
                 if (!hitInfo.collider.isTrigger){
                     Debug.Log("Resetting Position");
                     myRigidbody.position = hitInfo.point - (movementThisStep / movementMagnitude) * partialExtent; 
                 }
 
              }
	   } 
 
        previousPoints[0] = hiltPoint.transform.position;
        previousPoints[1] = tipPoint.transform.position;
	   previousPosition = myRigidbody.position; 
	}

    private bool BladeCast(Vector3 direction, float distance, out RaycastHit hitInfo){
        return Physics.CapsuleCast(
            hiltPoint.transform.position, 
            tipPoint.transform.position, 
            bladeRadius, 
            direction, 
            out hitInfo, 
            distance, 
            layerMask, 
            QueryTriggerInteraction.Ignore);
    }
}

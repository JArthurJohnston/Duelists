using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatantController : MonoBehaviour
{
    private Vector3 LookTarget;
    public GameObject[] Targets;
    public CombatantController Oponent;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public Vector3 Position(){
        return transform.position;
    }

    public void FaceOponent(){
        transform.LookAt(Oponent.Position());
    }

    

}

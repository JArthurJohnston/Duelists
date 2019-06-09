using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractPlayer : MonoBehaviour
{
    public AbstractPlayer Oponent;
    public GameObject[] Targets;
    
    public Vector3 Position(){
        return transform.position;
    }

    public void BeingAttacked(){
    }

    public GameObject AttackTarget {get; set;}
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractPlayer : MonoBehaviour
{
    public Weapon weapon;
    public AbstractPlayer Oponent;
    public GameObject[] Targets;

    void FixedUpdate(){
        Debug.Log("Weapon Angle: " + Vector3.Angle(weapon.transform.forward, Oponent.weapon.Heading()));
    }

    public bool IsAttacking(){
        return weapon.IsAttacking();
    }
    
    public Vector3 Position(){
        return transform.position;
    }

    public void BeingAttacked(){

    }

    public GameObject AttackTarget {get; set;}
}


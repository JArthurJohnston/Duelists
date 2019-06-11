using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractPlayer : MonoBehaviour
{
    public Weapon weapon;
    public AbstractPlayer Oponent;
    public GameObject[] Targets;

    void Update(){
        // Debug.Log("Weapon Angle: " + Vector3.Angle(weapon.transform.forward, Oponent.weapon.Heading()));
    }

    public bool IsAttacking(){
        return weapon.IsAttacking();
    }
    
    public Vector3 Position(){
        return transform.position;
    }

    public void BeingAttacked(){
        Debug.Log(Vector3.Angle(Oponent.weapon.Position() - transform.position, Oponent.weapon.Heading()));
    }

    public GameObject AttackTarget {get; set;}
}


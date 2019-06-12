using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractPlayer : MonoBehaviour
{
    public Weapon weapon;
    public AbstractPlayer Oponent;
    public GameObject[] Targets;
    public float attackDetectionAngle = 30f;

    void Update(){
        // Debug.Log("Weapon Angle: " + Vector3.Angle(weapon.transform.forward, Oponent.weapon.Heading()));
    }

    public bool IsAttacking(){
        return weapon.IsAttacking();
    }

    public bool IsBeingAttacked(){
        return Vector3.Angle(Oponent.weapon.Heading(), transform.position - Oponent.weapon.Position()) < attackDetectionAngle;
    }
    
    public Vector3 Position(){
        return transform.position;
    }

    public void BeingAttacked(){
        Debug.Log(Vector3.Angle(Oponent.weapon.Position() - transform.position, Oponent.weapon.Heading()));
    }

    public GameObject AttackTarget {get; set;}
}


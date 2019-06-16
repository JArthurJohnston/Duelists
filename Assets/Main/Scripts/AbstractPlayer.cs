using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractPlayer : MonoBehaviour
{
    private enum GUARDS {
        FOURTH=0,
        SIXTH=1,
        SEVENTH=2,
        EIGHTH=3,
    }
    public Weapon weapon;
    public AbstractPlayer Oponent;
    public GameObject[] Targets;
    public float attackDetectionAngle = 30f;

    void Update(){
        // Debug.Log("Weapon Angle: " + Vector3.Angle(weapon.transform.forward, Oponent.weapon.Heading()));
    }

    public bool IsBeingAttacked(){
        return Oponent.weapon.Speed() > 0 && AttackAngle() < attackDetectionAngle;
    }

    public Vector3 AttackHeading(){
        return (transform.position - Oponent.weapon.Position()).normalized; //might not need normalized
    }

    public float AttackAngle(){
        return Vector3.Angle(Oponent.weapon.Heading(), transform.position - Oponent.weapon.Position());
    }
    
    public Vector3 Position(){
        return transform.position;
    }

    public void BladesCollided(){
        Debug.Log("Wap!!!");
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public Weapon _weaponA;
    public Weapon _weaponB;
    public float minCollisionDistance = 0.01f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    float CollisionDistance(){
        float calculatedDistance = _weaponA.DistanceTraveled() + _weaponB.DistanceTraveled();
        return calculatedDistance < minCollisionDistance ? minCollisionDistance : calculatedDistance;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(_weaponA.transform.position, _weaponB.transform.position);
        Debug.Log("distance: " + distance + " threshold: " + CollisionDistance());
        if(Vector3.Distance(_weaponA.transform.position, _weaponB.transform.position) < CollisionDistance()){
            Debug.Log("unlock the things");
            _weaponA.grip.UnlockRotation();
            _weaponB.grip.UnlockRotation();
        } else {
            _weaponA.grip.LockRotation();
            _weaponB.grip.LockRotation();
        }
    }


}

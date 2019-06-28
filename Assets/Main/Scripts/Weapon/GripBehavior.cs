using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GripBehavior : MonoBehaviour
{
    private ConfigurableJoint _joint;

    void Start()
    {
        _joint = GetComponent<ConfigurableJoint>();
        LockRotation();
    }

    public void LockRotation(){
        //need to detect collisions when moving quickly again, 
        //may have to wait until the blade reaches its 0 rotation

        // another thing i could do is check the distance between the two weapons, if theyre close enough, unlock the motion, that avoids the need for colliders, and can be done during update

        _joint.angularXMotion = ConfigurableJointMotion.Locked;
        _joint.angularYMotion = ConfigurableJointMotion.Locked;
        _joint.angularZMotion = ConfigurableJointMotion.Locked;
    }

    public void UnlockRotation(){
        _joint.angularXMotion = ConfigurableJointMotion.Free;
        _joint.angularYMotion = ConfigurableJointMotion.Free;
        _joint.angularZMotion = ConfigurableJointMotion.Free;
    }


    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeTrigger : MonoBehaviour
{

    public AbstractPlayer weilder;

    void OnCollisionEnter(Collision c)
    {
        weilder.BladesCollided();
    }

    void OnCollisionExit(Collision c)
    {
    }
}

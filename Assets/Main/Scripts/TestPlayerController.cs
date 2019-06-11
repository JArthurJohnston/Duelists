using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerController : AbstractPlayer
{
    public float speed;
    public float rotationSpeed;
    public GameObject weaponHand;
    public float attackAngle = 5f;
    // Update is called once per frame
    void Update()
    {
        
        var color = Vector3.Angle(Oponent.weapon.Heading(), transform.position - Oponent.weapon.Position()) > attackAngle ? 
            Color.cyan : Color.magenta;
        Debug.DrawLine(transform.position, Oponent.weapon.Position(), color);

        MovePlayer();
        MoveWeapon();
    }

    void MovePlayer(){
        float translation = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
        
        // Move translation along the object's z-axis
        transform.Translate(0, 0, translation);

        // Rotate around our y-axis
        transform.Rotate(0, rotation, 0);
    }

    void MoveWeapon(){
        float rotationX = Input.GetAxisRaw("Mouse X") * rotationSpeed * Time.deltaTime;
        float rotationY = Input.GetAxisRaw("Mouse Y") * rotationSpeed * Time.deltaTime * -1;

        weaponHand.transform.Rotate(rotationY, rotationX, 0);
    }
}

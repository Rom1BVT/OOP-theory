using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float speed = 15;
    private float yRotationMax = 10;
    private float xPositionRange = 17;
    private float zPositionMax = 28;
    private float zPositionMin = -5;
    private float rotationSpeed = 100;

    private Camera cam;
    public GameObject turret;

    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        float yEulerRotation = transform.rotation.eulerAngles.y;
        if (yEulerRotation > 180) { yEulerRotation -= 360; } //return the rotation between -180 and 180 

        //add a translation to the player
        transform.Translate(Vector3.forward * verticalInput * speed * Time.deltaTime, Space.World);
        transform.Translate(Vector3.right * horizontalInput * speed * Time.deltaTime, Space.World);

        //avoid the player to quit the road
        if (transform.position.z < zPositionMin)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zPositionMin);
        }
        else if (transform.position.z > zPositionMax)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zPositionMax);
        }

        if (transform.position.x < -xPositionRange )
        {
            transform.position = new Vector3(-xPositionRange, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > xPositionRange)
        {
            transform.position = new Vector3(xPositionRange, transform.position.y, transform.position.z);
        }


        // add a rotation when the player moves left or right
        if (horizontalInput > 0)
        {
            if (yEulerRotation < yRotationMax)
            {
                transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
            }    
        }
        else if (horizontalInput < 0)
        {
            if (yEulerRotation > -yRotationMax)
            {
                transform.Rotate(-Vector3.up * rotationSpeed * Time.deltaTime);
            }
        }
        else
        {
            if (yEulerRotation > 0)
            {
                transform.Rotate(-Vector3.up * rotationSpeed * Time.deltaTime);
            }
            else if (yEulerRotation < 0)
            {
                transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
            }
        }

        // add a rotation to the turret to follow cursor
            
        //get the mouse position in world space coordinates
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = GameObject.Find("Main Camera").transform.position.y;
        Vector3 mouseWorldPos = cam.ScreenToWorldPoint(mousePosition);

        //calculate the angle between the player position and the mouse position
        Quaternion turretRotation = Quaternion.FromToRotation(Vector3.forward, transform.position - mouseWorldPos);

        //apply this angle to the turret (reset the rotation and apply a rotate action) 
        float yTurretAngle = turretRotation.eulerAngles.y - 180;
        Debug.Log(yTurretAngle);
        turret.transform.rotation = new Quaternion(0, 0, 0, 0);
        turret.transform.Rotate(0, yTurretAngle, 0);

    }
}
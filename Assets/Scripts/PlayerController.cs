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


    // Update is called once per frame
    void Update()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        float yEulerRotation = transform.rotation.eulerAngles.y;
        if (yEulerRotation > 180) { yEulerRotation -= 360; }

        transform.Translate(Vector3.forward * verticalInput * speed * Time.deltaTime, Space.World);
        transform.Translate(Vector3.right * horizontalInput * speed * Time.deltaTime, Space.World);
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

    }
}

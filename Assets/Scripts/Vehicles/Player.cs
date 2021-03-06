using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Vehicles
{
    //MoveWithPlayerInput() variables
    private float speed = 15;
    private float yRotationMax = 10;
    private float xPositionRange = 17;
    private float zPositionMax = 28;
    private float zPositionMin = -5;
    private float rotationSpeed = 100;
    private float spreadRadius = 4;

    public GameObject turret;
    public GameObject bodyArmor;

    //Missiles variables
    public GameObject missilePrefab;
    private string missilePath = "/Canvas/Missiles/Missile";
    private int missileCount;
    private int maxMissiles = 4;
    private int missileCooldown = 8;

    private Player()
    {
        healthPoint = 100;
        speedFire = 10;
        missileCount = maxMissiles;
    }


    //this method will be called in every frame
    protected override void Behaviour()
    {
        MoveWithPlayerInput(); //ABSTRACTION

        if (Input.GetButton("Fire1"))
        {
            Shoot(ammoPrefab, shotOrigin, spreadRadius);
        }
        else if (Input.GetButtonDown("Fire2"))
        {
            if (CheckMissile())
            {
                Shoot(missilePrefab, shotOrigin);
            }
        }
    }

    protected override void HandlingHealthbar()
    {
        if (HealthbarInstance == null)
        {
            HealthbarInstance = GameObject.Find("Player_Healthbar").GetComponent<Slider>();
        }
        HealthbarInstance.value = healthPoint / maxHealthPoint;
    }

    private void MoveWithPlayerInput()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");


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

        if (transform.position.x < -xPositionRange)
        {
            transform.position = new Vector3(-xPositionRange, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > xPositionRange)
        {
            transform.position = new Vector3(xPositionRange, transform.position.y, transform.position.z);
        }


        // add a rotation to the body armor when the player moves left or right
        float yEulerRotation = bodyArmor.transform.rotation.eulerAngles.y;
        if (yEulerRotation > 180) { yEulerRotation -= 360; } //return the rotation between -180 and 180 

        if (horizontalInput > 0)
        {
            if (yEulerRotation < yRotationMax)
            {
                bodyArmor.transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
            }
        }
        else if (horizontalInput < 0)
        {
            if (yEulerRotation > -yRotationMax)
            {
                bodyArmor.transform.Rotate(-Vector3.up * rotationSpeed * Time.deltaTime);
            }
        }
        else
        {
            if (yEulerRotation > 0)
            {
                bodyArmor.transform.Rotate(-Vector3.up * rotationSpeed * Time.deltaTime);
            }
            else if (yEulerRotation < 0)
            {
                bodyArmor.transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
            }
        }

        // add a rotation to the turret to follow cursor

        //get the mouse position in world space coordinates
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = GameObject.Find("Main Camera").transform.position.y;
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePosition);

        //calculate the angle between the player position and the mouse position
        Quaternion turretRotation = Quaternion.FromToRotation(Vector3.forward, transform.position - mouseWorldPos);

        //apply this angle to the turret (reset the rotation and apply a rotate action) 
        float yTurretAngle = turretRotation.eulerAngles.y - 180;
        turret.transform.rotation = new Quaternion(0, 0, 0, 0);
        turret.transform.Rotate(0, yTurretAngle, 0);

    }

    protected override void ComeOnStage()
    {
        isOnStage = true; // no entry routine
    }

    private bool CheckMissile()
    {
        if (missileCount > 0)
        {
            GameObject.Find(missilePath + missileCount).SetActive(false);
            missileCount--;
            StartCoroutine(RecoverMissileCooldown());
            return true;
        }
        else
        {
            return false;
        }

    }

    IEnumerator RecoverMissileCooldown()
    {
        yield return new WaitForSeconds(missileCooldown);
        missileCount++;
        GameObject.Find(missilePath + missileCount).SetActive(true);
    }
}

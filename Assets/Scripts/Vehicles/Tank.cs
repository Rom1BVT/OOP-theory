using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : Vehicles
{
    [SerializeField] protected GameObject turret;
    private Tank()
    {
        healthPoint = 300;
        speedFire = 0.5f;
        pointValue = 150;
        offsetBarPosition = new Vector3(0, 6, 0);
        entryDelay = 2.0f;
        zUnits = 11.0f;
    }

    //this method will be called in every frame
    protected override void Behaviour()
    {
        RotateTurret();
        Shoot(ammoPrefab, shotOrigin);
    }

    private void RotateTurret()
    {
        var playerPosition = FindPlayerPosition(); ;
        var turretRotation = Quaternion.FromToRotation(Vector3.forward, playerPosition - turret.transform.position);


        float yTurretAngle = turretRotation.eulerAngles.y;
        turret.transform.rotation = new Quaternion(0, 0, 0, 0);
        turret.transform.Rotate(0, yTurretAngle, 0);
    }


}

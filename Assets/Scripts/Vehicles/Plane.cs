using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : Vehicles
{
    Plane()
    {
        healthPoint = 1000;
        speedFire = 1.0f;
        pointValue = 1000;
        offsetBarPosition = new Vector3(0, 3.5f, 0);
        entryDelay = 4.0f;
        zUnits = 20.0f;
    }

    private float zMax = 28.0f;
    private int direction = 1;
    private float timeBeforeSwitch;

    protected override void Behaviour()
    {
        var PlayerPosition = GameObject.Find("Player").transform.position;
        Shoot(ammoPrefab, shotOrigin, PlayerPosition);
        transform.Translate(Vector3.forward * direction * Time.deltaTime * zUnits / entryDelay);

        timeBeforeSwitch -= Time.deltaTime;
        if(timeBeforeSwitch <= 0)
        {
            direction *= -1;
            timeBeforeSwitch = zMax / (zUnits / entryDelay);
        }
    }

    protected override void ComeOnStage()
    {
        //called every frame 
        transform.Translate(Vector3.forward * Time.deltaTime * zUnits / entryDelay);
        if (entryTimeLeft < Time.time)
        {
            isOnStage = true;
            timeBeforeSwitch = zMax / (zUnits / entryDelay);
        }
    }

}

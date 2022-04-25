using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Van : Vehicles
{
    private Van()
    {
        healthPoint = 100;
        speedFire = 0.5f;
        pointValue = 50;
        offsetBarPosition = new Vector3(0, 4, -2);
    }

    //this method will be called in every frame
    protected override void Behaviour()
    {
        var PlayerPosition = GameObject.Find("Player").transform.position;
        Shoot(ammoPrefab, shotOrigin,PlayerPosition);
    }


}

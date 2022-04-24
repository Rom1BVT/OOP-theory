using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : Vehicles
{
    // Start is called before the first frame update
    Plane()
    {
        healthPoint = 1000;
        speedFire = 1.0f;
        pointValue = 1000;
        offsetBarPosition = new Vector3(0, 3.5f, 0);
    }

    //this method will be called in every frame
    protected override void Behaviour()
    {

    }


}

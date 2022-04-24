using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : Vehicles
{
    // Start is called before the first frame update
    private Tank()
    {
        healthPoint = 300;
        speedFire = 0.5f;
        pointValue = 150;
        offsetBarPosition = new Vector3(0, 6, 0);
    }

    //this method will be called in every frame
    protected override void Behaviour()
    {

    }


}

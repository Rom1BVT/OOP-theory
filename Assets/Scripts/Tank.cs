using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : Vehicles
{
    // Start is called before the first frame update
    void Start()
    {
        healthPoint = 300;
        speedFire = 0.5f;
        pointValue = 150;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

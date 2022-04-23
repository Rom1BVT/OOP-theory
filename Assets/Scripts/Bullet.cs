using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Ammunition
{

    // Start is called before the first frame update
    void Start()
    {
        velocity = 100;
        strikePower = 5;
    }


}

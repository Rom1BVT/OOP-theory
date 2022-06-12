using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Player : Ammunition
{
    Bullet_Player()
    {
        velocity = 100;
        strikePower = 5; //reset to 5
        targetTag = "Enemy";
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Enemy : Ammunition
{
    Bullet_Enemy()
    {
        velocity = 10;
        strikePower = 5;
        targetTag = "Player";
    }

}

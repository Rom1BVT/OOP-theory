using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicles : MonoBehaviour
{
    private int healthPoint;
    private float speedFire;
    private int pointValue;
    
    public void TakeDamage(int damage)
    {
        healthPoint -= damage;
    }
    public void Shoot()
    {

    }

    public virtual void UpdateLifebar()
    {

    }

}

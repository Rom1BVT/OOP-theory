using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Ammunition
{
    [SerializeField] private GameObject explosionParticles;
    Missile()
    {
        velocity = 20;
        strikePower = 50;
        targetTag = "Enemy";
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(targetTag))
        {
            other.gameObject.GetComponent<Vehicles>().TakeDamage(strikePower);
            Instantiate(explosionParticles, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}

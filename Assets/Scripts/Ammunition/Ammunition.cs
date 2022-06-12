using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammunition : MonoBehaviour
{
    // INHERITANCE - Caracteristics of Ammunitions
    protected float velocity;
    protected int strikePower;
    protected string targetTag;

    private float inboundLimit = 50;




    private void Update() //INHERITANCE
    {
        //move forward
        transform.Translate(Vector3.forward * velocity * Time.deltaTime);
        
        //destroy if ammo is out of the screen
        if (transform.position.x < -inboundLimit || transform.position.x > inboundLimit)
        {
            Destroy(gameObject);
        }
        else if(transform.position.z < -inboundLimit || transform.position.z > inboundLimit)
        {
            Destroy(gameObject);
        }
    }


    protected virtual void OnTriggerEnter(Collider other) //INHERITANCE
    {
        if (other.gameObject.CompareTag(targetTag))
        {
            other.gameObject.GetComponent<Vehicles>().TakeDamage(strikePower);
            Destroy(gameObject);
        }
    }

   
}

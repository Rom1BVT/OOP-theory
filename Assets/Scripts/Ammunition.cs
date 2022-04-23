using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammunition : MonoBehaviour
{
    protected float velocity;
    protected int strikePower;
    private float inboundLimit = 50;
    


    private void Update()
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


    private void OnTriggerEnter(Collider other)
    {
        
    }
}

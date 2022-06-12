using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropellorRotation : MonoBehaviour
{
    private float speed = 1440;


    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * speed * Time.deltaTime);
    }
}

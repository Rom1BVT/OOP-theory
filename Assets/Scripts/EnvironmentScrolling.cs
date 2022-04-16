using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentScrolling : MonoBehaviour
{
    private BoxCollider groundCollider;
    private Vector3 initialPosition;
    [SerializeField]private float speed;

    private void Awake()
    {
        groundCollider = GetComponent<BoxCollider>();
    }
    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);
        if(transform.position.z < initialPosition.z - groundCollider.size.z/2)
        {
            transform.position = initialPosition;
        }
    }
}

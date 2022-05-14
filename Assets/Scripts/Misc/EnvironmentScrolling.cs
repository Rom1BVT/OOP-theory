using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentScrolling : MonoBehaviour
{
    private BoxCollider groundCollider;
    private GameManager gameManager;
    private Vector3 initialPosition;
    private float maxSpeed = 80;
    private float currentSpeed;
    private float secondsToStop = 10;

    private void Awake()
    {
        groundCollider = GetComponent<BoxCollider>();
        gameManager = GameObject.FindObjectOfType<GameManager>();

    }
    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        currentSpeed = maxSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        MoveForward();
        if(gameManager.isGameOver == true)
        {
            Decelerate();
        }
    }

    private void MoveForward()
    {
        transform.Translate(Vector3.back * currentSpeed * Time.deltaTime);
        if (transform.position.z < initialPosition.z - groundCollider.size.z / 2)
        {
            transform.position = initialPosition;
        }
    }

    private void Decelerate()
    {
        if (currentSpeed > 0)
        {
            currentSpeed -= Time.deltaTime * maxSpeed / secondsToStop;
        }
        else
        {
            currentSpeed = 0;
        }
    }
}

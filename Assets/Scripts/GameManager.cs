using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject[] enemyPrefab;

    //Variables for enemy positionning
    private Vector3 spawnMark = new Vector3(-14.37f, 0, 41.0f);
    private float gapBetweenLanes = 9.67f;
    private int lanes = 4;

    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        //for test purpose
        if (Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            InstanciateEnemy();
        }
    }

    private void InstanciateEnemy()
    {
        Instantiate(enemyPrefab[0], RandomSpawnPosition(), enemyPrefab[0].transform.rotation);

    }

    private Vector3 RandomSpawnPosition()
    {
        float randomX = spawnMark.x + Random.Range(0, lanes-1) * gapBetweenLanes;
        Vector3 randomPosition = new Vector3(randomX, spawnMark.y, spawnMark.z);
        return randomPosition;
    }


}


/*
 * Spawn positions :
 *-14.37 -4.7
 *9.67
 *
 * instanciation:
 * tire au hasard une voie de circulation
 * on instancie le véhicule
 * 
 * si un véhicule est sur une voie de circulation on ne peux pas instancier à cette position
*/
